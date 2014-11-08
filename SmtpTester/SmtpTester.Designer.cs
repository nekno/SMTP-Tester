namespace SmtpTester
{
	partial class SmtpTester
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmtpTester));
			this.lblServer = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.lblPort = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.chkEnableTLS = new System.Windows.Forms.CheckBox();
			this.lblFrom = new System.Windows.Forms.Label();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.lblTo = new System.Windows.Forms.Label();
			this.txtBCC = new System.Windows.Forms.TextBox();
			this.lblBCC = new System.Windows.Forms.Label();
			this.txtCC = new System.Windows.Forms.TextBox();
			this.lblCC = new System.Windows.Forms.Label();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.btnTestConnection = new System.Windows.Forms.Button();
			this.btnTestEmail = new System.Windows.Forms.Button();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.lblSubject = new System.Windows.Forms.Label();
			this.lblBody = new System.Windows.Forms.Label();
			this.txtBody = new System.Windows.Forms.TextBox();
			this.chkDefaultCredentials = new System.Windows.Forms.CheckBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.lblPassword = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lblUsername = new System.Windows.Forms.Label();
			this.pbLoading = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pbLoading)).BeginInit();
			this.SuspendLayout();
			// 
			// lblServer
			// 
			this.lblServer.AutoSize = true;
			this.lblServer.Location = new System.Drawing.Point(13, 13);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(41, 13);
			this.lblServer.TabIndex = 0;
			this.lblServer.Text = "Server:";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(77, 10);
			this.txtServer.Name = "txtServer";
			this.txtServer.ReadOnly = true;
			this.txtServer.Size = new System.Drawing.Size(140, 20);
			this.txtServer.TabIndex = 1;
			// 
			// lblPort
			// 
			this.lblPort.AutoSize = true;
			this.lblPort.Location = new System.Drawing.Point(227, 13);
			this.lblPort.Name = "lblPort";
			this.lblPort.Size = new System.Drawing.Size(29, 13);
			this.lblPort.TabIndex = 2;
			this.lblPort.Text = "Port:";
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(262, 10);
			this.txtPort.Name = "txtPort";
			this.txtPort.ReadOnly = true;
			this.txtPort.Size = new System.Drawing.Size(140, 20);
			this.txtPort.TabIndex = 3;
			// 
			// chkEnableTLS
			// 
			this.chkEnableTLS.AutoSize = true;
			this.chkEnableTLS.Enabled = false;
			this.chkEnableTLS.Location = new System.Drawing.Point(408, 12);
			this.chkEnableTLS.Name = "chkEnableTLS";
			this.chkEnableTLS.Size = new System.Drawing.Size(46, 17);
			this.chkEnableTLS.TabIndex = 4;
			this.chkEnableTLS.Text = "TLS";
			this.chkEnableTLS.UseVisualStyleBackColor = true;
			// 
			// lblFrom
			// 
			this.lblFrom.AutoSize = true;
			this.lblFrom.Location = new System.Drawing.Point(13, 109);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(33, 13);
			this.lblFrom.TabIndex = 10;
			this.lblFrom.Text = "From:";
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(77, 106);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.ReadOnly = true;
			this.txtFrom.Size = new System.Drawing.Size(140, 20);
			this.txtFrom.TabIndex = 11;
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(262, 106);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(202, 20);
			this.txtTo.TabIndex = 13;
			// 
			// lblTo
			// 
			this.lblTo.AutoSize = true;
			this.lblTo.Location = new System.Drawing.Point(227, 109);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(23, 13);
			this.lblTo.TabIndex = 12;
			this.lblTo.Text = "To:";
			// 
			// txtBCC
			// 
			this.txtBCC.Location = new System.Drawing.Point(77, 132);
			this.txtBCC.Name = "txtBCC";
			this.txtBCC.Size = new System.Drawing.Size(140, 20);
			this.txtBCC.TabIndex = 17;
			// 
			// lblBCC
			// 
			this.lblBCC.AutoSize = true;
			this.lblBCC.Location = new System.Drawing.Point(13, 135);
			this.lblBCC.Name = "lblBCC";
			this.lblBCC.Size = new System.Drawing.Size(31, 13);
			this.lblBCC.TabIndex = 16;
			this.lblBCC.Text = "BCC:";
			// 
			// txtCC
			// 
			this.txtCC.Location = new System.Drawing.Point(262, 132);
			this.txtCC.Name = "txtCC";
			this.txtCC.Size = new System.Drawing.Size(202, 20);
			this.txtCC.TabIndex = 15;
			// 
			// lblCC
			// 
			this.lblCC.AutoSize = true;
			this.lblCC.Location = new System.Drawing.Point(227, 135);
			this.lblCC.Name = "lblCC";
			this.lblCC.Size = new System.Drawing.Size(24, 13);
			this.lblCC.TabIndex = 14;
			this.lblCC.Text = "CC:";
			// 
			// txtOutput
			// 
			this.txtOutput.Location = new System.Drawing.Point(16, 358);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ReadOnly = true;
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutput.Size = new System.Drawing.Size(448, 225);
			this.txtOutput.TabIndex = 24;
			// 
			// btnTestConnection
			// 
			this.btnTestConnection.Location = new System.Drawing.Point(328, 62);
			this.btnTestConnection.Name = "btnTestConnection";
			this.btnTestConnection.Size = new System.Drawing.Size(136, 23);
			this.btnTestConnection.TabIndex = 22;
			this.btnTestConnection.Text = "&Test Connection";
			this.btnTestConnection.UseVisualStyleBackColor = true;
			this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
			// 
			// btnTestEmail
			// 
			this.btnTestEmail.Location = new System.Drawing.Point(328, 313);
			this.btnTestEmail.Name = "btnTestEmail";
			this.btnTestEmail.Size = new System.Drawing.Size(136, 23);
			this.btnTestEmail.TabIndex = 23;
			this.btnTestEmail.Text = "&Send Email";
			this.btnTestEmail.UseVisualStyleBackColor = true;
			this.btnTestEmail.Click += new System.EventHandler(this.btnTestEmail_Click);
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(77, 158);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(387, 20);
			this.txtSubject.TabIndex = 19;
			// 
			// lblSubject
			// 
			this.lblSubject.AutoSize = true;
			this.lblSubject.Location = new System.Drawing.Point(13, 161);
			this.lblSubject.Name = "lblSubject";
			this.lblSubject.Size = new System.Drawing.Size(46, 13);
			this.lblSubject.TabIndex = 18;
			this.lblSubject.Text = "Subject:";
			// 
			// lblBody
			// 
			this.lblBody.AutoSize = true;
			this.lblBody.Location = new System.Drawing.Point(13, 187);
			this.lblBody.Name = "lblBody";
			this.lblBody.Size = new System.Drawing.Size(34, 13);
			this.lblBody.TabIndex = 20;
			this.lblBody.Text = "Body:";
			// 
			// txtBody
			// 
			this.txtBody.Location = new System.Drawing.Point(77, 184);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtBody.Size = new System.Drawing.Size(387, 123);
			this.txtBody.TabIndex = 21;
			// 
			// chkDefaultCredentials
			// 
			this.chkDefaultCredentials.AutoSize = true;
			this.chkDefaultCredentials.Enabled = false;
			this.chkDefaultCredentials.Location = new System.Drawing.Point(408, 38);
			this.chkDefaultCredentials.Name = "chkDefaultCredentials";
			this.chkDefaultCredentials.Size = new System.Drawing.Size(60, 17);
			this.chkDefaultCredentials.TabIndex = 9;
			this.chkDefaultCredentials.Text = "Default";
			this.chkDefaultCredentials.UseVisualStyleBackColor = true;
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(262, 36);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.ReadOnly = true;
			this.txtPassword.Size = new System.Drawing.Size(140, 20);
			this.txtPassword.TabIndex = 8;
			// 
			// lblPassword
			// 
			this.lblPassword.AutoSize = true;
			this.lblPassword.Location = new System.Drawing.Point(227, 39);
			this.lblPassword.Name = "lblPassword";
			this.lblPassword.Size = new System.Drawing.Size(31, 13);
			this.lblPassword.TabIndex = 7;
			this.lblPassword.Text = "Pwd:";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(77, 36);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.ReadOnly = true;
			this.txtUsername.Size = new System.Drawing.Size(140, 20);
			this.txtUsername.TabIndex = 6;
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Location = new System.Drawing.Point(13, 39);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(58, 13);
			this.lblUsername.TabIndex = 5;
			this.lblUsername.Text = "Username:";
			// 
			// pbLoading
			// 
			this.pbLoading.BackColor = System.Drawing.Color.Transparent;
			this.pbLoading.Image = ((System.Drawing.Image)(resources.GetObject("pbLoading.Image")));
			this.pbLoading.Location = new System.Drawing.Point(132, 335);
			this.pbLoading.Name = "pbLoading";
			this.pbLoading.Size = new System.Drawing.Size(220, 20);
			this.pbLoading.TabIndex = 25;
			this.pbLoading.TabStop = false;
			this.pbLoading.Visible = false;
			// 
			// SmtpTester
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(484, 595);
			this.Controls.Add(this.pbLoading);
			this.Controls.Add(this.chkDefaultCredentials);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.lblPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.lblUsername);
			this.Controls.Add(this.lblBody);
			this.Controls.Add(this.txtBody);
			this.Controls.Add(this.lblSubject);
			this.Controls.Add(this.txtSubject);
			this.Controls.Add(this.btnTestEmail);
			this.Controls.Add(this.btnTestConnection);
			this.Controls.Add(this.txtOutput);
			this.Controls.Add(this.txtBCC);
			this.Controls.Add(this.lblBCC);
			this.Controls.Add(this.txtCC);
			this.Controls.Add(this.lblCC);
			this.Controls.Add(this.txtTo);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.txtFrom);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.chkEnableTLS);
			this.Controls.Add(this.txtPort);
			this.Controls.Add(this.lblPort);
			this.Controls.Add(this.txtServer);
			this.Controls.Add(this.lblServer);
			this.Name = "SmtpTester";
			this.Text = "SMTP Tester";
			this.Load += new System.EventHandler(this.SmtpTester_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbLoading)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Label lblPort;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.CheckBox chkEnableTLS;
		private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.TextBox txtBCC;
		private System.Windows.Forms.Label lblBCC;
		private System.Windows.Forms.TextBox txtCC;
		private System.Windows.Forms.Label lblCC;
		private System.Windows.Forms.TextBox txtOutput;
		private System.Windows.Forms.Button btnTestConnection;
		private System.Windows.Forms.Button btnTestEmail;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.Label lblSubject;
		private System.Windows.Forms.Label lblBody;
		private System.Windows.Forms.TextBox txtBody;
		private System.Windows.Forms.CheckBox chkDefaultCredentials;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label lblUsername;
		private System.Windows.Forms.PictureBox pbLoading;
	}
}

