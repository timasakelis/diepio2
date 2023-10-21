using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Stategy;
using FinaleSignalR_Client.Prototype;
using FinaleSignalR_Client.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinaleSignalR_Client.Bridge;

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
        public Weapon weapon {  get; set; }
        
        protected IInteractioBehavior _implementation;
        private Color color;
        private Point startingPoint;
        public Player() { }
        public Player(string id, string name, Color color, Point startingPoint, IInteractioBehavior implementation) {

            this.Id = id;
            this.Name = name;
            this.weapon = new Weapon(); // Default weapon

            this.PlayerBox = new PictureBox { 
                BackColor = color,
                Location = startingPoint,
                Name = id,
            };
            this.PlayerBox.TabIndex = 0;
            this.PlayerBox.TabStop = false;


            this._implementation = implementation;
        }

        public Player(string id, string name, Color color, Point startingPoint)
        {
            Id = id;
            Name = name;
            this.color = color;
            this.startingPoint = startingPoint;
        }

        public virtual void Move(string dirrection, Map mapControl)
        {
            _implementation.Move(dirrection,this,mapControl);
            //moveAlgorithm?.behaveDiffrentley(dirrection, this, mapControl);
        }

        /*private MoveAlgorithm moveAlgorithm;

        public void SetStrategy(MoveAlgorithm moveType)
        {
            this.moveAlgorithm = moveType;
        }*/

        
        public void LvlUp(LvlUp stats)
        {
            this.Playerspeed = Playerspeed + stats.Playerspeed;
            this.MaxHP = MaxHP + stats.MaxHP;
        }

        public virtual void TakeDamage(int damage)
        {
            this.CurrentHP -= damage;
        }

    }
}
