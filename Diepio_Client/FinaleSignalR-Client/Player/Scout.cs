using FinaleSignalR_Client.Adapter;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.State;
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
        public Scout(string id, string name, Color color, Point startingPoint, IWepon wepon) : base(id, name, color, startingPoint, wepon)
        {
            base.Playerspeed = 5;
            base.PlayerBox.Size = new Size(30, 30);
            base.MaxHP = 35;
            base.CurrentHP = 35;
            TransitionTo(new FullScout());
        }
    }
}
