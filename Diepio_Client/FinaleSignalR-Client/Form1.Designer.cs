using System.Collections.Generic;

namespace FinaleSignalR_Client
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.openConnection = new System.Windows.Forms.Button();
            this.LeftTimer = new System.Windows.Forms.Timer(this.components);
            this.RightTimer = new System.Windows.Forms.Timer(this.components);
            this.UpTimer = new System.Windows.Forms.Timer(this.components);
            this.DownTimer = new System.Windows.Forms.Timer(this.components);
            this.ServerTimer = new System.Windows.Forms.Timer(this.components);
            this.sendMessage = new System.Windows.Forms.Button();
            this.messageInput = new System.Windows.Forms.TextBox();
            this.messages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // openConnection
            // 
            this.openConnection.Location = new System.Drawing.Point(1288, 27);
            this.openConnection.Margin = new System.Windows.Forms.Padding(4);
            this.openConnection.Name = "openConnection";
            this.openConnection.Size = new System.Drawing.Size(201, 50);
            this.openConnection.TabIndex = 0;
            this.openConnection.Text = "Open Connection";
            this.openConnection.UseVisualStyleBackColor = true;
            this.openConnection.Click += new System.EventHandler(this.button1_Click);
            // 
            // LeftTimer
            // 
            this.LeftTimer.Interval = 5;
            this.LeftTimer.Tick += new System.EventHandler(this.LeftTimer_Tick_1);
            // 
            // RightTimer
            // 
            this.RightTimer.Interval = 5;
            this.RightTimer.Tick += new System.EventHandler(this.RightTimer_Tick_1);
            // 
            // UpTimer
            // 
            this.UpTimer.Interval = 5;
            this.UpTimer.Tick += new System.EventHandler(this.UpTimer_Tick_1);
            // 
            // DownTimer
            // 
            this.DownTimer.Interval = 5;
            this.DownTimer.Tick += new System.EventHandler(this.DownTimer_Tick);
            // 
            // ServerTimer
            // 
            this.ServerTimer.Interval = 10;
            this.ServerTimer.Tick += new System.EventHandler(this.ServerTimer_Tick);
            // 
            // sendMessage
            // 
            this.sendMessage.Enabled = false;
            this.sendMessage.Location = new System.Drawing.Point(1302, 85);
            this.sendMessage.Margin = new System.Windows.Forms.Padding(4);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(187, 53);
            this.sendMessage.TabIndex = 2;
            this.sendMessage.Text = "Send Message";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // messageInput
            // 
            this.messageInput.Location = new System.Drawing.Point(1318, 146);
            this.messageInput.Margin = new System.Windows.Forms.Padding(4);
            this.messageInput.Name = "messageInput";
            this.messageInput.Size = new System.Drawing.Size(171, 22);
            this.messageInput.TabIndex = 1;
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.ItemHeight = 16;
            this.messages.Location = new System.Drawing.Point(1339, 183);
            this.messages.Margin = new System.Windows.Forms.Padding(4);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(150, 68);
            this.messages.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1529, 983);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.messageInput);
            this.Controls.Add(this.openConnection);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.KeyIsDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.KeyIsUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox[] playerBoxes;
        private System.Windows.Forms.Button openConnection;
        private System.Windows.Forms.Timer LeftTimer;
        private System.Windows.Forms.Timer RightTimer;
        private System.Windows.Forms.Timer UpTimer;
        private System.Windows.Forms.Timer DownTimer;
        private System.Windows.Forms.Timer ServerTimer;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.TextBox messageInput;
        private System.Windows.Forms.ListBox messages;
    }
}

