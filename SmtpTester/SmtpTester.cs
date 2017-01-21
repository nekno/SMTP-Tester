using System;
using System.Configuration;
using System.Drawing;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace SmtpTester
{
	public partial class SmtpTester : Form
	{
		SmtpTesterLib smtpTester = new SmtpTesterLib();
		bool IsErrorCondition { get; set; }
		
		public SmtpTester()
		{
			InitializeComponent();

			smtpTester.LogOutput += AppendOutput;
			smtpTester.ResponseReceived += SetErrorDisplay;
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

				await smtpTester.TestConnection(server, port, chkEnableTLS.Checked);
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
				string to = txtTo.Text.Trim();
				string cc = txtCC.Text.Trim();
				string bcc = txtBCC.Text.Trim();

				ClearErrorDisplay();
				ShowLoading(true);

				await smtpTester.TestEmail(to, cc, bcc, txtSubject.Text, txtBody.Text);

				AppendOutput(String.Format("Message sent to {0} {1} {2}.", to, cc, bcc));
				SetErrorDisplay(false);
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

		protected string GetErrorString(Exception ex)
		{
			StringBuilder sbErrorString = new StringBuilder();

			sbErrorString.AppendLine(ex.Message);
			
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
