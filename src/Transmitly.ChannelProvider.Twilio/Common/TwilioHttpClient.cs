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

using System.Net.Http;
using System.Threading.Tasks;
using Twilio.Clients;

namespace Transmitly.ChannelProvider.TwilioClient
{
	sealed class TwilioHttpClient(HttpClient client, TwilioClientOptions twilioClientOptions) : ITwilioRestClient
	{
		public string AccountSid => _client.AccountSid;

		public string Region => _client.Region;

		private readonly TwilioRestClient _client = new(
				twilioClientOptions.AccountSid,
				twilioClientOptions.AuthToken,
				twilioClientOptions.AccountSid,
				twilioClientOptions.Region,
				new Twilio.Http.SystemNetHttpClient(client),
				twilioClientOptions.Edge
			);

		public Twilio.Http.HttpClient HttpClient => _client.HttpClient;

		public Twilio.Http.Response Request(Twilio.Http.Request request)
		{
			return _client.Request(request);
		}

		public Task<Twilio.Http.Response> RequestAsync(Twilio.Http.Request request)
		{
			return _client.RequestAsync(request);
		}
	}
}