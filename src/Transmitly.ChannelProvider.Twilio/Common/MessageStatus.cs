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

namespace Transmitly.ChannelProvider.TwilioClient
{
	/// <summary>
	/// A Message resource's <see cref="StatusReport.MessageStatus">Status</see>
	/// <para>
	/// As messages can be either outbound or inbound, each status description explicitly indicates to which message direction the status applies.
	/// </para>
	/// </summary>
	public enum MessageStatus
	{
		/// <summary>
		/// Transmitly specific status message for unhandled and unknown statuses.
		/// </summary>
		unknown,
		/// <summary>
		/// The API request to send an outbound message was successful and the message is queued to be sent out by a 
		/// specific <em>From</em> sender. For messages sent without a 
		/// <a href="https://www.twilio.com/docs/messaging/services">Messaging Service</a> this 
		/// is the initial <em>Status</em> value of the Message resource.
		/// </summary>
		queued,
		/// <summary>
		/// Twilio is in the process of dispatching the outbound message to the nearest upstream carrier in the network.
		/// </summary>
		sending,
		/// <summary>
		/// The nearest upstream carrier accepted the outbound message.
		/// </summary>
		sent,
		/// <summary>
		/// The outbound message failed to send. This can happen for 
		/// <a href="https://www.twilio.com/docs/messaging/guides/debugging-tools#error-codes">various reasons</a>
		/// including queue overflows, Account suspensions and media errors. 
		/// Twilio does not charge you for failed messages.
		/// </summary>
		failed,
		/// <summary>
		/// Twilio has received confirmation of outbound message delivery from the upstream carrier,
		/// and, where available, the destination handset.
		/// </summary>
		delivered,
		/// <summary>
		/// Twilio received a delivery receipt indicating that the outbound message was not delivered. 
		/// This can happen for <a href="https://www.twilio.com/docs/messaging/guides/debugging-tools#error-codes">many reasons</a> 
		/// including carrier content filtering and the availability of the destination handset.
		/// </summary>
		undelivered,
		/// <summary>
		/// The inbound message was received by Twilio and is currently being processed.
		/// </summary>
		receiving,
		/// <summary>
		/// The inbound message was received and processing is complete.
		/// </summary>
		received,
		/// <summary>
		/// Messaging Service only] Twilio has received your API request to immediatedly send an outbound message with a 
		/// <a href="https://www.twilio.com/docs/messaging/services">Messaging Service</a>.
		/// If you did not provide a specific From sender in the service's Sender Pool to use, the service is dynamically selecting a 
		/// From sender. For unscheduled messages to be sent with a Messaging Service, this is the initial Status value of the Message resource.
		/// </summary>
		accepted,
		/// <summary>
		/// [Messaging Service only] The Message resource is scheduled to be sent with a 
		/// <a href="https://www.twilio.com/docs/messaging/services">Messaging Service</a>. 
		/// If you <a href="https://www.twilio.com/docs/messaging/features/message-scheduling">schedule a message</a> with a Messaging Service, this is the initial Status value of the Message resource.
		/// </summary>
		scheduled,
		/// <summary>
		/// WhatsApp only: The recipient opened the outbound message. Recipient must have read receipts enabled.
		/// </summary>
		read,
		[Obsolete]
		partially_delivered,
		/// <summary>
		/// [Messaging Service only] The message scheduled with a 
		/// <a href="https://www.twilio.com/docs/messaging/services">Messaging Service</a> has been canceled.
		/// </summary>
		canceled,
		/// <summary>
		/// Completed
		/// </summary>
		completed



	}
}
