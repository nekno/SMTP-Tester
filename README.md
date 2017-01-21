SMTP Tester
===========

A simple library and GUI client for testing SMTP connections (with or without TLS/SSL), mail delivery, and the .Net `system.net/mailSettings` configuration section for use with the built-in `SmtpClient` in .Net. 

The goal of SMTP Tester is to answer the question, "Why is my app not sending email?". The intent is to display issues with TCP connections, remote SSL certificates, and negotiating a secure TLS/SSL connection, which aren't usually displayed or surfaced by the `SmtpClient` in .Net. 

To accomplish this, the test client displays details about validation errors with remote SSL certs, but then swallows them and barrels forward to discover what issues might be encountered if the SSL issues are resolved. That way, hopefully most, if not all, of the issues can be revealed with a single test run.

You can compile the project as a WinForms app for the GUI, or change the output to a Class Library to consume the client from other code. You can subscribe to events for log output and errors that occur along the way.

Written in C#.

# Features
- Tests SMTP server connections with or without TLS/SSL.
  - Connects over TCP and sends an `EHLO` command to enumerate extended SMTP commands on the server.
  - If TLS/SSL is enabled, sends a `STARTTLS` command to test negotiating a secure connection.
  - Supports TLS 1.0, 1.1, and 1.2. SSLv3 and prior are disabled to mitigate POODLE and other attacks. TLS 1.0 should be disabled to mitigate BEAST attacks, but support in the wild is inconsistent, as TLS 1.1 and above require Windows Server 2008 R2 or OpenSSL 1.0.1 and above.
  - Shows negotiated authentication protocols and X.509 certificate details.
  - Shows SSL certificate validation errors for all certs in the chain, then proceeds with the connection to continue testing.
- Tests mail delivery by optionally sending a message.
- Tests the standard .Net-based `App.config` file settings in the `system.net/mailSettings` configuration section to verify the default behavior of `SmtpClient` as it would be used in another application.
- Logs all output with events, which appear in the GUI or your own app.
- Fires an event when a response is received to indicate whether the request succeeded or failed. Exceptions are accumulated and thrown at the end of the test within an `AggregateException`.
- Uses the Task Parallel Library `async`/`await` constructs for efficient, asynchronous I/O without blocking the UI.

# Instructions
Configure all SMTP connection settings in the [`system.net/mailSettings`](http://msdn.microsoft.com/en-us/library/ms164240%28v=vs.110%29.aspx) configuration section of the `App.config` file before building. At runtime, the settings should be configured in the `SMTPTester.exe.config` file before launching the app. Settings that are configured in the config file are grayed out in the UI.

##### Testing SMTP Connections
To test an SMTP connection without sending mail, use the `Test Connection` button.

##### Testing Mail Delivery 
To test sending an email, fill out the available form fields and use the `Send Email` button.

# Consuming the client as a library
The SMTP Tester GUI is essentially a guide on how to consume the `SmtpTesterClient` class. You can drop the class in your own code, load the app executable as a library, or change the project output to a Class Library and reference it in your own project.

Because a number of facets of the configuration are being tested (and not simply the credentials used for authentication), the test methods don't return a simple success/failure Boolean. Instead, exceptions are thrown when a connection fails. Unless an exception is thrown from within the .Net Framework that stops processing, the connection to the server is made, then the `EHLO`, `STARTTLS`, and `QUIT` commands are sent. 

##### Testing SMTP Connections
To test an SMTP connection without sending mail, call the `SmtpTesterClient.TestConnection(...)` method.

##### Testing Mail Delivery 
To test sending an email, call the `SmtpTesterClient.TestEmail(...)` method.

##### Events
- Whenever there is log output, a `LogOutput` event is fired. Subscribe an event handler if you want to display log output in your app.
- Whenever a response is received from the server, a `ResponseReceived` event is fired with a Boolean indicating whether it was a success or error code. Subscribe an event handler if you want to know immediately when an SMTP error response is received from the server (generally to change an indicator in the UI). An exception will not be thrown immediately, in order to continue the test. Once the test is complete, if an error was detected in any of the server responses, an `AggregateException` is thrown that contains all of the server response errors in the `InnerExceptions` property. If you just want to get all the errors and don't care about showing an immediate indicator in the UI, then don't bother subscribing to this event; just catch the `AggregateException` and examine the messages of the `InnerExceptions` to get the error responses.
  - Note: An SMTP response error is defined as a response code that doesn't start with `2` or `3` (essentially in the 200-300 range). Any exceptions thrown by the .Net Framework will be thrown normally, so your try/catch handling should expect to handle both scenarios. You can look for exceptions of type `SmtpResponseException` to distiniguish SMTP response errors from other exceptions that might also be thrown inside an `AggregateException` by the .Net Framework.
