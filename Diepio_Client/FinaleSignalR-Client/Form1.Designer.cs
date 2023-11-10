using System.Collections.Generic;

namespace FinaleSignalR_Client
{
    partial class Form1
    {
        /// <summary>
        /// Requi
        /// designer variable.
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
            this.bulletMovementTimer = new System.Windows.Forms.Timer(this.components);
            this.sendMessage = new System.Windows.Forms.Button();
            this.messageInput = new System.Windows.Forms.TextBox();
            this.messages = new System.Windows.Forms.ListBox();
            this.ChangeColors = new System.Windows.Forms.Button();
            this.ChooseTank = new System.Windows.Forms.Button();
            this.ChooseScout = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openConnection
            // 
            this.openConnection.Location = new System.Drawing.Point(966, 22);
            this.openConnection.Name = "openConnection";
            this.openConnection.Size = new System.Drawing.Size(151, 41);
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
            this.ServerTimer.Interval = 50;
            this.ServerTimer.Tick += new System.EventHandler(this.ServerTimer_Tick);
            // 
            // bulletMovementTimer
            // 
            this.bulletMovementTimer.Interval = 1;
            this.bulletMovementTimer.Tick += new System.EventHandler(this.bulletMovementTimer_Tick);
            // 
            // sendMessage
            // 
            this.sendMessage.Enabled = false;
            this.sendMessage.Location = new System.Drawing.Point(976, 69);
            this.sendMessage.Name = "sendMessage";
            this.sendMessage.Size = new System.Drawing.Size(140, 43);
            this.sendMessage.TabIndex = 2;
            this.sendMessage.Text = "Send Message";
            this.sendMessage.UseVisualStyleBackColor = true;
            this.sendMessage.Click += new System.EventHandler(this.sendMessage_Click);
            // 
            // messageInput
            // 
            this.messageInput.Location = new System.Drawing.Point(989, 176);
            this.messageInput.Name = "messageInput";
            this.messageInput.Size = new System.Drawing.Size(129, 20);
            this.messageInput.TabIndex = 1;
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.Location = new System.Drawing.Point(845, 214);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(273, 524);
            this.messages.TabIndex = 3;
            // 
            // ChangeColors
            // 
            this.ChangeColors.Location = new System.Drawing.Point(976, 118);
            this.ChangeColors.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChangeColors.Name = "ChangeColors";
            this.ChangeColors.Size = new System.Drawing.Size(140, 39);
            this.ChangeColors.TabIndex = 4;
            this.ChangeColors.Text = "Change colors";
            this.ChangeColors.UseVisualStyleBackColor = true;
            this.ChangeColors.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ChooseTank
            // 
            this.ChooseTank.Location = new System.Drawing.Point(821, 118);
            this.ChooseTank.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChooseTank.Name = "ChooseTank";
            this.ChooseTank.Size = new System.Drawing.Size(140, 39);
            this.ChooseTank.TabIndex = 5;
            this.ChooseTank.Text = "Choose tank";
            this.ChooseTank.UseVisualStyleBackColor = true;
            this.ChooseTank.Click += new System.EventHandler(this.ChooseTank_Click);
            // 
            // ChooseScout
            // 
            this.ChooseScout.Location = new System.Drawing.Point(821, 73);
            this.ChooseScout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ChooseScout.Name = "ChooseScout";
            this.ChooseScout.Size = new System.Drawing.Size(140, 39);
            this.ChooseScout.TabIndex = 6;
            this.ChooseScout.Text = "Choose scout";
            this.ChooseScout.UseVisualStyleBackColor = true;
            this.ChooseScout.Click += new System.EventHandler(this.ChooseScout_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1147, 799);
            this.Controls.Add(this.ChooseScout);
            this.Controls.Add(this.ChooseTank);
            this.Controls.Add(this.ChangeColors);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.sendMessage);
            this.Controls.Add(this.messageInput);
            this.Controls.Add(this.openConnection);
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
        private System.Windows.Forms.Timer bulletMovementTimer;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.TextBox messageInput;
        private System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.Button ChangeColors;
        private System.Windows.Forms.Button ChooseTank;
        private System.Windows.Forms.Button ChooseScout;
    }
}

