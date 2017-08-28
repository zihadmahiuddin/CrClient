namespace CrClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public System.ComponentModel.IContainer components = null;

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
        public void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnHello = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.tbSessionKey = new System.Windows.Forms.TextBox();
            this.lbSessionKey = new System.Windows.Forms.Label();
            this.lblPacket = new System.Windows.Forms.Label();
            this.lblEncrypted = new System.Windows.Forms.Label();
            this.lblDecrypted = new System.Windows.Forms.Label();
            this.rtbPacket = new System.Windows.Forms.RichTextBox();
            this.rtbEncrypted = new System.Windows.Forms.RichTextBox();
            this.rtbDecrypted = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(50, 66);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(517, 66);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnHello
            // 
            this.btnHello.Location = new System.Drawing.Point(50, 95);
            this.btnHello.Name = "btnHello";
            this.btnHello.Size = new System.Drawing.Size(75, 23);
            this.btnHello.TabIndex = 2;
            this.btnHello.Text = "10100";
            this.btnHello.UseVisualStyleBackColor = true;
            this.btnHello.Click += new System.EventHandler(this.btnHello_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(517, 95);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "10101";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbSessionKey
            // 
            this.tbSessionKey.Location = new System.Drawing.Point(147, 32);
            this.tbSessionKey.Name = "tbSessionKey";
            this.tbSessionKey.Size = new System.Drawing.Size(445, 20);
            this.tbSessionKey.TabIndex = 4;
            // 
            // lbSessionKey
            // 
            this.lbSessionKey.AutoSize = true;
            this.lbSessionKey.Location = new System.Drawing.Point(47, 35);
            this.lbSessionKey.Name = "lbSessionKey";
            this.lbSessionKey.Size = new System.Drawing.Size(68, 13);
            this.lbSessionKey.TabIndex = 5;
            this.lbSessionKey.Text = "Session Key:";
            // 
            // lblPacket
            // 
            this.lblPacket.AutoSize = true;
            this.lblPacket.Location = new System.Drawing.Point(47, 135);
            this.lblPacket.Name = "lblPacket";
            this.lblPacket.Size = new System.Drawing.Size(88, 13);
            this.lblPacket.TabIndex = 8;
            this.lblPacket.Text = "Packet So Send:";
            // 
            // lblEncrypted
            // 
            this.lblEncrypted.AutoSize = true;
            this.lblEncrypted.Location = new System.Drawing.Point(47, 216);
            this.lblEncrypted.Name = "lblEncrypted";
            this.lblEncrypted.Size = new System.Drawing.Size(95, 13);
            this.lblEncrypted.TabIndex = 9;
            this.lblEncrypted.Text = "Encrypted Packet:";
            // 
            // lblDecrypted
            // 
            this.lblDecrypted.AutoSize = true;
            this.lblDecrypted.Location = new System.Drawing.Point(46, 291);
            this.lblDecrypted.Name = "lblDecrypted";
            this.lblDecrypted.Size = new System.Drawing.Size(96, 13);
            this.lblDecrypted.TabIndex = 10;
            this.lblDecrypted.Text = "Decrypted Packet:";
            // 
            // rtbPacket
            // 
            this.rtbPacket.Location = new System.Drawing.Point(141, 132);
            this.rtbPacket.Name = "rtbPacket";
            this.rtbPacket.Size = new System.Drawing.Size(512, 86);
            this.rtbPacket.TabIndex = 11;
            this.rtbPacket.Text = "";
            // 
            // rtbEncrypted
            // 
            this.rtbEncrypted.Location = new System.Drawing.Point(141, 213);
            this.rtbEncrypted.Name = "rtbEncrypted";
            this.rtbEncrypted.Size = new System.Drawing.Size(512, 75);
            this.rtbEncrypted.TabIndex = 12;
            this.rtbEncrypted.Text = "";
            // 
            // rtbDecrypted
            // 
            this.rtbDecrypted.Location = new System.Drawing.Point(141, 288);
            this.rtbDecrypted.Name = "rtbDecrypted";
            this.rtbDecrypted.Size = new System.Drawing.Size(512, 74);
            this.rtbDecrypted.TabIndex = 13;
            this.rtbDecrypted.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 376);
            this.Controls.Add(this.rtbDecrypted);
            this.Controls.Add(this.rtbEncrypted);
            this.Controls.Add(this.rtbPacket);
            this.Controls.Add(this.lblDecrypted);
            this.Controls.Add(this.lblEncrypted);
            this.Controls.Add(this.lblPacket);
            this.Controls.Add(this.lbSessionKey);
            this.Controls.Add(this.tbSessionKey);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnHello);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnConnect);
            this.Name = "Form1";
            this.Text = "CrClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button btnConnect;
        public System.Windows.Forms.Button btnDisconnect;
        public System.Windows.Forms.Button btnHello;
        public System.Windows.Forms.Button btnLogin;
        public System.Windows.Forms.TextBox tbSessionKey;
        public System.Windows.Forms.Label lbSessionKey;
        public System.Windows.Forms.Label lblPacket;
        public System.Windows.Forms.Label lblEncrypted;
        public System.Windows.Forms.Label lblDecrypted;
        public System.Windows.Forms.RichTextBox rtbPacket;
        public System.Windows.Forms.RichTextBox rtbEncrypted;
        public System.Windows.Forms.RichTextBox rtbDecrypted;
    }
}

