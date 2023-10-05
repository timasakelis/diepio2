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
using System.Linq;



// Afk players dont go away so create Player class and all that jazz

namespace FinaleSignalR_Client
{

    public class Bullet
    {
        public PictureBox BulletPictureBox { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; } = 10;

        public Bullet(Point startPosition, Vector2 direction)
        {
            BulletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = Color.Red,
                Location = startPosition,
            };
            Direction = direction;
        }

        public void Move()
        {
            BulletPictureBox.Left += (int)(Direction.X * Speed);
            BulletPictureBox.Top += (int)(Direction.Y * Speed);
        }
    }

    public class Pellet
    {
        public PictureBox PelletPictureBox { get; set; }
        public int ID { get; set; }
        public int HP { get; set; } = 1;
        public int EXP { get; set; } = 10;

        public Pellet(int id, Point startPosition)
        {
            PelletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = Color.Green, // Example color
                Location = startPosition,
            };
            ID = id;
        }

        // Add a method to check if the pellet is destroyed
        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }


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


        //Bullet
        private bool isShooting = false;
        private DateTime lastBulletFiredTime;
        private TimeSpan bulletCooldown = TimeSpan.FromMilliseconds(150);
        //Bullet end

        public Form1()
        {
            InitializeComponent();
            this.playerBoxes = new System.Windows.Forms.PictureBox[50];

            playerspeed = 5;
            this.KeyPreview = true;
            this.mapControl = new MapControl();
            this.Controls.Add(this.mapControl);
            this.mapControl.SendToBack();
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

        List<Pellet> pellets = new List<Pellet>();

        public void createPellet(int id, int x, int y)
        {

            // Check if pellet already exists
            if (pellets.Any(p => p.ID == id))
            {
                return;
            }

            Pellet pellet = new Pellet(id, new Point(x, y));
            this.Controls.Add(pellet.PelletPictureBox);
            this.mapControl.Controls.Add(pellet.PelletPictureBox);
        }


        private void ServerTimer_Tick(object sender, EventArgs e)
        {
            comm.SendCoordinates(playerBox.Left, playerBox.Top);

            //Send bullet info
            if (this.mapControl.IsShooting() && (DateTime.Now - lastBulletFiredTime) > bulletCooldown)
            {
                Point targetPoint = this.PointToClient(Cursor.Position);
                Vector2 start = new Vector2(playerBox.Location.X, playerBox.Location.Y);
                Vector2 target = new Vector2(targetPoint.X, targetPoint.Y);
                Vector2 direction = Vector2.Normalize(target - start);
                lastBulletFiredTime = DateTime.Now;
                //Bullet start from center of player instead of corner
                int bulletStartX = playerBox.Location.X + playerBox.Width / 2;
                int bulletStartY = playerBox.Location.Y + playerBox.Height / 2;
                comm.SendBulletInfo(bulletStartX, bulletStartY, direction.X, direction.Y);
            }
        }

        List<Bullet> bulletsToRemove = new List<Bullet>();
        List<Pellet> pelletsToRemove = new List<Pellet>();
        private void bulletMovementTimer_Tick(object sender, EventArgs e)
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Move();

                //if (bullet.BulletPictureBox.Left < 0 ||
                //    bullet.BulletPictureBox.Right > this.Width ||
                //    bullet.BulletPictureBox.Top < 0 ||
                //    bullet.BulletPictureBox.Bottom > this.Height)
                //{
                //    bulletsToRemove.Add(bullet);
                //    this.Controls.Remove(bullet.BulletPictureBox);
                //    this.mapControl.Controls.Remove(bullet.BulletPictureBox);
                //}

                foreach (Pellet pellet in pellets)
                {
                    if (bullet.BulletPictureBox.Bounds.IntersectsWith(pellet.PelletPictureBox.Bounds))
                    {
                        pellet.HP--;

                        // Remove the bullet when it hits a pellet
                        bulletsToRemove.Add(bullet);
                        this.Controls.Remove(bullet.BulletPictureBox);

                        // Check if the pellet is destroyed
                        if (pellet.IsDestroyed())
                        {
                            // Remove the pellet
                            pelletsToRemove.Add(pellet);
                            this.Controls.Remove(pellet.PelletPictureBox);
                            this.mapControl.Controls.Remove(pellet.PelletPictureBox);

                            // to do: increase tank's EXP when a pellet is destroyed
                        }
                    }
                }
            }

            // Remove out-of-bounds bullets from the list
            foreach (Bullet bullet in bulletsToRemove)
            {
                bullets.Remove(bullet);
            }
            // Remove out-of-bounds bullets from the list
            foreach (Pellet pellet in pelletsToRemove)
            {
                //To do logic send server
                pellets.Remove(pellet);
            }

        }
    }
}
