using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Stategy
{
    public interface MoveAlgorithm
    {
        void behaveDiffrentley(string dirrection, Player player, MapControl mapControl);
    }
}
