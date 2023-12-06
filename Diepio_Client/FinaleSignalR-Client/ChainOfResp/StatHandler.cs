using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Prototype;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.ChainOfResp
{
    internal class StatHandler
    {
        private readonly ILevelUpHandler _nextHandler;

        public StatHandler(ILevelUpHandler nextHandler = null)
        {
            _nextHandler = nextHandler;
        }

        public void HandleLevelUp(Player player)
        {
            LvlUp lvlUp = new LvlUp();
            switch (player.GetType().ToString())
            {
                case "Scout":
                    lvlUp.Playerspeed = 2;
                    lvlUp.MaxHP = 2;
                    player.Playerspeed =+ lvlUp.Playerspeed;
                    player.MaxHP =+ lvlUp.MaxHP;
                    break;
                case "Tank":
                    player.Playerspeed =+ lvlUp.Playerspeed;
                    player.MaxHP =+ lvlUp.MaxHP;
                    break;
                default:
                    break;  
            }

            _nextHandler?.HandleLevelUp(player);
        }
    }
}
