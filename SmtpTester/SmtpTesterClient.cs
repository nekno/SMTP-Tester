using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace SmtpTester
{
	public delegate void LogOutputHandler(string message);
	public delegate void ResponseHandler(bool isError);

	public class SmtpTesterClient
	{
		public event LogOutputHandler LogOutput;
		public event ResponseHandler ResponseReceived;

		AggregateException ServerException { get; set; }

		public SmtpTesterClient() {
			// Needed for showing remote SSL certificate errors with SmtpClient. TcpClient is passed its own callback handler.
			ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
		}

		public async Task TestConnection(string server, int port, bool enableTls) {
			ClearErrors();

			using (var client = new TcpClient()) {
				await client.ConnectAsync(server, port);

				using (var stream = client.GetStream())
				using (var reader = new StreamReader(stream))
				using (var writer = new StreamWriter(stream) { AutoFlush = true }) {
					// Log connection response
					string response = reader.ReadLine();
					FireLogOutput(response);
					ParseSmtpResponse(response);

					await SendSmtpRequest("EHLO localhost", writer, reader);

					// If your SMTP server doesn't support SSL you can work directly with the underlying stream
					if (enableTls) {
						await SendSmtpRequest("STARTTLS", writer, reader);

						using (var sslStream = new SslStream(stream, true, ValidateRemoteCertificate, null, EncryptionPolicy.RequireEncryption)) {
							// Disable SSLv3 to avoid POODLE
							// Should disable TLS 1.0 to avoid BEAST, but need it to support servers prior to Windows Server 2008 R2 and OpenSSL 1.0.1
							await sslStream.AuthenticateAsClientAsync(server, null, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, true);
							FireLogOutput(String.Format("Authentication negotiated:{0}" + 
								"\tSSL protocol: {1}{0}" +
								"\tCipher algo: {2}{0}" +
								"\tCipher strength: {3}{0}" +
								"\tHash algo: {4}{0}" + 
								"\tHash strength: {5}{0}" + 
								"\tIs authenticated: {6}{0}" + 
								"\tIs encrypted: {7}{0}" + 
								"\tIs mutually authenticated: {8}{0}" + 
								"\tIs signed: {9}{0}" + 
								"\tKey exchange algo: {10}{0}" + 
								"\tKey exchange strength: {11}{0}" +
								"Remote cert: {0}{12}", 
								System.Environment.NewLine,
								sslStream.SslProtocol,
								sslStream.CipherAlgorithm,
								sslStream.CipherStrength,
								sslStream.HashAlgorithm,
								sslStream.HashStrength,
								sslStream.IsAuthenticated,
								sslStream.IsEncrypted,
								sslStream.IsMutuallyAuthenticated,
								sslStream.IsSigned,
								sslStream.KeyExchangeAlgorithm,
								sslStream.KeyExchangeStrength,
								sslStream.RemoteCertificate.ToString(true)
							));

							using (var sslReader = new StreamReader(sslStream))
							using (var sslWriter = new StreamWriter(sslStream) { AutoFlush = true }) {
								await SendSmtpRequest("QUIT", sslWriter, sslReader);
							}
						}
					} else {
						await SendSmtpRequest("QUIT", writer, reader);
					}
				}
			}

			if (ServerException.InnerExceptions.Count > 0) {
				throw ServerException;
			}
		}

		public async Task TestEmail(string to, string cc = null, string bcc = null, string subject = null, string body = null) {
			ClearErrors();

			using (var client = new SmtpClient())
			using (var msg = new MailMessage()) {
				if (!String.IsNullOrEmpty(to)) {
					msg.To.Add(to);
				}
				if (!String.IsNullOrEmpty(cc)) {
					msg.CC.Add(cc);
				}
				if (!String.IsNullOrEmpty(bcc)) {
					msg.Bcc.Add(bcc);
				}

				msg.Subject = subject;
				msg.Body = body;

				await client.SendMailAsync(msg);
			}
		}

		protected async Task SendSmtpRequest(string msg, StreamWriter writer, StreamReader reader) {
			FireLogOutput(msg);
			await writer.WriteLineAsync(msg);
			string response = await reader.ReadLineAsync();
			FireLogOutput(response);
			ParseSmtpResponse(response);

			// In SMTP, responses have 3 digits then a space or dash. A space indicates end of response.
			// See http://tools.ietf.org/html/rfc821
			while (response.Length > 3 && response[3] != ' ') {
				response = await reader.ReadLineAsync();
				FireLogOutput(response);
				ParseSmtpResponse(response);
			}
		}

		protected void ParseSmtpResponse(string response) {
			bool isError = false;

			if (response == null || !(response.StartsWith("2") || response.StartsWith("3"))) {
				isError = true;
				List<Exception> exceptions = new List<Exception>(ServerException.InnerExceptions);
				exceptions.Add(new Exception(response));
				ServerException = new AggregateException("The SMTP server responded with one or more errors.", exceptions);
			}

			FireResponseReceived(isError);
		}

		protected void ClearErrors() {
			ServerException = new AggregateException();
		}

		protected bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors) {
			if (policyErrors != SslPolicyErrors.None) {
				FireResponseReceived(true);
				FireLogOutput(String.Format("Certificate policy errors: {0}", policyErrors));
				foreach (var chainElement in chain.ChainElements) {
					FireLogOutput(String.Format("Certificate chain cert: {0}", chainElement.Certificate.ToString(true)));
					FireLogOutput(String.Format("Certificate chain info: {0}", chainElement.Information));
					foreach (var certStatus in chainElement.ChainElementStatus) {
						FireLogOutput(String.Format("Certificate chain status: {0}", certStatus.StatusInformation));
					}
				}
			}

			return true; // allow errors to see what other errors arise, otherwise return (policyErrors == SslPolicyErrors.None);
		}

		protected void FireLogOutput(string message) {
			if (LogOutput != null) {
				LogOutput(message);
			}
		}

		protected void FireResponseReceived(bool isError) {
			if (ResponseReceived != null) {
				ResponseReceived(isError);
			}
		}
	}
}
