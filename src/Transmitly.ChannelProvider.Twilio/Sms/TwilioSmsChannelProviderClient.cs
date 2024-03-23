﻿// ﻿﻿Copyright (c) Code Impressions, LLC. All Rights Reserved.
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

namespace Transmitly.ChannelProvider.Twilio.Sms
{
	internal sealed class TwilioSmsChannelProviderClient : ChannelProviderClient<ISms>
	{
		private const string MessageIdQueryStringKey = "resourceId";

		public override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(ISms sms, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
		{
			Guard.AgainstNull(sms);
			Guard.AgainstNull(communicationContext);

			var recipients = communicationContext.RecipientAudiences.SelectMany(a => a.Addresses.Select(addr => new PhoneNumber(addr.Value))).ToList();
			var smsProperties = new ExtendedSmsChannelProperties(sms.ExtendedProperties);
			var results = new List<IDispatchResult>(recipients.Count);
			var messageId = Guid.NewGuid().ToString("N");

			foreach (var recipient in recipients)
			{
				Dispatch(communicationContext, sms);

				var message = await MessageResource.CreateAsync(
					recipient,
					from: sms.From?.Value,
					body: sms.Message,
					messagingServiceSid: smsProperties.MessagingServiceSid,
					statusCallback: await GetStatusCallbackUrl(messageId, smsProperties, sms, communicationContext).ConfigureAwait(false)
				);

				var twResult = new TwilioDispatchResult(message.Sid);

				results.Add(twResult);
				if (IsDispatched(message))
					Dispatched(communicationContext, sms, [twResult]);
				else
					Error(communicationContext, sms, [twResult]);
			}

			return results;
		}

		private static async Task<Uri?> GetStatusCallbackUrl(string messageId, ExtendedSmsChannelProperties smsProperties, ISms sms, IDispatchCommunicationContext context)
		{
			string? url = smsProperties.StatusCallbackUrl ?? sms.StatusCallbackUrl;

			var resolveUrl = smsProperties.StatusCallbackUrlResolver ?? sms.StatusCallbackUrlResolver;
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

		private static bool IsDispatched(MessageResource resource)
		{
			return resource.Status != MessageResource.StatusEnum.Failed && resource.Status != MessageResource.StatusEnum.Undelivered;
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
