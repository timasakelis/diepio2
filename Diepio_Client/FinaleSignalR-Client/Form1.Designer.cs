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
            this.ArcticColors = new System.Windows.Forms.Button();
            this.ChooseTank = new System.Windows.Forms.Button();
            this.ChooseScout = new System.Windows.Forms.Button();
            this.CreateCharacter = new System.Windows.Forms.Button();
            this.DesertColors = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openConnection
            // 
            this.openConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.openConnection.Location = new System.Drawing.Point(468, 217);
            this.openConnection.Margin = new System.Windows.Forms.Padding(4);
            this.openConnection.Name = "openConnection";
            this.openConnection.Size = new System.Drawing.Size(434, 116);
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
            this.sendMessage.Location = new System.Drawing.Point(1303, 144);
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
            this.messageInput.Location = new System.Drawing.Point(1319, 217);
            this.messageInput.Margin = new System.Windows.Forms.Padding(4);
            this.messageInput.Name = "messageInput";
            this.messageInput.Size = new System.Drawing.Size(171, 22);
            this.messageInput.TabIndex = 1;
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.ItemHeight = 16;
            this.messages.Location = new System.Drawing.Point(1151, 248);
            this.messages.Margin = new System.Windows.Forms.Padding(4);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(339, 660);
            this.messages.TabIndex = 3;
            // 
            // ArcticColors
            // 
            this.ArcticColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArcticColors.Location = new System.Drawing.Point(373, 217);
            this.ArcticColors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ArcticColors.Name = "ArcticColors";
            this.ArcticColors.Size = new System.Drawing.Size(289, 116);
            this.ArcticColors.TabIndex = 4;
            this.ArcticColors.Text = "Arctic colors";
            this.ArcticColors.UseVisualStyleBackColor = true;
            this.ArcticColors.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // ChooseTank
            // 
            this.ChooseTank.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseTank.Location = new System.Drawing.Point(725, 217);
            this.ChooseTank.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChooseTank.Name = "ChooseTank";
            this.ChooseTank.Size = new System.Drawing.Size(289, 116);
            this.ChooseTank.TabIndex = 5;
            this.ChooseTank.Text = "Choose tank";
            this.ChooseTank.UseVisualStyleBackColor = true;
            this.ChooseTank.Click += new System.EventHandler(this.ChooseTank_Click);
            // 
            // ChooseScout
            // 
            this.ChooseScout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseScout.Location = new System.Drawing.Point(373, 217);
            this.ChooseScout.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChooseScout.Name = "ChooseScout";
            this.ChooseScout.Size = new System.Drawing.Size(289, 116);
            this.ChooseScout.TabIndex = 6;
            this.ChooseScout.Text = "Choose scout";
            this.ChooseScout.UseVisualStyleBackColor = true;
            this.ChooseScout.Click += new System.EventHandler(this.ChooseScout_Click);
            // 
            // CreateCharacter
            // 
            this.CreateCharacter.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CreateCharacter.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateCharacter.Location = new System.Drawing.Point(485, 217);
            this.CreateCharacter.Name = "CreateCharacter";
            this.CreateCharacter.Size = new System.Drawing.Size(437, 116);
            this.CreateCharacter.TabIndex = 7;
            this.CreateCharacter.Text = "Create character";
            this.CreateCharacter.UseVisualStyleBackColor = true;
            this.CreateCharacter.Click += new System.EventHandler(this.CreateCharacter_Click);
            // 
            // DesertColors
            // 
            this.DesertColors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DesertColors.Location = new System.Drawing.Point(725, 217);
            this.DesertColors.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DesertColors.Name = "DesertColors";
            this.DesertColors.Size = new System.Drawing.Size(289, 116);
            this.DesertColors.TabIndex = 8;
            this.DesertColors.Text = "Desert colors";
            this.DesertColors.UseVisualStyleBackColor = true;
            this.DesertColors.Click += new System.EventHandler(this.DesertColors_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1529, 983);
            this.Controls.Add(this.DesertColors);
            this.Controls.Add(this.CreateCharacter);
            this.Controls.Add(this.ChooseScout);
            this.Controls.Add(this.ChooseTank);
            this.Controls.Add(this.ArcticColors);
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
        private System.Windows.Forms.Timer bulletMovementTimer;
        private System.Windows.Forms.Button sendMessage;
        private System.Windows.Forms.TextBox messageInput;
        public System.Windows.Forms.ListBox messages;
        private System.Windows.Forms.Button ArcticColors;
        private System.Windows.Forms.Button ChooseTank;
        private System.Windows.Forms.Button ChooseScout;
        private System.Windows.Forms.Button CreateCharacter;
        private System.Windows.Forms.Button DesertColors;
    }
}

