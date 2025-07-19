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

using Transmitly.Delivery;
using Transmitly.Util;

namespace Transmitly.ChannelProvider.Twilio.Configuration
{
	public class StatusReport
	{
		public StatusReport(IRequestAdaptorContext adaptorContext)
		{
			Guard.AgainstNull(adaptorContext);

			AccountSid = adaptorContext.GetValue("AccountSid");
			ApiVersion = adaptorContext.GetValue("ApiVersion");
			To = adaptorContext.GetValue("To");
			From = adaptorContext.GetValue("From");
			Signature = adaptorContext.GetValue("x-twilio-signature");
			IdempotencyId = adaptorContext.GetValue("i-twilio-idempotency-token");
			HomeRegion = adaptorContext.GetValue("x-home-region");
		}

		public string? AccountSid { get; protected set; }
		public string? ApiVersion { get; protected set; }
		public string? To { get; protected set; }
		public string? From { get; protected set; }
		public string? IdempotencyId { get; protected set; }
		public string? Signature { get; protected set; }
		public string? HomeRegion { get; protected set; }
	}
}