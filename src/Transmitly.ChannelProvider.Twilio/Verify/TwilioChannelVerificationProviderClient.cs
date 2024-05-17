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
using Transmitly.ChannelProvider.TwilioClient.Sms;
using Transmitly.Verification;
using Twilio.Rest.Verify.V2.Service;

namespace Transmitly.ChannelProvider.TwilioClient.Verify
{
    internal sealed class TwilioChannelVerificationProviderClient(TwilioClientOptions _twilioClientOptions) : BaseChannelVerificationChannelProviderRestClient(null)
    {
        private static ChannelVerificationStatus ConvertStatus(string status)
        {
            return status switch
            {
                "pending" => ChannelVerificationStatus.Dispatched,
                "approved" => ChannelVerificationStatus.Delivered,
                "canceled" => ChannelVerificationStatus.Canceled,
                _ => ChannelVerificationStatus.Unknown,
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

        public override async Task<IChannelVerificationValidationResult> CheckChannelVerificationAsync(IChannelVerificationContext channelVerificationContext, string code, string? token = null)
        {
            var properties = new ChannelVerificationExtendedProperties(channelVerificationContext.ExtendedProperties);

            if (string.IsNullOrWhiteSpace(properties.VerificationSid))
                throw new NotImplementedException();

            var result = await VerificationCheckResource.CreateAsync(new CreateVerificationCheckOptions(properties.VerificationSid)
            {
                To = channelVerificationContext.RecipientAddress.Value,
                VerificationSid = token,
                Code = code
            }, new TwilioHttpClient(HttpClient, _twilioClientOptions)).ConfigureAwait(false);

            return new ChannelVerificationValidationResult(true, result.Valid.HasValue && result.Valid.Value, channelVerificationContext.ChannelProviderId!, channelVerificationContext.ChannelId!, channelVerificationContext.RecipientAddress.Value);

        }

        public override async Task<IReadOnlyCollection<IStartChannelVerificationResult>> StartChannelVerificationAsync(IChannelVerificationContext channelVerificationContext)
        {
            Guard.AgainstNull(channelVerificationContext.ChannelId);
            var properties = new ChannelVerificationExtendedProperties(channelVerificationContext.ExtendedProperties);
            if (string.IsNullOrWhiteSpace(properties.VerificationSid))
                return [new StartChannelVerificationResult(ChannelVerificationStatus.Exception, channelVerificationContext.ChannelId, null)];

            var verification = await VerificationResource.CreateAsync(
                to: channelVerificationContext.RecipientAddress.Value,
                channel: ToTwilioChannel(channelVerificationContext.ChannelId),
                pathServiceSid: properties.VerificationSid, client: new TwilioHttpClient(HttpClient, _twilioClientOptions)).ConfigureAwait(false);

            var results = new List<IStartChannelVerificationResult>(verification.SendCodeAttempts.Count);

            foreach (var result in verification.SendCodeAttempts)
            {
                if (result == null)
                    continue;
                SendCodeAttempt? sendAttempt = new();
                sendAttempt = JsonConvert.DeserializeAnonymousType(JsonConvert.SerializeObject(result), sendAttempt);
                if (sendAttempt == null)
                    continue;

                results.Add(new StartChannelVerificationResult(ConvertStatus(verification.Status), channelVerificationContext.ChannelId!, verification.Sid));
            }
            return results;
        }

        protected override void ConfigureHttpClient(System.Net.Http.HttpClient client)
        {
            RestClientConfiguration.Configure(client, _twilioClientOptions);
            base.ConfigureHttpClient(client);
        }
    }
}