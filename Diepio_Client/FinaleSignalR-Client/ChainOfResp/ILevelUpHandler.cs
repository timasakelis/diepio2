using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.ChainOfResp
{
    public interface ILevelUpHandler
    {
        void HandleLevelUp(Player player);
    }
}
