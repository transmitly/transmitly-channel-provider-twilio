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
using Transmitly.ChannelProvider.Twilio;
using Twilio;

namespace Transmitly
{
	public static class TwilioChannelProviderExtensions
	{
		private const string TwilioId = "Twilio";

		public static string Twilio(this ChannelProviders channelProviders, string? providerId = null)
		{
			Guard.AgainstNull(channelProviders);
			return channelProviders.GetId(TwilioId, providerId);
		}

		public static CommunicationsClientBuilder AddTwilioSupport(this CommunicationsClientBuilder channelProviderConfiguration, Action<TwilioClientOptions> options, string? providerId = null)
		{
			var opts = new TwilioClientOptions();
			options(opts);

			TwilioClient.Init(opts.AccountSid, opts.AuthToken);

			channelProviderConfiguration.AddChannelProvider<TwilioSmsChannelProviderClient, ISms>(Id.ChannelProvider.Twilio(providerId), Id.Channel.Sms());

			return channelProviderConfiguration;
		}
	}
}