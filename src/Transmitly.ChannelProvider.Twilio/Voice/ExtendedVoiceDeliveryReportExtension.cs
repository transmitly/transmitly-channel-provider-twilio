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

using Transmitly.ChannelProvider.TwilioClient.Voice;

namespace Transmitly
{
	internal static class ExtendedVoiceDeliveryReportExtension
	{
		public static VoiceDeliveryReport ApplyExtendedProperties(this VoiceDeliveryReport voiceDeliveryReport, VoiceStatusReport report)
		{
			_ = new ExtendedVoiceDeliveryReportProperties(voiceDeliveryReport)
			{
				To = report.To,
				From = report.From,
				AccountSid = report.AccountSid,
				ApiVersion = report.ApiVersion,
				CallSid = report.CallSid,
				Duration = report.Duration,
				Timestamp = report.Timestamp,
				CallToken = report.CallToken
			};
			return voiceDeliveryReport;
		}
	}
}