using FinaleSignalR_Client.Adapter;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.State;
using FinaleSignalR_Client.Stategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Bridge
{
    public class Tank : Player
    {
        public int Armore { get; set; }
        public Tank(string id, string name, Color color, Point startingPoint, IWepon wepon) : base(id, name, color, startingPoint, wepon)
        {
            
            //ShotGun shotgun = new ShotGun();
            base.Playerspeed = 2;
            base.PlayerBox.Size = new Size(50,50);
            this.Armore = 1;
            base.MaxHP = 50;
            base.CurrentHP = 50;
            TransitionTo(new FullTank());
            //base.weapon = new ShotgunAdapt(shotgun);
        }

        public override void TakeDamage(int damage)
        {
            base.CurrentHP -= (damage - Armore);
            
        }
    }
}
