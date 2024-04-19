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
using System.Runtime.Serialization;

namespace Transmitly.ChannelProvider.TwilioClient.Sms
{
	public enum SmsStatus
	{
		Unknown,
		[EnumMember(Value = "queued")]
		Queued,
		[EnumMember(Value = "sending")]
		Sending,
		[EnumMember(Value = "sent")]
		Sent,
		[EnumMember(Value = "failed")]
		Failed,
		[EnumMember(Value = "delivered")]
		Delivered,
		[EnumMember(Value = "undelivered")]
		Undelivered,
		[EnumMember(Value = "receiving")]
		Receiving,
		[EnumMember(Value = "received")]
		Received,
		[EnumMember(Value = "accepted")]
		Accepted,
		[EnumMember(Value = "scheduled")]
		Scheduled,
		[EnumMember(Value = "read")]
		Read,
		[EnumMember(Value = "partially_delivered")]
		PartiallyDelivered,
		[EnumMember(Value = "canceled")]
		Canceled
	}
}