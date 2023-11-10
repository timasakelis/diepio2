﻿using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Stategy
{
    public class LowHPTank : MoveAlgorithm
    {
        public void behaveDiffrentley(string dirrection, Player player, Map mapControl)
        {

            switch (dirrection)
            {
                case "up":
                    if (player.PlayerBox.Top - player.Playerspeed/2 > mapControl.mapMinY)
                    {
                        player.PlayerBox.Top -= player.Playerspeed / 2;
                    }
                    break;
                case "down":
                    if (player.PlayerBox.Top + player.Playerspeed / 2 < mapControl.mapMaxY)
                    {
                        player.PlayerBox.Top += player.Playerspeed / 2;
                    }
                    break;
                case "left":
                    if (player.PlayerBox.Left - player.Playerspeed / 2 > mapControl.mapMinX)
                    {
                        player.PlayerBox.Left -= player.Playerspeed / 2;
                    }
                    break;
                case "right":
                    if (player.PlayerBox.Left + player.Playerspeed / 2 < mapControl.mapMaxX)
                    {
                        player.PlayerBox.Left += player.Playerspeed / 2;
                    }
                    break;
            }
        }
    }
}
