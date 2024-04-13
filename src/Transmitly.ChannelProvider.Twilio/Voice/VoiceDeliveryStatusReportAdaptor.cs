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
using System.Collections.Generic;
using System.Threading.Tasks;
using Transmitly.Delivery;

namespace Transmitly.ChannelProvider.Twilio.Voice
{
	sealed class VoiceDeliveryStatusReportAdaptor : IChannelProviderDeliveryReportRequestAdaptor
	{
		//todo: request validation, idempotent id
		//https://www.twilio.com/docs/usage/webhooks/webhooks-security
		public Task<IReadOnlyCollection<DeliveryReport>?> AdaptAsync(IRequestAdaptorContext adaptorContext)
		{
			if (!ShouldAdapt(adaptorContext))
				return Task.FromResult<IReadOnlyCollection<DeliveryReport>?>(null);

			var voiceReport = new VoiceStatusReport(adaptorContext);

			var ret = new VoiceDeliveryReport(
					DeliveryReport.Event.StatusChanged(),
					Id.Channel.Voice(),
					Id.ChannelProvider.Twilio(),
					adaptorContext.PipelineName,
					voiceReport.MessageSid,
					Util.ToDispatchStatus(voiceReport.MessageStatus),
					null,
					null,
					null
				).ApplyExtendedProperties(voiceReport);

			return Task.FromResult<IReadOnlyCollection<DeliveryReport>?>(new List<DeliveryReport>() { ret }.AsReadOnly());
		}

		private static bool ShouldAdapt(IRequestAdaptorContext adaptorContext)
		{
			return
				(adaptorContext.GetValue(DeliveryUtil.ChannelIdKey)?.Equals(Id.Channel.Voice(), StringComparison.InvariantCultureIgnoreCase) ?? false) &&
				(adaptorContext.GetValue(DeliveryUtil.ChannelProviderIdKey)?.StartsWith(Id.ChannelProvider.Twilio(), StringComparison.InvariantCultureIgnoreCase) ?? false);
		}
	}
}
