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
using Transmitly.ChannelProvider.TwilioClient.Configuration;
using Transmitly.ChannelProvider.TwilioClient.Configuration.Sms;
using Transmitly.ChannelProvider.TwilioClient.Configuration.Voice;
using Transmitly.ChannelProvider.TwilioClient.Sms;
using Transmitly.ChannelProvider.TwilioClient.Voice;

namespace Transmitly
{
    /// <summary>
    /// Provides Twilio specific channel provider extension methods.
    /// </summary>
    public static class TwilioChannelProviderExtensions
    {
        /// <summary>
        /// Adds channel provider support for Twilio.
        /// </summary>
        /// <param name="builder">Communications builder.</param>
        /// <param name="options">Twilio options.</param>
        /// <param name="providerId">Optional channel provider Id.</param>
        /// <returns></returns>
        public static CommunicationsClientBuilder AddTwilioSupport(this CommunicationsClientBuilder builder, Action<TwilioClientOptions> options, string? providerId = null)
        {
            var opts = new TwilioClientOptions();
            options(opts);
            builder.ChannelProvider
                .Build(Id.ChannelProvider.Twilio(providerId), opts)
                .AddClient<TwilioSmsChannelProviderClient, ISms>(Id.Channel.Sms())
                .AddClient<TwilioVoiceChannelProviderClient, IVoice>(Id.Channel.Voice())
                .AddDeliveryReportRequestAdaptor<TwilioSmsDeliveryStatusReportAdaptor>()
                .AddDeliveryReportRequestAdaptor<TwilioVoiceDeliveryStatusReportAdaptor>()
                .AddDeliveryReportExtendedProprtiesAdaptor<DeliveryReportExtendedProperties>()
                .AddSmsExtendedPropertiesAdaptor<ExtendedSmsChannelProperties>()
                .AddVoiceExtendedPropertiesAdaptor<ExtendedVoiceChannelProperties>()
                .Register();

            return builder;
        }
    }
}