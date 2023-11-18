using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Stategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.State
{
    public class FullTank : PlayerState
    {
        public override void Move(string dirrection, Map mapControl)
        {
            if (player.CurrentHP < player.MaxHP / 2)
            {
                player.TransitionTo(new HurtTank());
            }
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
