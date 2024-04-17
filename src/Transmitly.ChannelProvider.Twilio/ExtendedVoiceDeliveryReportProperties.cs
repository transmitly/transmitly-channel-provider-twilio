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
using Transmitly.ChannelProvider.TwilioClient.Voice;
using Transmitly.Delivery;

namespace Transmitly
{
	public sealed class ExtendedVoiceDeliveryReportProperties
	{
		private readonly IExtendedProperties _extendedProperties;
		private const string ProviderKey = Constant.SmsPropertiesKey;
		internal ExtendedVoiceDeliveryReportProperties(DeliveryReport deliveryReport)
		{
			_extendedProperties = Guard.AgainstNull(deliveryReport).ExtendedProperties;
		}

		internal ExtendedVoiceDeliveryReportProperties(IExtendedProperties properties)
		{
			_extendedProperties = Guard.AgainstNull(properties);
		}

		internal void Apply(VoiceStatusReport report)
		{
			To = report.To;
			From = report.From;
			NumMedia = report.NumMedia;
			Duration = report.Duration;
		}

		public string? From
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(From));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(From), value);
		}

		public string? To
		{
			get => _extendedProperties.GetValue<string?>(ProviderKey, nameof(To));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(To), value);
		}

		public int? NumMedia
		{
			get => _extendedProperties.GetValue<int?>(ProviderKey, nameof(NumMedia));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(NumMedia), value);
		}

		public TimeSpan? Duration
		{
			get => _extendedProperties.GetValue<TimeSpan?>(ProviderKey, nameof(Duration));
			set => _extendedProperties.AddOrUpdate(ProviderKey, nameof(Duration), value);
		}

	}
}