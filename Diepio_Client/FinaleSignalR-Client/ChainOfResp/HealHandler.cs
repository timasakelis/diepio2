using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.ChainOfResp
{
    public class HealHandler : ILevelUpHandler
    {
        private readonly ILevelUpHandler _nextHandler;

        public HealHandler(ILevelUpHandler nextHandler = null)
        {
            _nextHandler = nextHandler;
        }

        public void HandleLevelUp(Player player)
        {
            if (player.CurrentHP < player.MaxHP)
            {
                player.CurrentHP = player.MaxHP; 
            }

            _nextHandler?.HandleLevelUp(player);
        }
    }
}
