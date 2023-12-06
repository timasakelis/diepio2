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
using FinaleSignalR_Client.Facade;
using System.Runtime.Remoting.Messaging;
using FinaleSignalR_Client.Adapter;
using FinaleSignalR_Client.Proxy;
using FinaleSignalR_Client.Composite;
using FinaleSignalR_Client.Iterator;
using FinaleSignalR_Client.Mediator;
using FinaleSignalR_Client.Memento;

namespace FinaleSignalR_Client
{

    public partial class Form1 : Form
    {
        Node rootNode;
        Node reset;

        public string id;
        int playerCount = 0;
        bool changeColor = false;
        string myClass = "scout";
        Player player;
        public List<Player> players;
        public MementoMap meme = new MementoMap();
        

        IPrototype prototype = new LvlUpPrototype();

        public Map mapControl;
        
        //Server
        public CommunicationProxy commProxy;
        public int debugLevel = 0; //Sets the amount of messages being sent from proxy

        //Input handling
        InputControl inputControl;
        InputArrowKeys inputArrowKeys;
    
        List<IPellet> pellets = new List<IPellet>();
        PelletFactory pelletFactory = new PelletFactory();

        //Bullet
        BulletAllMediator bulletWallMediator;
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
            CreateCompositeTree();
            this.playerBoxes = new PictureBox[50];
            this.players = new List<Player>();

            this.KeyPreview = true;

            Random rnd = new Random();
            id = rnd.Next(100000).ToString();

            //comm = new CommunicationParser(messages, this);
            commProxy = new CommunicationProxy(this, messages, debugLevel);
            inputControl = new InputControl();
            inputArrowKeys = new InputArrowKeys();

            inputControl.setCommand(new CommandArrowKeys(inputArrowKeys));

            this.FormClosing += YourForm_FormClosing;
        }

        private void CreateCompositeTree()
        {
            rootNode = new CompositeButton("root", this.CreateCharacter);
            CompositeButton scoutNode = new CompositeButton("scout", this.ChooseScout);
            CompositeButton tankNode = new CompositeButton("tank", this.ChooseTank);
            CompositeButton startGameNode = new CompositeButton("start", this.openConnection);
            CompositeButton ArcticNode = new CompositeButton("arctic", this.ArcticColors);
            CompositeButton DesertNode = new CompositeButton("arctic", this.DesertColors);
            StartGameNode ResetNode = new StartGameNode(this.Reset);
            ArcticNode.AddNeighbour(DesertNode);
            DesertNode.AddNeighbour(ArcticNode);
            scoutNode.AddNeighbour(tankNode);
            tankNode.AddNeighbour(scoutNode);
            startGameNode.AddNode(ResetNode);
            ArcticNode.AddNode(startGameNode);
            DesertNode.AddNode(startGameNode);
            scoutNode.AddNode(ArcticNode);
            scoutNode.AddNode(DesertNode);
            tankNode.AddNode(ArcticNode);
            tankNode.AddNode(DesertNode);
            rootNode.AddNode(scoutNode);
            rootNode.AddNode(tankNode);
            reset = ResetNode;

            //CompositeIterator iterator = new CompositeIterator(rootNode);
            //for (Node node = iterator.First(); !iterator.IsDone(); node = iterator.Next())
            //{
            //    if (node is CompositeButton compositeNode)
            //    {
            //        compositeNode.button.Click += (sender, e) => {
            //            NodeClicked(compositeNode);
            //        };
            //    }
            //}

            // Initially, activate only the root node or the first level of options.
            rootNode.Activate();


        }

        private void NodeClicked(CompositeButton node)
        {
            // Deactivate the current node
            node.DeActivate();

            // Activate the clicked node's children
            foreach (var childNode in node.Nodes)
            {
                childNode.Activate();
            }

            // Deactivate the neighbours of the clicked node
            foreach (var neighbourNode in node.Neighbours)
            {
                neighbourNode.DeActivate();
            }
        }

        private void CreateMap(bool desert)
        {
            if (desert)
            {
                this.mapControl = new Map(new DesertThemeFactory());
            } 
            else 
            {
                this.mapControl = new Map(new ArcticThemeFactory());
            }
            mapControl.Visible = false;

            this.Controls.Add(this.mapControl);
            this.mapControl.SendToBack();

            bulletWallMediator = new BulletAllMediator(mapControl.obstacles, bullets,players,pellets);
        }

        private void YourForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Call RemoveAvatar when the form is closing
            if(this.id != null)
                commProxy.RemoveAvatar(this.id);
        }

        public void createPlayer(string id, string pClass)
        {
            Player pToRemove = players.FirstOrDefault(p => p.Id == id);
            if(pToRemove == null)
            {
                Player newPlayer = new Player();
                if (pClass == "scout")
                {
                    newPlayer = new Scout(id, "new player", SystemColors.ControlDark, new Point(666, 422), new Gun());
                }
                else if (pClass == "tank")
                {
                    ShotGun shotgun = new ShotGun();
                    newPlayer = new Tank(id, "new player", SystemColors.ControlDark, new Point(666, 422),  new ShotgunAdapt(shotgun));
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
        }

        public void RequestAccepted(string pClass)
        {
            if (this.player == null)
            {
                createPlayer(id.ToString(), pClass);
                this.player = players[this.playerCount-1];
                ServerTimer.Start();
                bulletMovementTimer.Start();

            }

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
            rootNode = rootNode.getNode(0);
            rootNode.DeActivate();
            mapControl.Visible = true;
            commProxy.ParseMessage(myClass);
        }

        public void StartParsing()
        {
            commProxy.ParseMessage(myClass);
        }

        public void AddMessage(string text)
        {
            messages.Items.Add(text);
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
            commProxy.SendChatMessage(messageInput.Text);
        }
        //------------------------Movement-------------------------------
        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if(this.player != null)
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
                    foundPlayer.Buff("size");
                }

                if (e.KeyCode == Keys.D2)
                {
                    Player foundPlayer = players.FirstOrDefault(player => player.Id == this.id);
                    foundPlayer.Buff("speed");
                }
                if (e.KeyCode == Keys.D3)
                {
                    Player foundPlayer = players.FirstOrDefault(player => player.Id == this.id);
                    foundPlayer.Buff("default");
                }

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
            if(playerid == this.player.Id)
                bullet = new BlueBullet(bullet);

            for (int i = 0; i < int.Parse(sentSpeed); i++)
            {
                bullet = new SpeedDecorator(bullet);
            }


            bullet = new SizeDecorator(bullet, int.Parse(sentSize));

            // This is how it looks with speed 3
            // 3 SizeDecorator(SpeedDecorator(SpeedDecorator(SpeedDecorator(BlueBullet(IBullet()))))).GetSpeed()
            
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

        public void LvlUpPlayer(string id)
        {
            foreach (Player pl in this.players)
            {
                if (pl.Id == id)
                {
                    pl.LvlUp();
                }
            }
        }


        private void bulletMovementTimer_Tick(object sender, EventArgs e)
        {
            bulletWallMediator.Interact(mapControl,bulletsToRemove, this.player, pelletsToRemove,commProxy,rootNode);

            /*ListIterator<IBullet> bulletIterator = new ListIterator<IBullet>(bullets);
            while (!bulletIterator.IsDone())
            {
                IBullet bullet = bulletIterator.CurrentItem();
                bullet.MoveBullet();

                ListIterator<Player> playerIterator = new ListIterator<Player>(players);
                while (!playerIterator.IsDone())
                {
                    Player pl = playerIterator.CurrentItem();
                    if (pl != null && pl.Id != bullet.playerid && bullet.GetPictureBox().Bounds.IntersectsWith(pl.PlayerBox.Bounds))
                    {
                        pl.TakeDamage(3);
                        if (pl.CurrentHP < 0)
                        {
                            if (this.player.Id == pl.Id) // Each player is responsible for themselves
                            {
                                commProxy.RemoveAvatar(this.player.Id);
                                rootNode.EnableNodes(); // This might not work correctly if called outside of UI thread
                            }
                        }
                        bulletsToRemove.Add(bullet);
                        this.mapControl.Controls.Remove(bullet.GetPictureBox());
                    }
                    playerIterator.Next();
                }

                ListIterator<IPellet> pelletIterator = new ListIterator<IPellet>(pellets);
                while (!pelletIterator.IsDone())
                {
                    IPellet pellet = pelletIterator.CurrentItem();
                    if (bullet.GetPictureBox().Bounds.IntersectsWith(pellet.PelletPictureBox.Bounds))
                    {
                        pellet.HP--;
                        bulletsToRemove.Add(bullet);
                        this.mapControl.Controls.Remove(bullet.GetPictureBox());
                        if (pellet.IsDestroyed())
                        {
                            LvlUpPlayer(bullet.playerid);
                            pelletsToRemove.Add(pellet);
                            this.mapControl.Controls.Remove(pellet.PelletPictureBox);
                        }
                    }
                    pelletIterator.Next();
                }

                bulletIterator.Next();
            }
            */
            foreach (IBullet bullet in bulletsToRemove)
            {
                bullets.Remove(bullet);
            }
            bulletsToRemove.Clear();

            foreach (IPellet pellet in pelletsToRemove)
            {
                pellets.Remove(pellet);
            }
            pelletsToRemove.Clear();
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
            if(pToRemove != null)
            {
                this.mapControl.Controls.Remove(pToRemove.PlayerBox);
                RemoveEnemyBox(toDelete);
                //var t = playerBoxes;
                players.Remove(pToRemove);
                if(this.player.Id == toDelete)
                {
                    this.player = null;
                    commProxy.StopConnection();
                }

            }
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
            if (this.player != null)
            {
                commProxy.SendCoordinates(this.player.PlayerBox.Left, this.player.PlayerBox.Top);

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

                    List<IBullet> bullets = this.player.Fire(bulletStartX, bulletStartY, direction, this.player.PlayerBox.Name);

                    foreach (IBullet bullet in bullets)
                    {
                        commProxy.SendBulletInformation(bulletStartX, bulletStartY, bullet.Direction.X, bullet.Direction.Y, this.player.PlayerBox.Name, player.GetWeopenSpeed().ToString(), player.GetWeopenSize().ToString());
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            rootNode = rootNode.getNode(0);
            changeColor = false;
            CreateMap(false);
            rootNode.EnableNodes();
        }

        private void ChooseTank_Click(object sender, EventArgs e)
        {
            rootNode = rootNode.getNode(1);
            this.myClass = rootNode.name;
            rootNode.EnableNodes();
        }

        private void ChooseScout_Click(object sender, EventArgs e)
        {
            rootNode = rootNode.getNode(0);
            this.myClass = rootNode.name;
            rootNode.EnableNodes();
        }

        public List<string> ReturnMessageItems()
        {
            List<string> messagesCopy = new List<string>();

            // Copy the items
            foreach (var item in messages.Items)
            {
                messagesCopy.Add(item.ToString());
            }
            return messagesCopy;
        }

        private void CreateCharacter_Click(object sender, EventArgs e)
        {
            rootNode.EnableNodes();
        }

        private void DesertColors_Click(object sender, EventArgs e)
        {
            rootNode = rootNode.getNode(1);
            changeColor = true;
            CreateMap(true);
            rootNode.EnableNodes();
        }

        private void Reset_Click(object sender, EventArgs e)
        {
            commProxy.ParseMessage(myClass);
            reset.DeActivate();
        }

        //Interpreter moment
        public bool speedChangeEnable = false;
        public bool debugChangeEnable = false;

        public void ChangeSpeed(string level)
        {
            if (speedChangeEnable)
            {
                switch (level)
                {
                    case "zero":
                        player.Playerspeed = 0;
                        break;
                    case "low":
                        player.Playerspeed = 2;
                        break;
                    case "medium":
                        player.Playerspeed = 5;
                        break;
                    case "high":
                        player.Playerspeed = 10;
                        break;
                }
            }
            speedChangeEnable = false;
    }

        public void ChangeDebug(string level)
        {
            if (debugChangeEnable)
            {
                switch (level)
                {
                    case "zero":
                        debugLevel = 0;
                        break;
                    case "low":
                        debugLevel = 1;
                        break;
                    case "medium":
                        debugLevel = 2;
                        break;
                    case "high":
                        debugLevel = 3;
                        break;
                }
            }
            debugChangeEnable = false;
            commProxy.debugLevel = debugLevel;
        }

        public void SavePositions()
        {
            meme.Save(players);
        }

        public void RestorePositions(int index)
        {
            players = meme.Restore(players, index);
        }
    }
}
