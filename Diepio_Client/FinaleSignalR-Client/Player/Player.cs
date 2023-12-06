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
using FinaleSignalR_Client.Adapter;
using System.Numerics;
using FinaleSignalR_Client.ChainOfResp;

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
        public int lvl { get; set; }
        int exp;
        private StatHandler levelUpHandler;

        //--------------Bridge--------------------
        protected IWepon weapon;
        
        //-----------------State------------------------
        private PlayerState _state;
        //----------------------------------------
        private Color color;
        private Point startingPoint;
        public Player() { }
        public Player(string id, string name, Color color, Point startingPoint, IWepon wepon) {

            this.Id = id;
            this.Name = name;
            this.weapon = wepon; // Default weapon
            this.lvl = 0;
            this.exp = 0;
            var healHandler = new HealHandler();
            this.levelUpHandler = new StatHandler(healHandler);

            this.PlayerBox = new PictureBox { 
                BackColor = color,
                Location = startingPoint,
                Name = id,
            };
            this.PlayerBox.TabIndex = 0;
            this.PlayerBox.TabStop = false;

        }

        public void TransitionTo(PlayerState state)
        {
            this._state = state;
            _state.setContexct(this);
        }


        public virtual void Move(string dirrection, Map mapControl)
        {
            _state.Move(dirrection, mapControl);
        }

        public virtual List<IBullet> Fire(int x, int y, Vector2 Direction, string id)
        {
            return this.weapon.Fire(x, y, Direction, id);
        }

        public virtual void Buff(string type)
        {
            switch(type)
            {
                case "speed":
                    this.weapon.SpeedBoost();
                    break;
                case "size":
                    this.weapon.SizeBoost();
                    break;
                case "default":
                    this.weapon.Default();
                    break;
            }
        }

        public virtual int GetWeopenSpeed()
        {
            return this.weapon.Speed;
        }

        public virtual int GetWeopenSize()
        {
            return this.weapon.Size;
        }


        public void LvlUp()
        {
            this.exp++;
            if (exp > (lvl * 1 + 1))
            {
                this.lvl++;
                this.levelUpHandler.HandleLevelUp(this);
                if (this.lvl%2 == 0)
                {
                    this.PlayerBox.BackColor = Color.Green;
                } else
                {
                    this.PlayerBox.BackColor = Color.Brown;
                }
            }
        }

        public virtual void TakeDamage(int damage)
        {
            this.CurrentHP -= damage;
        }

    }
}
