using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Stategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Bridge
{
    public class TankBehavior : IInteractioBehavior
    {
        private MoveAlgorithm moveAlgorithm;
        public void Move(string dirrection, Player player, Map mapControl)
        {
            if (player.CurrentHP>player.MaxHP/2)
            {
                moveAlgorithm = new TankMove();
            }
            else
            {
                moveAlgorithm = new LowHPTank();
            }
            moveAlgorithm?.behaveDiffrentley(dirrection,player,mapControl);
        }

    }
}
