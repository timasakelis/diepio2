using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Stategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Objects
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Playerspeed { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public PictureBox PlayerBox { get; set; }
        public Weapon weapon;
        public Player(Size playerSize, string id, string name, int maxHP, int currentHP, 
            int speed, Color color, Point startingPoint) {
            this.Id = id;
            this.Name = name;
            this.CurrentHP = currentHP;
            this.MaxHP = maxHP;
            this.weapon = new Weapon(Id); // Default weapon
            this.Playerspeed = speed;
            this.PlayerBox = new PictureBox();
            this.PlayerBox.BackColor = color;
            this.PlayerBox.Location = startingPoint;
            this.PlayerBox.Name = id;
            this.PlayerBox.Size = playerSize;

            this.PlayerBox.TabIndex = 0;
            this.PlayerBox.TabStop = false;
        }

        private MoveAlgorithm moveAlgorithm;

        public void SetStrategy(MoveAlgorithm moveType)
        {
            moveAlgorithm = moveType;
        }

        public void ExecuteStrategy(string dirrection, Map mapControl)
        {
            moveAlgorithm?.behaveDiffrentley(dirrection, this, mapControl);
        }


        public void Fire(string x, string y, string directionX, string directionY, string playerid, Map mapControl, List<IBullet> bullets)
        {
            weapon.Fire( x,  y,  directionX,  directionY,  mapControl,  bullets);
        }

    }
}
