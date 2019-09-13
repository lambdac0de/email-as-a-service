# email-as-a-service
A simple .net core app to expose email service as a web service (http)

### Why did I make this? email is simple as it is!

This tiny app may not make much sense to some, but you'd be surprised how many system administrators and developers do not put notification or alerting functionalitis in their scripts. Moreover, some people don't have details of their email infrastructure, preventing them from readily setting up alerting. Also, it's unnecessary for every developer to recreate a function just to send alerting every time they need to!<br><br>
This app is intended to give system administrators and developers a readily useable service for adding email-based notifications and alerting for their scripts and apps.

### Initial setup
1. Fill in `this.server` in file `Models\Smtp.cs` with your SMTP smarthost
2. (Optional) Fill-in the default email parameters in the constructor of class `Alert`  in file `Models\Alert.cs`
3. Take note of some tokens to use in class `ValidTOkens` file `Models\Smtp.cs`. This is completely arbitrary and you can choose to recreate your own tokens.

### Parameters

|Parameter|Mandatory|Description|
| ------- | ------- | --------- |
|sender|No|Email address and alias of the sender of the notification|
|recipient|Yes|Comma-separated list of email addresses to receive the notification|
|cc|No|Comma-separated list of email addresses to be in the ‘CC’ of the notification|
|subject|No|The email subject|
|body|Yes|The email body|
|html|No|Flag to set if the body should be displayed in html. Valid values are 1 (set to html) and 0 (plain text). Default is 0|
|priority|No|Flag to set to determine the priority of the email. Valid values are 1 (high priority) and 0 (normal). Default is 0|
|token|Yes|This is just a fake authentication token just to minimize possible abuse in case the Uri gets unintentionally discovered. This is still required though|

### Usage

```
GET /
https://<your_url>/?parameter1=value1&parameter2=value2&parameterN=valueN

# Example in PowerShell
Invoke-Webrequest -Uri “https://<your_url>?sender=test@nodomain.com&recipient=me@nodomain.com&cc=me@personaldomain.com&subject=eaas is cool!&body=eaas is really cool!&token=XXXXXX” -UseBasicParsing
```
```
POST /
https://<your_url>
{
  "sender": "<email_address_of_sender>",
  "cc": "<email_addresses_in_cc_filed>",
  "recipient": "<recipients_of_notification>", 
  "subject": "<email_subject>",
  "body": "<email_body>",
  "html": "<is_body_html?>",
  "priority": "<is_email_high_priority?>",
  "token": "<the_fake_auth_token>"
 }
 
 # Example in PowerShell
 $body = @{'sender'='monitor@nodomain.com';
           'recipient'='myteam@nodomain.com';
           'subject'='Your script failed somewhere';
           'body'='Your script failed somewhere in this part of the code';
           'token'='XXXXXXX';
           } | ConvertTo-Json -Compress

Invoke-WebRequest -Uri 'https://<your_url>' -Method Post  -UseBasicParsing -Body $body -ContentType 'text/plain' # explicitly setting content type to text/plain is important!
```

### How do I setup the app?

Follow stardard instructions in deploying .net core apps. See some examples below:<br>
- [Deploy in Windows/IIS](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/index?view=aspnetcore-2.2)<br>
- [Deploy in Linux/nginx](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-2.2)<br>
