using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmtpTester
{
	public partial class SmtpTester : Form
	{
		protected bool IsErrorCondition { get; set; }
		
		public SmtpTester()
		{
			InitializeComponent();
		}

		protected void SmtpTester_Load(object sender, EventArgs e)
		{
			using (var client = new SmtpClient())
			using (var msg = new MailMessage())
			{
				var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
				var mailSettings = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

				txtServer.Text = client.Host;
				txtPort.Text = client.Port.ToString();
				chkEnableTLS.Checked = client.EnableSsl;
				txtUsername.Text = (mailSettings != null) ? mailSettings.Smtp.Network.UserName : String.Empty;
				txtPassword.Text = (mailSettings != null) ? mailSettings.Smtp.Network.Password : String.Empty;
				chkDefaultCredentials.Checked = client.UseDefaultCredentials;
				txtFrom.Text = (msg.From != null) ? msg.From.Address : String.Empty;
			}

			// Needed for showing remote SSL certificate errors with SmtpClient. TcpClient is passed its own callback handler.
			ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateRemoteCertificate);
		}

		protected async void btnTestConnection_Click(object sender, EventArgs e)
		{
			try
			{
				var server = txtServer.Text;
				var port = Convert.ToInt32(txtPort.Text);

				AppendOutput("\nInitiating connection...");
				ClearErrorDisplay();
				ShowLoading(true);

				using (var client = new TcpClient())
				{
					await client.ConnectAsync(server, port);

					using (var stream = client.GetStream())
					using (var reader = new StreamReader(stream))
					using (var writer = new StreamWriter(stream) { AutoFlush = true })
					{
						// Log connection response
						string response = reader.ReadLine();
						AppendOutput(response);
						ParseSmtpResponse(response);

						await SendSmtpRequest("EHLO localhost", writer, reader);

						// If your SMTP server doesn't support SSL you can work directly with the underlying stream
						if (chkEnableTLS.Checked)
						{
							await SendSmtpRequest("STARTTLS", writer, reader);

							using (var sslStream = new SslStream(stream, true, ValidateRemoteCertificate, null, EncryptionPolicy.RequireEncryption))
							{
								// Disable SSLv3 to avoid POODLE
								// Should disable TLS 1.0 to avoid BEAST, but need it to support servers prior to Windows Server 2008 R2 and OpenSSL 1.0.1
								await sslStream.AuthenticateAsClientAsync(server, null, SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12, true);
								AppendOutput("Authentication negotiated:");
								AppendOutput(String.Format("SSL protocol: {0}", sslStream.SslProtocol));
								AppendOutput(String.Format("Cipher algo: {0}", sslStream.CipherAlgorithm));
								AppendOutput(String.Format("Cipher strength: {0}", sslStream.CipherStrength));
								AppendOutput(String.Format("Hash algo: {0}", sslStream.HashAlgorithm));
								AppendOutput(String.Format("Hash strength: {0}", sslStream.HashStrength));
								AppendOutput(String.Format("Is authenticated: {0}", sslStream.IsAuthenticated));
								AppendOutput(String.Format("Is encrypted: {0}", sslStream.IsEncrypted));
								AppendOutput(String.Format("Is mutually authenticated: {0}", sslStream.IsMutuallyAuthenticated));
								AppendOutput(String.Format("Is signed: {0}", sslStream.IsSigned));
								AppendOutput(String.Format("Key exchange algo: {0}", sslStream.KeyExchangeAlgorithm));
								AppendOutput(String.Format("Key exchange strength: {0}", sslStream.KeyExchangeStrength));
								AppendOutput(String.Format("Remote cert: {0}", sslStream.RemoteCertificate.ToString(true)));

								using (var sslReader = new StreamReader(sslStream))
								using (var sslWriter = new StreamWriter(sslStream) { AutoFlush = true })
								{
									await SendSmtpRequest("QUIT", sslWriter, sslReader);
								}
							}
						}
						else
						{
							await SendSmtpRequest("QUIT", writer, reader);
						}
					}
				}
			}
			catch (Exception ex)
			{
				AppendOutput(GetErrorString(ex));
				SetErrorDisplay(true);
			}
			finally
			{
				ShowLoading(false);
			}
		}

		protected async void btnTestEmail_Click(object sender, EventArgs e)
		{
			try
			{
				ClearErrorDisplay();
				ShowLoading(true);

				using (var client = new SmtpClient())
				using (var msg = new MailMessage())
				{
					string to = txtTo.Text.Trim();
					string cc = txtCC.Text.Trim();
					string bcc = txtBCC.Text.Trim();

					if (to.Length > 0)
					{
						msg.To.Add(to);
					}
					if (cc.Length > 0)
					{
						msg.CC.Add(cc);
					}
					if (bcc.Length > 0)
					{
						msg.Bcc.Add(bcc);
					}
					msg.Subject = txtSubject.Text;
					msg.Body = txtBody.Text;

					await client.SendMailAsync(msg);
					AppendOutput(String.Format("Message sent to {0} {1} {2}.", to, cc, bcc));
					SetErrorDisplay(false);
				}
			}
			catch (Exception ex)
			{
				AppendOutput(GetErrorString(ex));
				SetErrorDisplay(true);
			}
			finally
			{
				ShowLoading(false);
			}
		}

		#region Utility Methods
		
		protected async Task SendSmtpRequest(string msg, StreamWriter writer, StreamReader reader)
		{
			AppendOutput(msg);
			await writer.WriteLineAsync(msg);
			string response = await reader.ReadLineAsync();
			AppendOutput(response);
			ParseSmtpResponse(response);

			// In SMTP, responses have 3 digits then a space or dash. A space indicates end of response.
			// See http://tools.ietf.org/html/rfc821
			while (response.Length > 3 && response[3] != ' ')
			{
				response = await reader.ReadLineAsync();
				AppendOutput(response);
				ParseSmtpResponse(response);
			}
		}

		protected void ParseSmtpResponse(string response)
		{
			bool isError = false;
			
			if (response == null || !(response.StartsWith("2") || response.StartsWith("3")))
			{
				isError = true;
			}

			SetErrorDisplay(isError);
		}

		protected bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
		{
			if (policyErrors != SslPolicyErrors.None)
			{
				SetErrorDisplay(true);
				AppendOutput(String.Format("Certificate policy errors: {0}", policyErrors));
				foreach (var chainElement in chain.ChainElements)
				{
					AppendOutput(String.Format("Certificate chain cert: {0}", chainElement.Certificate.ToString(true)));
					AppendOutput(String.Format("Certificate chain info: {0}", chainElement.Information));
					foreach (var certStatus in chainElement.ChainElementStatus)
					{
						AppendOutput(String.Format("Certificate chain status: {0}", certStatus.StatusInformation));
					}
				}
			}

			return true; // allow errors to see what other errors arise, otherwise return (policyErrors == SslPolicyErrors.None);
		}

		protected string GetErrorString(Exception ex)
		{
			StringBuilder sbErrorString = new StringBuilder();

			sbErrorString.AppendLine(ex.Message);
			sbErrorString.AppendLine(ex.StackTrace);
			
			if (ex.InnerException != null)
			{
				sbErrorString.AppendLine(GetErrorString(ex.InnerException));
			}

			return sbErrorString.ToString();
		}

		#endregion Utility Methods

		#region UI Methods

		protected delegate void AppendOutputDelegate(string text);
		protected void AppendOutput(string text)
		{
			if (txtOutput.InvokeRequired)
			{
				txtOutput.Invoke(new AppendOutputDelegate(AppendOutput), text);
				return;
			}

			txtOutput.AppendText(String.Concat(text, System.Environment.NewLine));
		}

		protected delegate void SetErrorDisplayDelegate(bool isError);
		protected void SetErrorDisplay(bool isError)
		{
			if (txtOutput.InvokeRequired)
			{
				txtOutput.Invoke(new SetErrorDisplayDelegate(SetErrorDisplay), isError);
				return;
			}

			IsErrorCondition |= isError;
			txtOutput.BackColor = IsErrorCondition ? Color.Pink : Color.LightGreen;
		}

		protected void ClearErrorDisplay()
		{
			if (txtOutput.InvokeRequired)
			{
				txtOutput.Invoke(new MethodInvoker(ClearErrorDisplay));
				return;
			}

			IsErrorCondition = false;
			txtOutput.BackColor = System.Drawing.SystemColors.Window;
		}

		protected delegate void ShowLoadingDelegate(bool show);
		protected void ShowLoading(bool show)
		{
			if (InvokeRequired)
			{
				Invoke(new ShowLoadingDelegate(ShowLoading), show);
				return;
			}

			pbLoading.Visible = show;

			foreach (Control ctrl in Controls)
			{
				if (ctrl is Button)
				{
					ctrl.Enabled = !show;
				}
			}
		}
		#endregion UI Methods
	}
}
