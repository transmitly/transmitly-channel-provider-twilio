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

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Transmitly.ChannelProvider.Twilio
{
	internal sealed class TwilioSmsChannelProviderClient : ChannelProviderClient<ISms>
	{
		public override async Task<IReadOnlyCollection<IDispatchResult?>> DispatchAsync(ISms sms, IDispatchCommunicationContext communicationContext, CancellationToken cancellationToken)
		{
			Guard.AgainstNull(sms);
			Guard.AgainstNull(communicationContext);
			Guard.AgainstNull(sms.From);
			var recipients = communicationContext.RecipientAudiences.SelectMany(a => a.Addresses.Select(addr => new PhoneNumber(addr.Value))).ToList();
			var results = new List<IDispatchResult>(recipients.Count);

			foreach (var recipient in recipients)
			{
				var message = await MessageResource.CreateAsync(from: sms.From.Value, body: sms.Body, to: recipient);
				results.Add(new TwilioDispatchResult(message.Sid));
			}

			return results;
		}
	}
}
