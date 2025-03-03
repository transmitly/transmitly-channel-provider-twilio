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
using Transmitly.ChannelProvider.Configuration;
using Transmitly.ChannelProvider.TwilioClient.Configuration.Sms;
using Transmitly.ChannelProvider.TwilioClient.Configuration.Voice;
using Transmitly.Delivery;

namespace Transmitly.ChannelProvider.TwilioClient.Configuration
{
	public static class TwilioChannelProviderConfigurationExtensions
	{
		/// <summary>
		/// Gets the channel provider id for Twilio.
		/// </summary>
		/// <param name="channelProviders">Channel providers object.</param>
		/// <param name="providerId">Optional channel provider Id.</param>
		/// <returns>Twilio channel provider id.</returns>
		public static string Twilio(this ChannelProviders channelProviders, string? providerId = null)
		{
			Guard.AgainstNull(channelProviders);
			return channelProviders.GetId(TwilioConstant.Id, providerId);
		}

		/// <summary>
		/// Twilio specific settings for Sms channels.
		/// </summary>
		/// <param name="sms">Sms Channel.</param>
		/// <returns>Twilio Sms properties.</returns>
		public static ISmsExtendedChannelProperties Twilio(this ISmsChannel sms)
		{
			return TwilioChannelProviderExtendedPropertiesBuilderExtensions.Sms.Adapt(sms);
		}

		/// <summary>
		/// Twilio specific settings for voice channels.
		/// </summary>
		/// <param name="voice">Voice Channel.</param>
		/// <returns>Twilio voice properties.</returns>
		public static IExtendedVoiceChannelProperties Twilio(this IVoiceChannel voice)
		{
			return TwilioChannelProviderExtendedPropertiesBuilderExtensions.Voice.Adapt(voice);
		}

		/// <summary>
		/// Twilio specific settings for sms delivery reports.
		/// </summary>
		/// <param name="deliveryReport">Delivery Report.</param>
		/// <returns>Twilio SMS delivery report properties.</returns>
		public static IDeliveryReportExtendedProperties Twilio(this DeliveryReport deliveryReport)
		{
			return TwilioChannelProviderExtendedPropertiesBuilderExtensions.DeliveryReport.Adapt(deliveryReport);
		}
	}
}
