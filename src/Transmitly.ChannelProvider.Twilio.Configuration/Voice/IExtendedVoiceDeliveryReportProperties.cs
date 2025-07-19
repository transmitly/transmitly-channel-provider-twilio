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

namespace Transmitly.ChannelProvider.Twilio.Configuration.Voice
{
	public interface IExtendedVoiceDeliveryReportProperties
	{
		string? AccountSid { get; set; }
		string? AnsweredBy { get; set; }
		string? ApiVersion { get; set; }
		string? CallbackSource { get; set; }
		TimeSpan? CallDuration { get; set; }
		string? Called { get; set; }
		string? CalledCity { get; set; }
		string? CalledCountry { get; set; }
		string? CalledState { get; set; }
		string? CalledZip { get; set; }
		string? Caller { get; set; }
		string? CallerCity { get; set; }
		string? CallerCountry { get; set; }
		string? CallerState { get; set; }
		string? CallerZip { get; set; }
		string? CallSid { get; set; }
		CallStatus? CallStatus { get; set; }
		Direction? Direction { get; set; }
		TimeSpan? Duration { get; set; }
		string? From { get; set; }
		string? FromCity { get; set; }
		string? FromCountry { get; set; }
		string? FromState { get; set; }
		string? FromZip { get; set; }
		string? IdempotencyId { get; set; }
		int? SequenceNumber { get; set; }
		string? Signature { get; set; }
		string? SipResponseCode { get; set; }
		DateTimeOffset? Timestamp { get; set; }
		string? To { get; set; }
		string? ToCity { get; set; }
		string? ToCountry { get; set; }
		string? ToState { get; set; }
		string? ToZip { get; set; }
	}
}