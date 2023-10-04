using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Web;
using Microsoft.AspNetCore.SignalR.Client;



// Afk players dont go away so create Player class and all that jazz

namespace FinaleSignalR_Client
{
    public partial class Form1 : Form
    {
        int playerspeed;
        int playerCount = 0;
        public string id;
        PictureBox playerBox;
        public MapControl mapControl;
        Communication comm;

        int mapMinX = 0;
        int mapMinY = 0;
        int mapMaxX = 0;
        int mapMaxY = 0;

        //List<Rectangle> obstacles;

        public Form1()
        {
            InitializeComponent();
            this.playerBoxes = new System.Windows.Forms.PictureBox[50];

            playerspeed = 5;
            this.KeyPreview = true;
            this.mapControl = new MapControl();
            this.Controls.Add(this.mapControl);
            //this.obstacles = new List<Rectangle>();
            //obstacles.Add(new Rectangle(100, 100, 50, 50));

            Random rnd = new Random();
            id = rnd.Next(100000).ToString();

            comm = new Communication(messages, this);
        }

        public void createPlayer(string id)
        {
            var Player = new PictureBox();
            Player.BackColor = System.Drawing.SystemColors.ControlDark;
            Player.Location = new System.Drawing.Point(666, 422);
            Player.Name = id;
            Player.Size = new System.Drawing.Size(64, 64);
            Player.TabIndex = 0;
            Player.TabStop = false;

            this.mapMaxX = mapControl.Width - Player.Width;
            this.mapMaxY = mapControl.Height - Player.Height;

            //adding player to map
            playerBoxes[playerCount] = Player;
            this.mapControl.Controls.Add(playerBoxes[playerCount]);


            playerCount++;
        }

        public void RequestAccepted()
        {
            createPlayer(id.ToString());
            playerBox = playerBoxes[0];
            ServerTimer.Start();
        }

        public void GameStartButtonStuff()
        {
            openConnection.Enabled = false;
            sendMessage.Enabled = true;
        }

        public void GameClosedButtonStuff()
        {
            openConnection.Enabled = true;
            sendMessage.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            comm.ParseMessage();
        }

        public void moveEnemy(string id, int left, int top)
        {
            for (int i = 0; i < playerCount; i++)
            {
                if (playerBoxes[i].Name == id)
                {
                    playerBoxes[i].Left = left;
                    playerBoxes[i].Top = top;
                }
            }
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            comm.SendChatMessage(messageInput.Text);
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                LeftTimer.Start();
            }

            if (e.KeyCode == Keys.Right)
            {
                RightTimer.Start();
            }

            if (e.KeyCode == Keys.Up)
            {
                UpTimer.Start();
            }

            if (e.KeyCode == Keys.Down)
            {
                DownTimer.Start();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                LeftTimer.Stop();
            }
            if (e.KeyCode == Keys.Right)
            {
                RightTimer.Stop();
            }
            if (e.KeyCode == Keys.Up)
            {
                UpTimer.Stop();
            }
            if (e.KeyCode == Keys.Down)
            {
                DownTimer.Stop();
            }
        }

        private void DownTimer_Tick(object sender, EventArgs e)
        {

            if (playerBox.Top + playerspeed < mapMaxY)
            {
                int newPlayerTop = playerBox.Top + playerspeed;

                // Check if the new position collides with any obstacle
                if (!CollidesWithObstacle(playerBox.Left, newPlayerTop, playerBox.Width, playerBox.Height))
                    playerBox.Top += playerspeed;
            }
        }

        private void LeftTimer_Tick_1(object sender, EventArgs e)
        {
            if (playerBox.Left - playerspeed > mapMinX)
            {
                int newPlayerLeft = playerBox.Left - playerspeed;

                // Check if the new position collides with any obstacle
                if (!CollidesWithObstacle(newPlayerLeft, playerBox.Top, playerBox.Width, playerBox.Height))

                    playerBox.Left -= playerspeed;
            }
        }

        private void UpTimer_Tick_1(object sender, EventArgs e)
        {
            if (playerBox.Top - playerspeed > mapMinY)
            {
                int newPlayerTop = playerBox.Top - playerspeed;

                // Check if the new position collides with any obstacle
                if (!CollidesWithObstacle(playerBox.Left, newPlayerTop, playerBox.Width, playerBox.Height))
                    playerBox.Top -= playerspeed;
            }
        }

        private void RightTimer_Tick_1(object sender, EventArgs e)
        {
            if (playerBox.Left + playerspeed < mapMaxX)
            {
                int newPlayerLeft = playerBox.Left + playerspeed;
                if (!CollidesWithObstacle(newPlayerLeft, playerBox.Top, playerBox.Width, playerBox.Height))
                    playerBox.Left += playerspeed;
            }
        }

        // Function to check collision with obstacles
        private bool CollidesWithObstacle(int x, int y, int width, int height)
        {
            Rectangle playerRect = new Rectangle(x, y, width, height);

            foreach (Rectangle obstacle in this.mapControl.obstacles)
            {
                if (playerRect.IntersectsWith(obstacle))
                {
                    return true; // Collision detected
                }
            }

            return false; // No collision detected
        }

        private void ServerTimer_Tick(object sender, EventArgs e)
        {
            comm.SendCoordinates(playerBox.Left, playerBox.Top);
        }
    }
}
