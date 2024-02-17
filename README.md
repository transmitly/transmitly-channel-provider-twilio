# Transmitly.ChannelProvider.Twilio

A [Transmitly](https://github.com/transmitly/transmitly) channel provider that enables sending SMS communications with Twilio

### Getting started

To use the Twilio channel provider, first install the [NuGet package](https://github.com/transmitly/transmitly-channel-provider-twilio):

```shell
dotnet add package Transmitly.ChannelProvider.Twilio
```

Then add the channel provider using `AddTwilioSupport()`:

```csharp
using Transmitly;
...
var communicationClient = new CommunicationsClientBuilder()
	.AddTwilioSupport(options =>
	{
		options.AccountSid = "ACCOUNT_SID";
		options.AuthToken  = "AUTH_TOKEN";
	})
```
* See the [Transmitly](https://github.com/transmitly/transmitly) project for more details on what a channel provider is and how it can be configured.


<picture>
  <source media="(prefers-color-scheme: dark)" srcset="https://github.com/transmitly/transmitly/assets/3877248/524f26c8-f670-4dfa-be78-badda0f48bfb">
  <img alt="an open-source project sponsored by CiLabs of Code Impressions, LLC" src="https://github.com/transmitly/transmitly/assets/3877248/34239edd-234d-4bee-9352-49d781716364" width="350" align="right">
</picture> 

---------------------------------------------------

_Copyright &copy; Code Impressions, LLC - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
