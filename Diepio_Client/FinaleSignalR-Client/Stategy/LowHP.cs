using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Stategy
{
    public class LowHP : MoveAlgorithm
    {
        public void behaveDiffrentley(string dirrection, Player player, Map mapControl)
        {
            player.Playerspeed = 2;
            switch (dirrection)
            {
                case "up":
                    if (player.PlayerBox.Top - player.Playerspeed > mapControl.mapMinY)
                    {
                        player.PlayerBox.Top -= player.Playerspeed;
                    }
                    break;
                case "down":
                    if (player.PlayerBox.Top + player.Playerspeed < mapControl.mapMaxY)
                    {
                        player.PlayerBox.Top += player.Playerspeed;
                    }
                    break;
                case "left":
                    if (player.PlayerBox.Left - player.Playerspeed > mapControl.mapMinX)
                    {
                        player.PlayerBox.Left -= player.Playerspeed;
                    }
                    break;
                case "right":
                    if (player.PlayerBox.Left + player.Playerspeed < mapControl.mapMaxX)
                    {
                        player.PlayerBox.Left += player.Playerspeed;
                    }
                    break;
            }
        }
    }
}
