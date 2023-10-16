using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Web;
using Microsoft.AspNetCore.SignalR.Client;
using System.Linq;
using System.Drawing.Drawing2D;
using FinaleSignalR_Client.Stategy;



// Afk players dont go away so create Player class and all that jazz

namespace FinaleSignalR_Client
{
    //Pellet
    public interface IPellet
    {
        PictureBox PelletPictureBox { get; }
        int ID { get; }
        int HP { get; set; }
        int EXP { get; set; }
        bool IsDestroyed();
    }

    public class PelletFactory
    {
        public IPellet CreatePellet(int id, int x, int y, int type)
        {
            switch (type)
            {
                case 0:
                    return new SquarePellet(id, x, y);
                case 1:
                    return new TrianglePellet(id, x, y);
                case 2:
                    return new OctagonPellet(id, x, y);
                default:
                    throw new ArgumentException("Invalid pellet type", nameof(type));
            }
        }
    }

    public class SquarePellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public int ID { get; }
        public int HP { get; set; } = 5;  // example value
        public int EXP { get; set; } = 10; // example value

        public SquarePellet(int id, int x, int y)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = Color.Green,
                Location = new Point(x, y),
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }

    public class TrianglePellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public int ID { get; }
        public int HP { get; set; } = 6;  // example value
        public int EXP { get; set; } = 18; // example value

        public TrianglePellet(int id, int x, int y)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(14, 14), // adjusted size
                BackColor = Color.Transparent,
                Location = new Point(x, y),
            };

            PelletPictureBox.Paint += (s, e) =>
            {
                // Points to define the triangle with doubled size
                Point point1 = new Point(7, 0);
                Point point2 = new Point(0, 14);
                Point point3 = new Point(14, 14);

                // Define a polygon (here a triangle) and fill it
                e.Graphics.FillPolygon(Brushes.Yellow, new Point[] { point1, point2, point3 });
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }



    public class OctagonPellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public int ID { get; }
        public int HP { get; set; } = 2;  // example value
        public int EXP { get; set; } = 20; // example value

        public OctagonPellet(int id, int x, int y)
        {
            ID = id;

            PelletPictureBox = new PictureBox
            {
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                Location = new Point(x, y),
            };

            PelletPictureBox.Paint += PelletPictureBox_Paint;
        }

        private void PelletPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Point[] octagonPoints = new Point[]
            {
            new Point(10, 0),
            new Point(20, 5),
            new Point(20, 15),
            new Point(10, 20),
            new Point(0, 15),
            new Point(0, 5),
            new Point(10, 0),
            };

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(octagonPoints);
            g.FillPath(Brushes.Purple, path);
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }



    public partial class Form1 : Form
    {
        public string id;
        int playerCount = 0;
        Player player;
        Player[] players;

        public Map mapControl;
        Communication comm;

        //int mapMinX = 0;
        //int mapMinY = 0;
        //int mapMaxX = 0;
        //int mapMaxY = 0;


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
            this.mapControl = new Map();
            this.Controls.Add(this.mapControl);
            this.mapControl.SendToBack();

            Random rnd = new Random();
            id = rnd.Next(100000).ToString();

            comm = new Communication(messages, this);
        }

        public void createPlayer(string id)
        {
            var newPlayer = new Player(new Size(64, 64), id, "new player", 20, 20, 5, SystemColors.ControlDark, new Point(666, 422));
            newPlayer.SetStrategy(new HighHP());


            mapControl.mapMinX = 0;
            mapControl.mapMinY = 0;
            mapControl.mapMaxX = mapControl.Width - newPlayer.PlayerBox.Width;
            mapControl.mapMaxY = mapControl.Height - newPlayer.PlayerBox.Height;

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
                    return;
                }
            }
        }

        private void sendMessage_Click(object sender, EventArgs e)
        {
            comm.SendChatMessage(messageInput.Text);
        }
        //------------------------Movement-------------------------------
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
            this.player.ExecuteStrategy("down", mapControl);
        }

        private void LeftTimer_Tick_1(object sender, EventArgs e)
        {
            this.player.ExecuteStrategy("left", mapControl);
        }

        private void UpTimer_Tick_1(object sender, EventArgs e)
        {
            this.player.ExecuteStrategy("up", mapControl);
        }

        private void RightTimer_Tick_1(object sender, EventArgs e)
        {
            this.player.ExecuteStrategy("right", mapControl);
        }
        //-------------------------------------------------------

        //Bullet
        List<Bullet> bullets = new List<Bullet>();
        public void shootBullet(string x, string y, string directionX, string directionY, string playerid)
        {
            Vector2 bulletDirection = new Vector2(float.Parse(directionX), float.Parse(directionY));
            Point startPoint = new Point(int.Parse(x), int.Parse(y));
            var bullet = new Bullet(startPoint, bulletDirection, playerid);
            bullet.BulletPictureBox.BringToFront();
            //this.Controls.Add(bullet.BulletPictureBox);
            this.mapControl.Controls.Add(bullet.BulletPictureBox);
            bullets.Add(bullet);

            //Limit on client to keep from lagging out
            if (bullets.Count >= 30)
            {
                // Remove the first (oldest) bullet
                if (bullets[0].BulletPictureBox != null)
                {
                    this.mapControl.Controls.Remove(bullets[0].BulletPictureBox);
                    //this.Controls.Remove(bullets[0].BulletPictureBox);
                    bullets[0].BulletPictureBox.Dispose();
                }
                bullets.RemoveAt(0);
            }
        }
        //Bullet End

        List<IPellet> pellets = new List<IPellet>();
        PelletFactory pelletFactory = new PelletFactory();

        public void createPellet(int id, int x, int y, int type)
        {
            // Check if pellet already exists
            if (pellets.Any(p => p.ID == id))
            {
                return;
            }

            // Use the factory to create a pellet of a specified type.
            IPellet pellet = pelletFactory.CreatePellet(id, x, y, type);

            // Add the pellet to UI controls.
            //this.Controls.Add(pellet.PelletPictureBox);
            this.mapControl.Controls.Add(pellet.PelletPictureBox);

            // Add the pellet to the pellet list.
            pellets.Add(pellet);
        }


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
                comm.SendBulletInfo(bulletStartX, bulletStartY, direction.X, direction.Y, this.player.PlayerBox.Name);
            }
        }

        List<Bullet> bulletsToRemove = new List<Bullet>();
        List<IPellet> pelletsToRemove = new List<IPellet>();
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
                foreach (Player pl in this.players)
                {
                    if (pl != null)
                    {
                        if (pl.PlayerBox.Name != bullet.playerid && bullet.BulletPictureBox.Bounds.IntersectsWith(pl.PlayerBox.Bounds))
                        {
                            pl.CurrentHP--;
                            if (pl.CurrentHP < pl.MaxHP * 0.5)
                                pl.SetStrategy(new LowHP());
                            if (pl.CurrentHP < 1)
                            {
                                pl.PlayerBox.BackColor = Color.Red;
                            }
                            bulletsToRemove.Add(bullet);
                            this.mapControl.Controls.Remove(bullet.BulletPictureBox);  
                        }
                    }
                }

                foreach (IPellet pellet in pellets)
                {
                    if (bullet.BulletPictureBox.Bounds.IntersectsWith(pellet.PelletPictureBox.Bounds))
                    {
                        pellet.HP--;

                        // Remove the bullet when it hits a pellet
                        bulletsToRemove.Add(bullet);
                        this.mapControl.Controls.Remove(bullet.BulletPictureBox);

                        // Check if the pellet is destroyed
                        if (pellet.IsDestroyed())
                        {
                            // Remove the pellet
                            pelletsToRemove.Add(pellet);
                            //this.Controls.Remove(pellet.PelletPictureBox);
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
            foreach (IPellet pellet in pelletsToRemove)
            {
                //To do logic send server
                pellets.Remove(pellet);
            }

        }
    }
}
