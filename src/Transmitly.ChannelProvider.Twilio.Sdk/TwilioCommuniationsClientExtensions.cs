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
using System.Net.Http;
using System.Threading.Tasks;
using Transmitly.ChannelProvider.TwilioClient;
using Transmitly.ChannelProvider.TwilioClient.Configuration;
using Twilio.Rest.Api.V2010.Account;

namespace Transmitly
{
    /// <summary>
    /// Twilio specific communications client extensions.
    /// </summary>
    public sealed class TwilioCommunicationsClientExtensions
    {
        private readonly TwilioClientOptions _twilioClientOptions;
        private static readonly HttpClient _httpClient = new();

        public TwilioCommunicationsClientExtensions(TwilioClientOptions twilioClientOptions)
        {
            _twilioClientOptions = Guard.AgainstNull(twilioClientOptions);
        }

        /// <summary>
        /// Validate a number to be used for caller Id.
        /// </summary>
        /// <returns></returns>
        public async Task ValidateCallerIdAsync(CreateValidationRequestOptions createValidationRequestOptions)
        {
            var result = await ValidationRequestResource.CreateAsync(Guard.AgainstNull(createValidationRequestOptions), new TwilioHttpClient(_httpClient, _twilioClientOptions));
            throw new NotImplementedException();

            //return new { result.ValidationCode, result.CallSid, result.FriendlyName, PhoneNumber = result.PhoneNumber.ToString(), result.AccountSid };
        }
    }
}