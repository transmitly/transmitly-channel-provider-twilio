# Transmitly.ChannelProvider.Twilio

`Transmitly.ChannelProvider.Twilio` is the convenience package for using [Transmitly](https://github.com/transmitly/transmitly) with [Twilio](https://www.twilio.com/) for SMS and voice calls.

This is the package most applications should install. It wires together:

- `Transmitly.ChannelProvider.Twilio.Configuration`
- `Transmitly.ChannelProvider.Twilio.Sdk`

Supported channels:

- `Sms`
- `Voice`

## Install

```shell
dotnet add package Transmitly.ChannelProvider.Twilio
```

## Quick Start

```csharp
using Transmitly;

ICommunicationsClient client = new CommunicationsClientBuilder()
	.AddTwilioSupport(options =>
	{
		options.AccountSid = "your-account-sid";
		options.AuthToken = "your-auth-token";
	})
	.AddPipeline("sms-alert", pipeline =>
	{
		pipeline.AddSms("+15550001111".AsIdentityAddress(), sms =>
		{
			sms.Message.AddStringTemplate("There is an update on your account.");
		});
	})
	.BuildClient();

var result = await client.DispatchAsync(
	"sms-alert",
	"+15551234567".AsIdentityAddress(),
	new { });
```

## Configuration

`AddTwilioSupport(options => ...)` accepts `TwilioClientOptions`.

Common settings:

- `AccountSid`: your Twilio account SID.
- `AuthToken`: your Twilio auth token.
- `WebProxy`: optional outbound proxy.
- `Edge`: defaults to `ashburn`.
- `Region`: defaults to `us1`.

## Twilio-Specific Channel Features

This package registers Twilio extensions for both SMS and voice channels.

SMS features are available through `sms.Twilio()`:

- `MessagingServiceSid`
- `StatusCallbackUrl`
- `StatusCallbackUrlResolver`
- `StatusCallbackMethod`

Voice features are available through `voice.Twilio()`:

- `StatusCallbackUrl` and `StatusCallbackUrlResolver`
- `Url` and `UrlResolver` for TwiML hosting
- `Timeout`
- `MachineDetection`
- `OnStoreMessageForRetrievalAsync`

## Voice Calls And TwiML

The voice dispatcher can send inline TwiML when the generated TwiML is short enough. If the generated TwiML is too large, you must provide a `Url` or `UrlResolver` so Twilio can retrieve the call instructions.

## Delivery Reports

This package registers Twilio SMS and voice delivery-report request adaptors and the Twilio delivery-report extended properties adaptor, which makes `report.Twilio()` available for provider-specific webhook data.

## Related Packages

- [Transmitly](https://github.com/transmitly/transmitly)
- [Transmitly.ChannelProvider.Twilio.Configuration](https://github.com/transmitly/transmitly-channel-provider-twilio-configuration)
- [Transmitly.ChannelProvider.Twilio.Sdk](https://github.com/transmitly/transmitly-channel-provider-twilio-sdk)

---
_Copyright (c) Code Impressions, LLC. This open-source project is sponsored and maintained by Code Impressions and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
