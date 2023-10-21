using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Bridge
{
    public class Scout : Player
    {
        /*public Scout(string id, string name, Color color, Point startingPoint, IInteractioBehavior behavior) : base(id, name, color, startingPoint)
        {
            base.Playerspeed = 5;
            base.PlayerBox.Size = new Size(30, 30);
            base.MaxHP = 25;
            base.CurrentHP = 25;
        }*/
        public Scout(string id, string name, Color color, Point startingPoint, IInteractioBehavior behavior) : base(id, name, color, startingPoint, behavior)
        {
            base.Playerspeed = 5;
            base.PlayerBox.Size = new Size(30, 30);
            base.MaxHP = 25;
            base.CurrentHP = 25;
        }
    }
}
