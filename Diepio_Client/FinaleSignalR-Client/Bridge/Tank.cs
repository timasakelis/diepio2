using FinaleSignalR_Client.Objects;
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
        public Tank(string id, string name, Color color, Point startingPoint, IInteractioBehavior behavior) : base(id, name, color, startingPoint, behavior)
        {
            base.Playerspeed = 2;
            base.PlayerBox.Size = new Size(50,50);
            this.Armore = 1;
            base.MaxHP = 50;
            base.CurrentHP = 50;
        }

        public override void TakeDamage(int damage)
        {
            base.CurrentHP -= (damage - Armore);
        }
    }
}
