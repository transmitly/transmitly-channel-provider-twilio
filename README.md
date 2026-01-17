# Transmitly.ChannelProvider.Twilio

A [Transmitly](https://github.com/transmitly/transmitly) channel provider that enables sending SMS and Voice communications with Twilio

### Getting started

To use the Twilio channel provider, first install the [NuGet package](https://github.com/transmitly/transmitly-channel-provider-twilio):

```shell
dotnet add package Transmitly.ChannelProvider.Twilio
```

Then add the channel provider using `AddTwilioSupport()`:

```csharp
using Transmitly;
//...
var communicationClient = new CommunicationsClientBuilder()
.AddTwilioSupport(options =>
{
	options.AccountSid = "ACCOUNT_SID";
	options.AuthToken  = "AUTH_TOKEN";
})
//Pipelines are the heart of Transmitly. Pipelines allow you to define your communications
//as a domain action. This allows your domain code to stay agnostic to the details of how you
//may send out a transactional communication.
.AddPipeline("first-pipeline", pipeline =>
{
	//AddSms is a Channel that is core to the Transmitly library. 
	//AsIdentityAddress() is also a convience method that helps us create an audience address
	//Audience addresses can be anything, email, phone, or even a device/app Id for push notifications!
	pipeline.AddSms(sms=>{
		//Transmitly is a bit different. All of our communication content is configured by templates.
		//Out of the box, we have static or string templates, file and even embedded template support.
		//There are multiple types of templates to get you started. You can even create templates 
		//specific to certain cultures!
		sms.Message.AddStringTemplate("Hey, check out Transmit.ly to manage your app communications!");
	});
})
.BuildClient();

//Dispatch (send) the transactional sms to our friend Joe (888-555-1234) using our configured Twilio account and our "first-pipeline" pipeline.
var result = await communicationsClient.DispatchAsync("first-pipeline", "888-555-1234".AsIdentityAddress(), new { });
```
* See the [Transmitly](https://github.com/transmitly/transmitly) project for more details on what a channel provider is and how it can be configured.


---
_Copyright © Code Impressions, LLC.  This open-source project is sponsored and maintained by Code Impressions
and is licensed under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html)._
