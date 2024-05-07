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
using System.Threading.Tasks;
using Transmitly.ChannelProvider.TwilioClient;
using Transmitly.ChannelProvider.TwilioClient.Sms;
using Transmitly.ChannelProvider.TwilioClient.Voice;
using Transmitly.Delivery;
using Transmitly.Verification;
using Transmitly.Verification.Configuration;

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

		public static SenderVerificationExtendedProprties Twilio(this ISenderVerificationConfiguration configuration)
		{
			return new SenderVerificationExtendedProprties(configuration.ExtendedProperties);
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
				.Create(Id.ChannelProvider.Twilio(providerId), opts)
				.AddClient<TwilioSmsChannelProviderClient, ISms>(Id.Channel.Sms())
				.AddClient<TwilioVoiceChannelProviderClient, IVoice>(Id.Channel.Voice())
				.AddDeliveryReportRequestAdaptor<SmsDeliveryStatusReportAdaptor>()
				.AddDeliveryReportRequestAdaptor<VoiceDeliveryStatusReportAdaptor>()
				.AddSenderVerificationClient<SmsSenderVerificationProviderClient>(true, Id.Channel.Sms())
				.Register();
			//builder.AddChannelProvider<TwilioSmsChannelProviderClient, ISms>(Id.ChannelProvider.Twilio(providerId), opts, Id.Channel.Sms());
			//builder.AddChannelProvider<TwilioVoiceChannelProviderClient, IVoice>(Id.ChannelProvider.Twilio(providerId), opts, Id.Channel.Voice());
			//builder.ChannelProvider.AddDeliveryReportRequestAdaptor<SmsDeliveryStatusReportAdaptor>();
			//builder.ChannelProvider.AddDeliveryReportRequestAdaptor<VoiceDeliveryStatusReportAdaptor>();
			//builder.ChannelProvider.AddSenderVerificationClient<SmsSenderVerificationProvider>(Id.Channel.Sms());

			return builder;
		}
	}

	public sealed class SenderVerificationExtendedProprties
	{
		private readonly IExtendedProperties _extendedProprties;

		internal SenderVerificationExtendedProprties(IExtendedProperties extendedProperties)
        {
            _extendedProprties = extendedProperties;
        }
        /// <summary>
        /// The SID of the verification Service to fetch the resource from.
        /// </summary>
        public string? ServiceSid
		{
			get => _extendedProprties.GetValue<string?>(Constant.SenderVerifyPropertyKey, nameof(ServiceSid));
			set => _extendedProprties.AddOrUpdate(Constant.SenderVerifyPropertyKey, nameof(ServiceSid), value);
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
	class InitiateSenderVerificationResult(bool isSuccessful, string channelId, string? code, string? sid) : IInitiateSenderVerificationResult
	{
		public bool IsSuccessful => isSuccessful;

		public string? Code => code;

		public string? Nonce => sid;

		public string ChannelId => channelId;

		public string ChannelProviderId => Id.ChannelProvider.Twilio();
	}



	class SmsSenderVerificationProviderClient : ISenderVerificationChannelProviderClient
	{
		public Task<IInitiateSenderVerificationResult> InitiateSenderVerification(ISenderVerificationContext senderVerificationContext)
		{
			var properties = new SenderVerificationExtendedProprties(senderVerificationContext.ExtendedProperties);
			if (string.IsNullOrWhiteSpace(properties.ServiceSid))
				return Task.FromResult<IInitiateSenderVerificationResult>(new InitiateSenderVerificationResult(false, senderVerificationContext.ChannelId, null, null));
			throw new NotImplementedException();
		}

		public Task<ISenderVerificationStatus> IsSenderVerified(ISenderVerificationContext senderVerificationContext)
		{
			throw new NotImplementedException();
		}

		public Task<ISenderVerificationValidationResult> ValidateSenderVerification(ISenderVerificationContext senderVerificationContext, string code, string? nonce = null)
		{
			throw new NotImplementedException();
		}
	}
}