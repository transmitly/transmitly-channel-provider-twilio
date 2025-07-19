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
using Transmitly.Delivery;
using Transmitly.Util;

namespace Transmitly.ChannelProvider.Twilio.Configuration.Voice
{
	public sealed class VoiceStatusReport : StatusReport
	{
		public VoiceStatusReport(IRequestAdaptorContext adaptorContext) : base(adaptorContext)
		{
			Guard.AgainstNull(adaptorContext);

			Called = adaptorContext.GetValue("Called");
			ToState = adaptorContext.GetValue("ToState");
			CallerCountry = adaptorContext.GetValue("CallerCountry");
			CallbackSource = adaptorContext.GetValue("CallbackSource");
			SipResponseCode = adaptorContext.GetValue("SipResponseCode");
			CallerState = adaptorContext.GetValue("CallerState");
			ToZip = adaptorContext.GetValue("ToZip");
			CallSid = adaptorContext.GetValue("CallSid");
			CallerZip = adaptorContext.GetValue("CallerZip");
			ToCountry = adaptorContext.GetValue("ToCountry");
			CalledZip = adaptorContext.GetValue("CalledZip");
			CalledCity = adaptorContext.GetValue("CalledCity");
			CalledCountry = adaptorContext.GetValue("CalledCountry");
			CallerCity = adaptorContext.GetValue("CallerCity");
			ToCity = adaptorContext.GetValue("ToCity");
			FromCountry = adaptorContext.GetValue("FromCountry");
			CalledState = adaptorContext.GetValue("CalledState");
			FromZip = adaptorContext.GetValue("FromZip");
			AnsweredBy = adaptorContext.GetValue("AnsweredBy");
			FromState = adaptorContext.GetValue("FromState");

			if (DateTime.TryParse(adaptorContext.GetValue("Timestamp"), out var timestamp))
				Timestamp = timestamp;

			if (int.TryParse(adaptorContext.GetValue("SequenceNumber"), out var sequenceNumber))
				SequenceNumber = sequenceNumber;

			if (int.TryParse(adaptorContext.GetValue("Duration"), out var duration))
				Duration = TimeSpan.FromMinutes(duration);

			if (int.TryParse(adaptorContext.GetValue("CallDuration"), out var callDuration))
				CallDuration = TimeSpan.FromSeconds(callDuration);

			var callStatus = EnumUtil.ToEnum<CallStatus>(adaptorContext.GetValue("CallStatus"));
			if (callStatus != default)
				CallStatus = callStatus;

			var direction = EnumUtil.ToEnum<Direction>(adaptorContext.GetValue("Direction"));
			if (direction != default)
				Direction = direction;
		}

		public string? Called { get; }

		public string? ToState { get; }

		public string? CallerCountry { get; }

		public Direction? Direction { get; }

		public DateTimeOffset? Timestamp { get; }

		public string? CallbackSource { get; }

		public string? SipResponseCode { get; }

		public string? CallerState { get; }

		public string? ToZip { get; }

		public int? SequenceNumber { get; }

		public string? CallSid { get; }

		public string? CallerZip { get; }

		public string? ToCountry { get; }

		public string? CalledZip { get; }

		public string? CalledCity { get; }

		public CallStatus? CallStatus { get; }

		/// <summary>
		/// The duration of the just-completed call; 
		/// calls are billed by the minute. 
		/// Only present in the completed event.
		/// </summary>
		public TimeSpan? Duration { get; }

		/// <summary>
		/// The duration of the just-completed call. 
		/// Only present in the completed event
		/// </summary>
		public TimeSpan? CallDuration { get; }

		public string? CalledCountry { get; }

		public string? CallerCity { get; }

		public string? ToCity { get; }

		public string? FromCountry { get; }

		public string? Caller { get; }

		public string? FromCity { get; }

		public string? CalledState { get; }

		public string? FromZip { get; }

		public string? AnsweredBy { get; }

		public string? FromState { get; }

	}
}
