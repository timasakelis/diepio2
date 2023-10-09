using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;



// Afk players dont go away so create Player class and all that jazz

namespace FinaleSignalR_Client
{


    public partial class Form1 : Form
    {
        public string id;
        int playerCount = 0;
        Player player;
        Player[] players;

        public MapControl mapControl;
        Communication comm;

        int mapMinX = 0;
        int mapMinY = 0;
        int mapMaxX = 0;
        int mapMaxY = 0;


        //Bullet
        private bool isShooting = false;
        private DateTime lastBulletFiredTime;
        private TimeSpan bulletCooldown = TimeSpan.FromMilliseconds(150);
        //Bullet end

        public Form1()
        {
            InitializeComponent();
            this.playerBoxes = new System.Windows.Forms.PictureBox[50];
            this.players = new Player[50];
            
            this.KeyPreview = true;
            this.mapControl = new MapControl();
            this.Controls.Add(this.mapControl);
            this.mapControl.SendToBack();

            Random rnd = new Random();
            id = rnd.Next(100000).ToString();

            comm = new Communication(messages, this);
        }

        public void createPlayer(string id)
        {
            var newPlayer = new Player(new Size(64, 64), id, "new player", 500, 500, 5, SystemColors.ControlDark, new Point(666, 422));

            this.mapMaxX = mapControl.Width - newPlayer.PlayerBox.Width;
            this.mapMaxY = mapControl.Height - newPlayer.PlayerBox.Height;

            //adding player to map
            playerBoxes[playerCount] = newPlayer.PlayerBox;
            players[playerCount] = newPlayer;
            this.mapControl.Controls.Add(playerBoxes[playerCount]);
            playerCount++;
        }

        public void RequestAccepted()
        {
            createPlayer(id.ToString());
            this.player = players[this.playerCount-1];
            ServerTimer.Start();
            bulletMovementTimer.Start();

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

            if (this.player.PlayerBox.Top + this.player.Playerspeed < mapMaxY)
            {
                int newPlayerTop = this.player.PlayerBox.Top + this.player.Playerspeed;

                // Check if the new position collides with any obstacle
                if (!CollidesWithObstacle(this.player.PlayerBox.Left, newPlayerTop, this.player.PlayerBox.Width, this.player.PlayerBox.Height))
                    this.player.PlayerBox.Top += this.player.Playerspeed;
            }
        }

        private void LeftTimer_Tick_1(object sender, EventArgs e)
        {
            if (this.player.PlayerBox.Left - this.player.Playerspeed > mapMinX)
            {
                int newPlayerLeft = this.player.PlayerBox.Left - this.player.Playerspeed;

                // Check if the new position collides with any obstacle
                if (!CollidesWithObstacle(newPlayerLeft, this.player.PlayerBox.Top, this.player.PlayerBox.Width, this.player.PlayerBox.Height))

                    this.player.PlayerBox.Left -= this.player.Playerspeed;
            }
        }

        private void UpTimer_Tick_1(object sender, EventArgs e)
        {
            if (this.player.PlayerBox.Top - this.player.Playerspeed > mapMinY)
            {
                int newPlayerTop = this.player.PlayerBox.Top - this.player.Playerspeed;

                // Check if the new position collides with any obstacle
                if (!CollidesWithObstacle(this.player.PlayerBox.Left, newPlayerTop, this.player.PlayerBox.Width, this.player.PlayerBox.Height))
                    this.player.PlayerBox.Top -= this.player.Playerspeed;
            }
        }

        private void RightTimer_Tick_1(object sender, EventArgs e)
        {
            if (this.player.PlayerBox.Left + this.player.Playerspeed < mapMaxX)
            {
                int newPlayerLeft = this.player.PlayerBox.Left + this.player.Playerspeed;
                if (!CollidesWithObstacle(newPlayerLeft, this.player.PlayerBox.Top, this.player.PlayerBox.Width, this.player.PlayerBox.Height))
                    this.player.PlayerBox.Left += this.player.Playerspeed;
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


        //Bullet
        List<Bullet> bullets = new List<Bullet>();
        public void shootBullet(string x, string y, string directionX, string directionY)
        {
            Vector2 bulletDirection = new Vector2(float.Parse(directionX), float.Parse(directionY));
            Point startPoint = new Point(int.Parse(x), int.Parse(y));
            var bullet = new Bullet(startPoint, bulletDirection);
            bullet.BulletPictureBox.BringToFront();
            this.Controls.Add(bullet.BulletPictureBox);
            this.mapControl.Controls.Add(bullet.BulletPictureBox);
            bullets.Add(bullet);

            //Limit on client to keep from lagging out
            if (bullets.Count >= 30)
            {
                // Remove the first (oldest) bullet
                if (bullets[0].BulletPictureBox != null)
                {
                    this.mapControl.Controls.Remove(bullets[0].BulletPictureBox);
                    this.Controls.Remove(bullets[0].BulletPictureBox); 
                    bullets[0].BulletPictureBox.Dispose(); 
                }
                bullets.RemoveAt(0); 
            }
        }
        //Bullet End

        private void ServerTimer_Tick(object sender, EventArgs e)
        {
            comm.SendCoordinates(this.player.PlayerBox.Left, this.player.PlayerBox.Top);

            //Send bullet info
            if (this.mapControl.IsShooting() && (DateTime.Now - lastBulletFiredTime) > bulletCooldown)
            {
                Point targetPoint = this.PointToClient(Cursor.Position);
                Vector2 start = new Vector2(this.player.PlayerBox.Location.X, this.player.PlayerBox.Location.Y);
                Vector2 target = new Vector2(targetPoint.X, targetPoint.Y);
                Vector2 direction = Vector2.Normalize(target - start);
                lastBulletFiredTime = DateTime.Now;
                //Bullet start from center of player instead of corner
                int bulletStartX = this.player.PlayerBox.Location.X + this.player.PlayerBox.Width / 2;
                int bulletStartY = this.player.PlayerBox.Location.Y + this.player.PlayerBox.Height / 2;
                comm.SendBulletInfo(bulletStartX, bulletStartY, direction.X, direction.Y);
            }
        }

        private void bulletMovementTimer_Tick(object sender, EventArgs e)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Move();
            }
            
        }
    }
}
