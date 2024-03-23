// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
//  
//  Licensed under the Apache License, Version 2.0 (the "License")
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//  
//      http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Transmitly.ChannelProvider.Twilio.Voice
{
	internal sealed class TwilioVoiceChannelProviderClient : ChannelProviderClient<IVoice>
	{
		private const string MessageIdQueryStringKey = "resourceId";

		/// <summary>
		/// Dispatches a Voice communication using Twilio.
		/// </summary>
		/// <param name="voice">Voice communication.</param>
		/// <param name="communicationContext">Context of the dispatch.</param>
		/// <param name="cancellationToken">Cancellation token</param>
		/// <returns>Result of the dispatch.</returns>
		public override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(IVoice voice, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
		{
			var voiceProperties = new ExtendedVoiceChannelProperties(voice.ExtendedProperties);
			var recipients = communicationContext.RecipientAudiences.SelectMany(a => a.Addresses.Select(addr => new PhoneNumber(addr.Value))).ToList();
			var results = new List<IDispatchResult>(recipients.Count);
			var from = new PhoneNumber(Guard.AgainstNull(voice.From).Value);

			foreach (var recipient in recipients)
			{
				Dispatch(communicationContext, voice);
				if (voiceProperties.OnStoreMessageForRetrievalAsync != null)
					_ = Task.Run(() => voiceProperties.OnStoreMessageForRetrievalAsync(voice, communicationContext).ConfigureAwait(false), cancellationToken).ConfigureAwait(false);

				var messageId = Guid.NewGuid().ToString("N");
				var resource = await CallResource.CreateAsync(
					to: recipient,
					from: from,
					url: await GetTwiMLCallbackUrl(messageId, voiceProperties, communicationContext).ConfigureAwait(false),
					timeout: voiceProperties.Timeout,
					statusCallback: await GetStatusCallbackUrl(messageId, voiceProperties, voice, communicationContext).ConfigureAwait(false),
					statusCallbackMethod: voiceProperties.StatusCallbackMethod,
					machineDetection: ConvertMachineDetection(voice.MachineDetection, voiceProperties.MachineDetection)
				);

				var twResult = new TwilioDispatchResult(resource.Sid);
				results.Add(twResult);

				if (IsDispatched(resource))
					Dispatched(communicationContext, voice, [twResult]);
				else
					Error(communicationContext, voice, [twResult]);
			}

			return results;
		}

		private static bool IsDispatched(CallResource resource)
		{
			return resource.Status != CallResource.StatusEnum.Failed && resource.Status != CallResource.StatusEnum.Canceled;
		}

		private static string? ConvertMachineDetection(Transmitly.MachineDetection tlyValue, MachineDetection? overrideValue)
		{
			MachineDetection? value;
			if (overrideValue.HasValue)
			{
				value = overrideValue;
			}
			else
			{
				value = tlyValue switch
				{
					Transmitly.MachineDetection.Enabled =>
						(MachineDetection?)MachineDetection.Enable,
					Transmitly.MachineDetection.MessageEnd =>
						(MachineDetection?)MachineDetection.DetectMessageEnd,
					_ => null,
				};
			}
			if (value == null)
				return null;

			return Enum.GetName(typeof(MachineDetection), value);
		}

		private static async Task<Uri> GetTwiMLCallbackUrl(string messageId, ExtendedVoiceChannelProperties voiceProperties, IDispatchCommunicationContext context)
		{
			const string RequiredUrlExceptionMessage = $"Twilio requires a url that returns TwiML when called. Ensure you have a value set for the Twilio extended property: '{nameof(voiceProperties.Url)}' OR '{nameof(voiceProperties.UrlResolver)}'.";

			if (string.IsNullOrWhiteSpace(voiceProperties.Url) && voiceProperties.UrlResolver == null)
				throw new TwilioException(RequiredUrlExceptionMessage);

			string? url = voiceProperties.Url;

			if (voiceProperties.UrlResolver != null)
				return new Uri(Guard.AgainstNullOrWhiteSpace(await voiceProperties.UrlResolver(context).ConfigureAwait(false)));
			else if (string.IsNullOrWhiteSpace(url))
				throw new TwilioException(RequiredUrlExceptionMessage);

			return AddParameter(new Uri(url), MessageIdQueryStringKey, messageId);
		}

		private static async Task<Uri?> GetStatusCallbackUrl(string messageId, ExtendedVoiceChannelProperties voiceProperties, IVoice voice, IDispatchCommunicationContext context)
		{
			string? url = voiceProperties.StatusCallbackUrl ?? voice.StatusCallbackUrl;

			var resolveUrl = voiceProperties.StatusCallbackUrlResolver ?? voice.StatusCallbackUrlResolver;
			if (resolveUrl != null)
			{
				var urlResult = await resolveUrl(context).ConfigureAwait(false);
				if (string.IsNullOrWhiteSpace(urlResult))
					return null;
				return new Uri(urlResult);
			}

			if (string.IsNullOrWhiteSpace(url))
				return null;

			return AddParameter(new Uri(url), MessageIdQueryStringKey, messageId);
		}

		//Source=https://stackoverflow.com/a/19679135
		private static Uri AddParameter(Uri url, string paramName, string paramValue)
		{
			var uriBuilder = new UriBuilder(url);
			var query = HttpUtility.ParseQueryString(uriBuilder.Query);
			query[paramName] = paramValue;
			uriBuilder.Query = query.ToString();

			return uriBuilder.Uri;
		}
	}
}