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
            this.lblClanChatMessage = new System.Windows.Forms.Label();
            this.tbClanChatMessage = new System.Windows.Forms.TextBox();
            this.btnSendClanChatMessage = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.tbTagForFreeChest = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblJoinClan = new System.Windows.Forms.Label();
            this.btnJoinClan = new System.Windows.Forms.Button();
            this.tbClanTag = new System.Windows.Forms.TextBox();
            this.btnLeaveCurrentClan = new System.Windows.Forms.Button();
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
            this.btnDisconnect.Location = new System.Drawing.Point(141, 66);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 1;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnHello
            // 
            this.btnHello.Location = new System.Drawing.Point(42, 125);
            this.btnHello.Name = "btnHello";
            this.btnHello.Size = new System.Drawing.Size(75, 23);
            this.btnHello.TabIndex = 2;
            this.btnHello.Text = "Hello";
            this.btnHello.UseVisualStyleBackColor = true;
            this.btnHello.Click += new System.EventHandler(this.btnHello_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(141, 125);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // tbSessionKey
            // 
            this.tbSessionKey.Location = new System.Drawing.Point(141, 6);
            this.tbSessionKey.Name = "tbSessionKey";
            this.tbSessionKey.Size = new System.Drawing.Size(451, 20);
            this.tbSessionKey.TabIndex = 4;
            // 
            // lbSessionKey
            // 
            this.lbSessionKey.AutoSize = true;
            this.lbSessionKey.Location = new System.Drawing.Point(47, 9);
            this.lbSessionKey.Name = "lbSessionKey";
            this.lbSessionKey.Size = new System.Drawing.Size(68, 13);
            this.lbSessionKey.TabIndex = 5;
            this.lbSessionKey.Text = "Session Key:";
            // 
            // lblPacket
            // 
            this.lblPacket.AutoSize = true;
            this.lblPacket.Location = new System.Drawing.Point(39, 194);
            this.lblPacket.Name = "lblPacket";
            this.lblPacket.Size = new System.Drawing.Size(88, 13);
            this.lblPacket.TabIndex = 8;
            this.lblPacket.Text = "Packet So Send:";
            // 
            // lblEncrypted
            // 
            this.lblEncrypted.AutoSize = true;
            this.lblEncrypted.Location = new System.Drawing.Point(39, 286);
            this.lblEncrypted.Name = "lblEncrypted";
            this.lblEncrypted.Size = new System.Drawing.Size(95, 13);
            this.lblEncrypted.TabIndex = 9;
            this.lblEncrypted.Text = "Encrypted Packet:";
            // 
            // lblDecrypted
            // 
            this.lblDecrypted.AutoSize = true;
            this.lblDecrypted.Location = new System.Drawing.Point(39, 367);
            this.lblDecrypted.Name = "lblDecrypted";
            this.lblDecrypted.Size = new System.Drawing.Size(96, 13);
            this.lblDecrypted.TabIndex = 10;
            this.lblDecrypted.Text = "Decrypted Packet:";
            // 
            // rtbPacket
            // 
            this.rtbPacket.Location = new System.Drawing.Point(141, 191);
            this.rtbPacket.Name = "rtbPacket";
            this.rtbPacket.Size = new System.Drawing.Size(512, 86);
            this.rtbPacket.TabIndex = 11;
            this.rtbPacket.Text = "";
            // 
            // rtbEncrypted
            // 
            this.rtbEncrypted.Location = new System.Drawing.Point(141, 283);
            this.rtbEncrypted.Name = "rtbEncrypted";
            this.rtbEncrypted.Size = new System.Drawing.Size(512, 75);
            this.rtbEncrypted.TabIndex = 12;
            this.rtbEncrypted.Text = "";
            // 
            // rtbDecrypted
            // 
            this.rtbDecrypted.Location = new System.Drawing.Point(141, 364);
            this.rtbDecrypted.Name = "rtbDecrypted";
            this.rtbDecrypted.Size = new System.Drawing.Size(512, 74);
            this.rtbDecrypted.TabIndex = 13;
            this.rtbDecrypted.Text = "";
            // 
            // lblClanChatMessage
            // 
            this.lblClanChatMessage.AutoSize = true;
            this.lblClanChatMessage.Location = new System.Drawing.Point(47, 43);
            this.lblClanChatMessage.Name = "lblClanChatMessage";
            this.lblClanChatMessage.Size = new System.Drawing.Size(53, 13);
            this.lblClanChatMessage.TabIndex = 15;
            this.lblClanChatMessage.Text = "Message:";
            // 
            // tbClanChatMessage
            // 
            this.tbClanChatMessage.Location = new System.Drawing.Point(141, 40);
            this.tbClanChatMessage.Name = "tbClanChatMessage";
            this.tbClanChatMessage.Size = new System.Drawing.Size(370, 20);
            this.tbClanChatMessage.TabIndex = 14;
            // 
            // btnSendClanChatMessage
            // 
            this.btnSendClanChatMessage.Location = new System.Drawing.Point(517, 37);
            this.btnSendClanChatMessage.Name = "btnSendClanChatMessage";
            this.btnSendClanChatMessage.Size = new System.Drawing.Size(75, 23);
            this.btnSendClanChatMessage.TabIndex = 16;
            this.btnSendClanChatMessage.Text = "Send";
            this.btnSendClanChatMessage.UseVisualStyleBackColor = true;
            this.btnSendClanChatMessage.Click += new System.EventHandler(this.btnSendClanChatMessage_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(594, 67);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(59, 23);
            this.btnOpen.TabIndex = 18;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // tbTagForFreeChest
            // 
            this.tbTagForFreeChest.Location = new System.Drawing.Point(350, 70);
            this.tbTagForFreeChest.Name = "tbTagForFreeChest";
            this.tbTagForFreeChest.Size = new System.Drawing.Size(238, 20);
            this.tbTagForFreeChest.TabIndex = 17;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Open Free Chest(Tag):";
            // 
            // lblJoinClan
            // 
            this.lblJoinClan.AutoSize = true;
            this.lblJoinClan.Location = new System.Drawing.Point(229, 130);
            this.lblJoinClan.Name = "lblJoinClan";
            this.lblJoinClan.Size = new System.Drawing.Size(78, 13);
            this.lblJoinClan.TabIndex = 22;
            this.lblJoinClan.Text = "Join Clan(Tag):";
            // 
            // btnJoinClan
            // 
            this.btnJoinClan.Location = new System.Drawing.Point(594, 125);
            this.btnJoinClan.Name = "btnJoinClan";
            this.btnJoinClan.Size = new System.Drawing.Size(59, 23);
            this.btnJoinClan.TabIndex = 21;
            this.btnJoinClan.Text = "Join";
            this.btnJoinClan.UseVisualStyleBackColor = true;
            this.btnJoinClan.Click += new System.EventHandler(this.btnJoinClan_Click);
            // 
            // tbClanTag
            // 
            this.tbClanTag.Location = new System.Drawing.Point(350, 127);
            this.tbClanTag.Name = "tbClanTag";
            this.tbClanTag.Size = new System.Drawing.Size(238, 20);
            this.tbClanTag.TabIndex = 20;
            // 
            // btnLeaveCurrentClan
            // 
            this.btnLeaveCurrentClan.Location = new System.Drawing.Point(671, 97);
            this.btnLeaveCurrentClan.Name = "btnLeaveCurrentClan";
            this.btnLeaveCurrentClan.Size = new System.Drawing.Size(59, 23);
            this.btnLeaveCurrentClan.TabIndex = 23;
            this.btnLeaveCurrentClan.Text = "Leave current clan";
            this.btnLeaveCurrentClan.UseVisualStyleBackColor = true;
            this.btnLeaveCurrentClan.Click += new System.EventHandler(this.btnLeaveCurrentClan_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 450);
            this.Controls.Add(this.btnLeaveCurrentClan);
            this.Controls.Add(this.lblJoinClan);
            this.Controls.Add(this.btnJoinClan);
            this.Controls.Add(this.tbClanTag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.tbTagForFreeChest);
            this.Controls.Add(this.btnSendClanChatMessage);
            this.Controls.Add(this.lblClanChatMessage);
            this.Controls.Add(this.tbClanChatMessage);
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
        public System.Windows.Forms.Label lblClanChatMessage;
        public System.Windows.Forms.TextBox tbClanChatMessage;
        public System.Windows.Forms.Button btnSendClanChatMessage;
        public System.Windows.Forms.Button btnOpen;
        public System.Windows.Forms.TextBox tbTagForFreeChest;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label lblJoinClan;
        public System.Windows.Forms.Button btnJoinClan;
        public System.Windows.Forms.TextBox tbClanTag;
        public System.Windows.Forms.Button btnLeaveCurrentClan;
    }
}

