using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Factory;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Stategy;
using FinaleSignalR_Client.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using System.Drawing.Drawing2D;
using FinaleSignalR_Client.Command;
using FinaleSignalR_Client.Prototype;
using FinaleSignalR_Client.Bridge;



// Afk players dont go away so create Player class and all that jazz

namespace FinaleSignalR_Client
{
    //Pellet

    public partial class Form1 : Form
    {
        public string id;
        int playerCount = 0;
        bool changeColor = false;
        string myClass = "scout";
        Player player;
        public List<Player> players;

        LvlUpPrototype prototype = new LvlUpPrototype();

        public Map mapControl;
        Communication comm;
        InputControl inputControl;
        InputArrowKeys inputArrowKeys;
        InputAWSD inputAWSD;
    
        List<IPellet> pellets = new List<IPellet>();
        PelletFactory pelletFactory = new PelletFactory();

        //Bullet
        List<IBullet> bullets = new List<IBullet>();
        private bool isShooting = false;
        private DateTime lastBulletFiredTime;
        private TimeSpan bulletCooldown = TimeSpan.FromMilliseconds(150);
        List<IBullet> bulletsToRemove = new List<IBullet>();
        List<IPellet> pelletsToRemove = new List<IPellet>();
        //Bullet end

        public Form1()
        {
            InitializeComponent();
            this.playerBoxes = new System.Windows.Forms.PictureBox[50];
            this.players = new List<Player>();

            this.KeyPreview = true;
            this.mapControl = new Map();
            this.Controls.Add(this.mapControl);
            this.mapControl.SendToBack();

            Random rnd = new Random();
            id = rnd.Next(100000).ToString();

            comm = new Communication(messages, this);
            inputControl = new InputControl();
            inputArrowKeys = new InputArrowKeys();
            inputAWSD = new InputAWSD();

            inputControl.setCommand(new CommandArrowKeys(inputArrowKeys));

            this.FormClosing += YourForm_FormClosing;
        }
        private void YourForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Call RemoveAvatar when the form is closing
            if(this.id != null)
                comm.RemoveAvatar(this.id);
        }

        public void createPlayer(string id, string pClass)
        {
            //var newPlayer = new Player(id, "new player", SystemColors.ControlDark, new Point(666, 422));
            //newPlayer.SetStrategy(new ScoutMove());
            Player newPlayer = new Player();
            if (pClass == "scout")
            {
                newPlayer = new Scout(id, "new player", SystemColors.ControlDark, new Point(666, 422), new ScoutBehavior());
            }else if (pClass == "tank")
            {
                newPlayer = new Tank(id, "new player", SystemColors.ControlDark, new Point(666, 422), new TankBehavior());
            }
            

            mapControl.mapMinX = 0;
            mapControl.mapMinY = 0;
            mapControl.mapMaxX = mapControl.Width - newPlayer.PlayerBox.Width;
            mapControl.mapMaxY = mapControl.Height - newPlayer.PlayerBox.Height;

            //adding player to map
            playerBoxes[playerCount] = newPlayer.PlayerBox;
            players.Add(newPlayer);
            this.mapControl.Controls.Add(playerBoxes[playerCount]);
            playerCount++;
        }

        public void RequestAccepted(string pClass)
        {
            createPlayer(id.ToString(), pClass);
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
            comm.ParseMessage(myClass);
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
            if (e.KeyCode == Keys.K)
            {
                inputControl.SwitchToArrowKeys();
            }
            if (e.KeyCode == Keys.L)
            {
                inputControl.SwitchToAWSD();
            }
            if (e.KeyCode == Keys.O)
            {
                inputControl.UndoSwitch();
            }

            string direction = inputControl.inputDetected(e);
            switch (direction)
            {
                case "left":
                    LeftTimer.Start();
                    break;
                case "right":
                    RightTimer.Start();
                    break;
                case "up":
                    UpTimer.Start();
                    break;
                case "down":
                    DownTimer.Start();
                    break;
            }

            if(e.KeyCode == Keys.D1)
            {
                Player foundPlayer = players.FirstOrDefault(player => player.Id == this.id);
                foundPlayer.weapon.SizeBoost();
            }

            if (e.KeyCode == Keys.D2)
            {
                Player foundPlayer = players.FirstOrDefault(player => player.Id == this.id);
                foundPlayer.weapon.SpeedBoost();
            }
            if (e.KeyCode == Keys.D3)
            {
                Player foundPlayer = players.FirstOrDefault(player => player.Id == this.id);
                foundPlayer.weapon.Default();

            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            string direction = inputControl.inputDetected(e);
            switch (direction)
            {
                case "left":
                    LeftTimer.Stop();
                    break;
                case "right":
                    RightTimer.Stop();
                    break;
                case "up":
                    UpTimer.Stop();
                    break;
                case "down":
                    DownTimer.Stop();
                    break;
            }
        }

        private void DownTimer_Tick(object sender, EventArgs e)
        {
            this.player.Move("down", mapControl);
        }

        private void LeftTimer_Tick_1(object sender, EventArgs e)
        {
            this.player.Move("left", mapControl);
        }

        private void UpTimer_Tick_1(object sender, EventArgs e)
        {
            this.player.Move("up", mapControl);
        }

        private void RightTimer_Tick_1(object sender, EventArgs e)
        {
            this.player.Move("right", mapControl);
        }
        //-------------------------------------------------------

        //Bullet
        public void shootBullet(string x, string y, string directionX, string directionY, string playerid, string sentSpeed, string sentSize)
        {
            Vector2 bulletDirection = new Vector2(float.Parse(directionX), float.Parse(directionY));
            Point startPoint = new Point(int.Parse(x), int.Parse(y));
            IBullet bullet = new Bullet(playerid);
            bullet = new BlueBullet(bullet);

            for (int i = 0; i < int.Parse(sentSpeed); i++)
            {
                bullet = new SpeedDecorator(bullet);
            }

            bullet = new SizeDecorator(bullet, int.Parse(sentSize));

            bullet.SetTragectory(startPoint, bulletDirection);
            mapControl.Controls.Add(bullet.GetPictureBox());
            bullets.Add(bullet);

            if (bullets.Count >= 30)
            {
                // Remove the first (oldest) bullet
                if (bullets[0].GetPictureBox() != null)
                {
                    mapControl.Controls.Remove(bullets[0].GetPictureBox());
                    bullets[0].GetPictureBox().Dispose();
                }
                bullets.RemoveAt(0);
            }

        }

        private void LvlUpPlayer(string id)
        {
            foreach (Player pl in this.players)
            {
                if (pl.Id == id)
                {
                    pl.LvlUp(prototype.stats);
                }
            }
        }


        private void bulletMovementTimer_Tick(object sender, EventArgs e)
        {
            foreach (IBullet bullet in bullets)
            {
                MoveBullet(bullet);
             
                foreach (Player pl in this.players)
                {
                    if (pl != null)
                    {//pl.PlayerBox.Name != (bullet as Bullet).playerid &&
                        if (pl.PlayerBox.Name != bullet.playerid && bullet.GetPictureBox().Bounds.IntersectsWith(pl.PlayerBox.Bounds))
                        {
                            pl.TakeDamage(3);
                            /*if (pl.CurrentHP < pl.MaxHP * 0.5)
                                pl.SetStrategy(new TankMove());*/
                            if (pl.CurrentHP < 1)
                            {
                                pl.PlayerBox.BackColor = Color.Red;
                            }
                            bulletsToRemove.Add(bullet);
                            this.mapControl.Controls.Remove(bullet.GetPictureBox());  
                        }
                    }
                }

                foreach (IPellet pellet in pellets)
                {
                    if (bullet.GetPictureBox().Bounds.IntersectsWith(pellet.PelletPictureBox.Bounds))
                    {
                        pellet.HP--;

                        // Remove the bullet when it hits a pellet
                        bulletsToRemove.Add(bullet);
                        this.mapControl.Controls.Remove(bullet.GetPictureBox());

                        // Check if the pellet is destroyed
                        if (pellet.IsDestroyed())
                        {
                            LvlUpPlayer(bullet.playerid);
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
            foreach (IBullet bullet in bulletsToRemove)
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

        public void MoveBullet(IBullet bullet)
        {
            bullet.GetPictureBox().Left += (int)(bullet.Direction.X * bullet.GetSpeed());
            bullet.GetPictureBox().Top += (int)(bullet.Direction.Y * bullet.GetSpeed());
        }
        //Bullet End

        public void createPellet(int id, int x, int y, int type)
        {
            // Check if pellet already exists
            if (pellets.Any(p => p.ID == id))
            {
                return;
            }

            // Use the factory to create a pellet of a specified type.
            IPellet pellet = pelletFactory.CreatePellet(id, x, y, type, changeColor);

            // Add the pellet to UI controls.
            //this.Controls.Add(pellet.PelletPictureBox);
            this.mapControl.Controls.Add(pellet.PelletPictureBox);

            // Add the pellet to the pellet list.
            pellets.Add(pellet);
        }

        public void RemoveEnemy(string toDelete)
        {
            Player pToRemove = players.FirstOrDefault(p => p.Id == toDelete);

            this.mapControl.Controls.Remove(pToRemove.PlayerBox);
            RemoveEnemyBox(toDelete);
            var t = playerBoxes;
            players.Remove(pToRemove);
        }
        private void RemoveEnemyBox(string id)
        {
            for (int i = 0; i < this.playerCount; i++)
            {
                if (playerBoxes[i] != null)
                {
                    if(playerBoxes[i].Name == id)
                    { 
                        for(int j = i; j < this.playerCount; j++)
                        {
                            playerBoxes[j] = playerBoxes[j + 1];
                        }
                    }

                }
                
            }
            this.playerCount--;
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
                comm.SendBulletInfo(bulletStartX, bulletStartY, direction.X, direction.Y, this.player.PlayerBox.Name, player.weapon.Speed.ToString(), player.weapon.Size.ToString());
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            changeColor = true;
        }

        private void ChooseTank_Click(object sender, EventArgs e)
        {
            this.myClass = "tank";
        }

        private void ChooseScout_Click(object sender, EventArgs e)
        {
            this.myClass = "scout";
        }
    }
}
