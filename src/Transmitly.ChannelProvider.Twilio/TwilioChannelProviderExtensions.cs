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
using Transmitly.ChannelProvider.Twilio;
using Transmitly.ChannelProvider.Twilio.Sms;
using Transmitly.ChannelProvider.Twilio.Voice;
using Twilio;

namespace Transmitly
{
	public static class TwilioChannelProviderExtensions
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
			return channelProviders.GetId(Constant.Id, providerId);
		}

		/// <summary>
		/// Twilio specific settings for Sms channels.
		/// </summary>
		/// <param name="sms">Sms Channel.</param>
		/// <returns>Twilio Sms properties.</returns>
		//public static ExtendedSmsChannelProperties Twilio(this ISmsChannel sms)
		//{
		//	return new ExtendedSmsChannelProperties(sms);
		//}

		/// <summary>
		/// Twilio specific settings for voice channels.
		/// </summary>
		/// <param name="sms">Voice Channel.</param>
		/// <returns>Twilio voice properties.</returns>
		public static ExtendedVoiceChannelProperties Twilio(this IVoiceChannel email)
		{
			return new ExtendedVoiceChannelProperties(email);
		}


		public static CommunicationsClientBuilder AddTwilioSupport(this CommunicationsClientBuilder channelProviderConfiguration, Action<TwilioClientOptions> options, string? providerId = null)
		{
			var opts = new TwilioClientOptions();
			options(opts);

			TwilioClient.Init(opts.AccountSid, opts.AuthToken);

			channelProviderConfiguration.AddChannelProvider<TwilioSmsChannelProviderClient, ISms>(Id.ChannelProvider.Twilio(providerId), Id.Channel.Sms());
			channelProviderConfiguration.AddChannelProvider<TwilioVoiceChannelProviderClient, IVoice>(Id.ChannelProvider.Twilio(providerId), Id.Channel.Voice());

			return channelProviderConfiguration;
		}
	}
}