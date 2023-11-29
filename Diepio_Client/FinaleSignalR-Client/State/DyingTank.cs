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
    public class DyingTank : PlayerState
    {
        public override void Move(string dirrection, Map mapControl)
        {
            if (player.CurrentHP < player.MaxHP * 0.75 && player.CurrentHP > player.MaxHP * 0.30)
            {
                player.TransitionTo(new HurtTank());
            }
            else if (player.CurrentHP > player.MaxHP * 0.75)
            {
                player.TransitionTo(new FullTank());
            }

            switch (dirrection)
            {
                case "up":
                    if (player.PlayerBox.Top - (int)(player.Playerspeed * 0.30) > mapControl.mapMinY)
                    {
                        player.PlayerBox.Top -= (int)(player.Playerspeed * 0.30);
                    }
                    break;
                case "down":
                    if (player.PlayerBox.Top + (int)(player.Playerspeed * 0.30) < mapControl.mapMaxY)
                    {
                        player.PlayerBox.Top += (int)(player.Playerspeed * 0.30);
                    }
                    break;
                case "left":
                    if (player.PlayerBox.Left - (int)(player.Playerspeed * 0.30) > mapControl.mapMinX)
                    {
                        player.PlayerBox.Left -= (int)(player.Playerspeed * 0.30);
                    }
                    break;
                case "right":
                    if (player.PlayerBox.Left + (int)(player.Playerspeed * 0.30) < mapControl.mapMaxX)
                    {
                        player.PlayerBox.Left += (int)(player.Playerspeed * 0.30);
                    }
                    break;
            }
        }
    }
}
