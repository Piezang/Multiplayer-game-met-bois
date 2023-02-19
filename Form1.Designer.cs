namespace Multiplayer_game_met_bois
{//1419
    public partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.rtServer = new System.Windows.Forms.RichTextBox();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.ServerPage = new System.Windows.Forms.TabPage();
            this.btnSendServer = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.ClientPage = new System.Windows.Forms.TabPage();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.rtClient = new System.Windows.Forms.RichTextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPortClient = new System.Windows.Forms.TextBox();
            this.lbl = new System.Windows.Forms.Label();
            this.txtHostClient = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lblAmmo = new System.Windows.Forms.Label();
            this.HPbox = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Tabs.SuspendLayout();
            this.ServerPage.SuspendLayout();
            this.ClientPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HPbox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(50, 12);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(152, 23);
            this.txtHost.TabIndex = 0;
            this.txtHost.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Host:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(418, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(178, 34);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(258, 13);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(129, 23);
            this.txtPort.TabIndex = 4;
            this.txtPort.Text = "8910";
            // 
            // rtServer
            // 
            this.rtServer.BackColor = System.Drawing.SystemColors.HotTrack;
            this.rtServer.Location = new System.Drawing.Point(440, 295);
            this.rtServer.Name = "rtServer";
            this.rtServer.Size = new System.Drawing.Size(409, 134);
            this.rtServer.TabIndex = 5;
            this.rtServer.Text = "";
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.ServerPage);
            this.Tabs.Controls.Add(this.ClientPage);
            this.Tabs.Controls.Add(this.tabPage1);
            this.Tabs.Location = new System.Drawing.Point(12, 12);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1427, 837);
            this.Tabs.TabIndex = 6;
            // 
            // ServerPage
            // 
            this.ServerPage.BackColor = System.Drawing.Color.DimGray;
            this.ServerPage.Controls.Add(this.btnSendServer);
            this.ServerPage.Controls.Add(this.txtOutput);
            this.ServerPage.Controls.Add(this.btnStop);
            this.ServerPage.Controls.Add(this.rtServer);
            this.ServerPage.Controls.Add(this.btnStart);
            this.ServerPage.Controls.Add(this.txtPort);
            this.ServerPage.Controls.Add(this.label1);
            this.ServerPage.Controls.Add(this.txtHost);
            this.ServerPage.Controls.Add(this.label2);
            this.ServerPage.Location = new System.Drawing.Point(4, 24);
            this.ServerPage.Name = "ServerPage";
            this.ServerPage.Padding = new System.Windows.Forms.Padding(3);
            this.ServerPage.Size = new System.Drawing.Size(1419, 809);
            this.ServerPage.TabIndex = 0;
            this.ServerPage.Text = "Server";
            // 
            // btnSendServer
            // 
            this.btnSendServer.Location = new System.Drawing.Point(18, 281);
            this.btnSendServer.Name = "btnSendServer";
            this.btnSendServer.Size = new System.Drawing.Size(234, 48);
            this.btnSendServer.TabIndex = 8;
            this.btnSendServer.Text = "Send";
            this.btnSendServer.UseVisualStyleBackColor = true;
            this.btnSendServer.Click += new System.EventHandler(this.btnSendServer_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(18, 105);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(234, 156);
            this.txtOutput.TabIndex = 7;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(613, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(160, 34);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // ClientPage
            // 
            this.ClientPage.BackColor = System.Drawing.Color.RosyBrown;
            this.ClientPage.Controls.Add(this.txtMessage);
            this.ClientPage.Controls.Add(this.btnSend);
            this.ClientPage.Controls.Add(this.rtClient);
            this.ClientPage.Controls.Add(this.btnConnect);
            this.ClientPage.Controls.Add(this.txtPortClient);
            this.ClientPage.Controls.Add(this.lbl);
            this.ClientPage.Controls.Add(this.txtHostClient);
            this.ClientPage.Controls.Add(this.label4);
            this.ClientPage.Location = new System.Drawing.Point(4, 24);
            this.ClientPage.Name = "ClientPage";
            this.ClientPage.Padding = new System.Windows.Forms.Padding(3);
            this.ClientPage.Size = new System.Drawing.Size(1419, 809);
            this.ClientPage.TabIndex = 1;
            this.ClientPage.Text = "Client";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(15, 66);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(807, 101);
            this.txtMessage.TabIndex = 14;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(662, 173);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(160, 34);
            this.btnSend.TabIndex = 13;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // rtClient
            // 
            this.rtClient.BackColor = System.Drawing.SystemColors.HotTrack;
            this.rtClient.Location = new System.Drawing.Point(15, 258);
            this.rtClient.Name = "rtClient";
            this.rtClient.Size = new System.Drawing.Size(815, 134);
            this.rtClient.TabIndex = 12;
            this.rtClient.Text = "";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(634, 16);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(178, 34);
            this.btnConnect.TabIndex = 10;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPortClient
            // 
            this.txtPortClient.Location = new System.Drawing.Point(267, 23);
            this.txtPortClient.Name = "txtPortClient";
            this.txtPortClient.Size = new System.Drawing.Size(129, 23);
            this.txtPortClient.TabIndex = 11;
            this.txtPortClient.Text = "8910";
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(15, 18);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(35, 15);
            this.lbl.TabIndex = 8;
            this.lbl.Text = "Host:";
            // 
            // txtHostClient
            // 
            this.txtHostClient.Location = new System.Drawing.Point(56, 15);
            this.txtHostClient.Name = "txtHostClient";
            this.txtHostClient.Size = new System.Drawing.Size(152, 23);
            this.txtHostClient.TabIndex = 7;
            this.txtHostClient.Text = "127.0.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(229, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Port:";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lblAmmo);
            this.tabPage1.Controls.Add(this.HPbox);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1419, 809);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lblAmmo
            // 
            this.lblAmmo.AutoSize = true;
            this.lblAmmo.BackColor = System.Drawing.Color.Pink;
            this.lblAmmo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAmmo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblAmmo.Location = new System.Drawing.Point(18, 82);
            this.lblAmmo.Name = "lblAmmo";
            this.lblAmmo.Size = new System.Drawing.Size(142, 32);
            this.lblAmmo.TabIndex = 1;
            this.lblAmmo.Text = "AMMO: 100";
            // 
            // HPbox
            // 
            this.HPbox.BackColor = System.Drawing.Color.Lime;
            this.HPbox.Location = new System.Drawing.Point(18, 20);
            this.HPbox.Name = "HPbox";
            this.HPbox.Size = new System.Drawing.Size(230, 59);
            this.HPbox.TabIndex = 0;
            this.HPbox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Pink;
            this.pictureBox1.Location = new System.Drawing.Point(3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(4000, 800);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.WaitOnLoad = true;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClicked);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.TimerUpdate);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1444, 861);
            this.Controls.Add(this.Tabs);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_keyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClicked);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Tabs.ResumeLayout(false);
            this.ServerPage.ResumeLayout(false);
            this.ServerPage.PerformLayout();
            this.ClientPage.ResumeLayout(false);
            this.ClientPage.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HPbox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TextBox txtHost;
        private Label label1;
        private Label label2;
        private Button btnStart;
        private TextBox txtPort;
        private RichTextBox rtServer;
        private TabControl Tabs;
        private TabPage ServerPage;
        private Button btnStop;
        private TabPage ClientPage;
        private TextBox txtMessage;
        private Button btnSend;
        private RichTextBox rtClient;
        private Button btnConnect;
        private TextBox txtPortClient;
        private Label lbl;
        private TextBox txtHostClient;
        private Label label4;
        public TextBox txtOutput;
        private Button btnSendServer;
        private System.Windows.Forms.Timer timer1;
        private TabPage tabPage1;
        private PictureBox pictureBox1;
        private PictureBox HPbox;
        private Label lblAmmo;
    }
}