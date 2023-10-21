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
    public class ScoutBehavior : IInteractioBehavior
    {
        private MoveAlgorithm moveAlgorithm;
        public void Move(string dirrection, Player player, Map mapControl)
        {
            if (player.CurrentHP > player.MaxHP / 2)
            {
                moveAlgorithm = new ScoutMove();
            }
            else
            {
                moveAlgorithm = new LowHPScout();
            }
            moveAlgorithm?.behaveDiffrentley(dirrection, player, mapControl);
        }
    }
}
