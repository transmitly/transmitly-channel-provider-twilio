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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transmitly.Verification;
using Twilio.Rest.Verify.V2.Service;

namespace Transmitly.ChannelProvider.TwilioClient.Verify
{
	class TwilioSenderVerificationProviderClient : ISenderVerificationChannelProviderClient
	{
		public async Task<IReadOnlyCollection<IInitiateSenderVerificationResult>> InitiateSenderVerification(ISenderVerificationContext senderVerificationContext)
		{
			var properties = new SenderVerificationExtendedProperties(senderVerificationContext.ExtendedProperties);
			if (string.IsNullOrWhiteSpace(properties.ServiceSid))
				return [new InitiateSenderVerificationResult(SenderVerificationStatus.Exception, Guard.AgainstNull(senderVerificationContext.ChannelId), null)];

			var verification = await VerificationResource.CreateAsync(
				to: senderVerificationContext.SenderAddress.Value,
				channel: ToTwilioChannel(senderVerificationContext.ChannelId),
				pathServiceSid: properties.ServiceSid
			);
			var results = new List<IInitiateSenderVerificationResult>(verification.SendCodeAttempts.Count);
			foreach (var result in verification.SendCodeAttempts)
			{
				if (result == null)
					continue;
				SendCodeAttempt? sendAttempt = new();
				sendAttempt = JsonConvert.DeserializeAnonymousType(JsonConvert.SerializeObject(result), sendAttempt);
				if (sendAttempt == null)
					continue;

				results.Add(new InitiateSenderVerificationResult(ConvertStatus(verification.Status), senderVerificationContext.ChannelId!, sendAttempt.AttemptSid));
			}
			return results;
		}
		
		private static SenderVerificationStatus ConvertStatus(string status)
		{
			return status switch
			{
				"pending" => SenderVerificationStatus.Dispatched,
				"approved" => SenderVerificationStatus.Delivered,
				"canceled" => SenderVerificationStatus.Canceled,
				_ => SenderVerificationStatus.Unknown,
			};
		}

		private static string ToTwilioChannel(string? channel)
		{
			Guard.AgainstNullOrWhiteSpace(channel);

			if (channel.Equals(Id.Channel.Voice(), StringComparison.OrdinalIgnoreCase))
				return "call";
			else if (channel.Equals(Id.Channel.Sms(), StringComparison.OrdinalIgnoreCase))
				return "sms";
			else if (channel.Equals(Id.Channel.Email(), StringComparison.OrdinalIgnoreCase))
				return "email";
			else
				throw new TwilioException("Unexpected Transmitly channel.");

		}

		public Task<ISenderVerificationStatusResult> IsSenderVerified(ISenderVerificationContext senderVerificationContext, string? token)
		{
			return Task.FromResult<ISenderVerificationStatusResult>(new SenderVerificationStatusResult(senderVerificationContext.ChannelId!));
		}

		public Task<ISenderVerificationValidationResult> ValidateSenderVerification(ISenderVerificationContext senderVerificationContext, string code, string? token = null)
		{
			throw new NotImplementedException();
		}
	}
}