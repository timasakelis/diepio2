using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Stategy
{
    public abstract class PlayerState
    {
        protected Player player;
        public void setContexct(Player player)
        {
            this.player = player;
        }
        public abstract void Move(string dirrection, Map mapControl);
    }
}
