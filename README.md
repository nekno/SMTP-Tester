SMTP Tester
===========

A simple GUI client for testing SMTP connections with or without TLS/SSL, mail delivery, and the .Net `system.net/mailSettings` configuration section for use with the built-in `SmtpClient` in .Net. 

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
- Logs all output to the GUI.
- Uses the Task Parallel Library `async`/`await` constructs for efficient, asynchronous I/O without blocking the UI.

# Instructions
- Configure all SMTP connection settings in the [`system.net/mailSettings`](http://msdn.microsoft.com/en-us/library/ms164240%28v=vs.110%29.aspx) configuration section of the `App.config` file before building. At runtime, the settings should be configured in the `SMTPTester.exe.config` file. Settings that are configured in the `App.config` file are grayed out in the UI.

##### Testing SMTP Connections
- To test an SMTP connection without sending mail, use the `Test Connection` button.

##### Testing Mail Delivery 
- To test sending an email, fill out the available form fields and use the `Send Email` button.
