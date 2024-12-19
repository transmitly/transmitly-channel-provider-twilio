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
using Transmitly.ChannelProvider.TwilioClient.Configuration.Verify;
using Transmitly.ChannelProvider.TwilioClient.Configuration.Voice;
using Transmitly.ChannelProvider.TwilioClient.Sms;
using Transmitly.ChannelProvider.TwilioClient.Verify;
using Transmitly.ChannelProvider.TwilioClient.Voice;
using Transmitly.Delivery;
using Transmitly.Verification.Configuration;

namespace Transmitly
{
	/// <summary>
	/// Provides Twilio specific channel provider extension methods.
	/// </summary>
	public static class TwilioChannelProviderExtensions
	{
		//todo: this won't work for multiple twilio configurations
		static TwilioCommunicationsClientExtensions? _clientExtensions;

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
        public static ExtendedSmsChannelProperties Twilio(this ISmsChannel sms)
		{
			return new ExtendedSmsChannelProperties(sms);
		}

		/// <summary>
		/// Twilio specific settings for voice channels.
		/// </summary>
		/// <param name="voice">Voice Channel.</param>
		/// <returns>Twilio voice properties.</returns>
		public static ExtendedVoiceChannelProperties Twilio(this IVoiceChannel voice)
		{
			return new ExtendedVoiceChannelProperties(voice);
		}

		/// <summary>
		/// Twilio specific settings for sms delivery reports.
		/// </summary>
		/// <param name="deliveryReport">Delivery Report.</param>
		/// <returns>Twilio SMS delivery report properties.</returns>
		public static DeliveryReportExtendedProperties Twilio(this DeliveryReport deliveryReport)
		{
			return new DeliveryReportExtendedProperties(deliveryReport);
		}

		/// <summary>
		/// Twilio specific sender verification configurations.
		/// </summary>
		/// <param name="configuration">Sender verification configuration.</param>
		/// <returns>Twilio extended properties for sender verification.</returns>
		public static ChannelVerificationExtendedProperties Twilio(this IChannelVerificationConfiguration configuration)
		{
			return new ChannelVerificationExtendedProperties(configuration.ExtendedProperties);
		}

		/// <summary>
		/// Provides twilio specific methods for the provided <see cref="ICommunicationsClient"/>.
		/// </summary>
		/// <param name="communicationsClient">Communications client instance.</param>
		/// <returns></returns>
		public static TwilioCommunicationsClientExtensions Twilio(this ICommunicationsClient communicationsClient)
		{
			if (_clientExtensions == null)
				throw new NotSupportedException($"Twilio not configured. You must first call {nameof(AddTwilioSupport)}. and initialize the Twilio channel provider.");

			return _clientExtensions;
		}
		
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
				.AddChannelVerificationClient<TwilioChannelVerificationProviderClient>(opts, Id.Channel.Sms(), Id.Channel.Email(), Id.Channel.Voice())
				.Register();

			_clientExtensions = new(opts);

			return builder;
		}
	}
}