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

namespace Transmitly.ChannelProvider.TwilioClient.Verify
{
    public sealed class SenderVerificationExtendedProperties
    {
        private readonly IExtendedProperties _extendedProprties;

        internal SenderVerificationExtendedProperties(IExtendedProperties extendedProperties)
        {
            _extendedProprties = extendedProperties;
        }
        /// <summary>
        /// The SID of the verification Service to fetch the resource from.
        /// </summary>
        public string? VerificationSid
        {
            get => _extendedProprties.GetValue<string?>(Constant.SenderVerifyPropertyKey, nameof(VerificationSid));
            set => _extendedProprties.AddOrUpdate(Constant.SenderVerifyPropertyKey, nameof(VerificationSid), value);
        }
        /// <summary>
        /// The Twilio-provided string that uniquely identifies the Verification resource to fetch.
        /// </summary>
        public string? Sid
        {
            get => _extendedProprties.GetValue<string?>(Constant.SenderVerifyPropertyKey, nameof(Sid));
            set => _extendedProprties.AddOrUpdate(Constant.SenderVerifyPropertyKey, nameof(Sid), value);
        }

        public string? TemplateId
        {
            get => _extendedProprties.GetValue<string?>(Constant.SenderVerifyPropertyKey, nameof(TemplateId));
            set => _extendedProprties.AddOrUpdate(Constant.SenderVerifyPropertyKey, nameof(TemplateId), value);
        }

    }
}