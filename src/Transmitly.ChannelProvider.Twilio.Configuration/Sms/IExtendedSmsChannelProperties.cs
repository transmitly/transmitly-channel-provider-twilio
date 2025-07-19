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
using Transmitly.Channel.Configuration;

namespace Transmitly.ChannelProvider.Twilio.Configuration.Sms
{
	/// <summary>
	/// Twilio specific properties for the sms channel.
	/// </summary>
	public interface IExtendedSmsChannelProperties
	{
		IExtendedSmsChannelProperties Adapt(IChannel<ISms> sms);

		/// <summary>
		/// The SID of the Messaging Service you want to associate with the message.
		/// <seealso href="https://www.twilio.com/docs/messaging/services">Documentation</seealso>
		/// </summary>
		string? MessagingServiceSid { get; set; }
		/// <summary>
		/// HTTP method to use to send status information to your application.
		/// </summary>
		string? StatusCallbackMethod { get; set; }
		/// <summary>
		/// The URL we should call to send status information to your application.
		/// </summary>
		string? StatusCallbackUrl { get; set; }
		/// <summary>
		/// A resolver that will return the URL we should call to send status information to your application.
		/// </summary>
		Func<IDispatchCommunicationContext, Task<string?>>? StatusCallbackUrlResolver { get; set; }
	}
}