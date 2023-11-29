using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Stategy;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.State
{
    public class FullScout : PlayerState
    {
        public override void Move(string dirrection,  Map mapControl)
        {
            if (player.CurrentHP < player.MaxHP * 0.75 && player.CurrentHP > player.MaxHP * 0.30)
            {
                player.TransitionTo(new HurtScout());
            }
            else if (player.CurrentHP < player.MaxHP * 0.30)
            {
                player.TransitionTo(new DyingScout());
            }
            switch (dirrection)
            {
                case "up":
                    if (player.PlayerBox.Top - player.Playerspeed > mapControl.mapMinY)
                    {
                        int newPlayerTop = player.PlayerBox.Top - player.Playerspeed;

                        // Check if the new position collides with any obstacle
                        if (!CollidesWithObstacle(player.PlayerBox.Left, newPlayerTop, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))
                            player.PlayerBox.Top -= player.Playerspeed;
                    }
                    break;
                case "down":
                    if (player.PlayerBox.Top + player.Playerspeed < mapControl.mapMaxY)
                    {
                        int newPlayerTop = player.PlayerBox.Top + player.Playerspeed;

                        // Check if the new position collides with any obstacle
                        if (!CollidesWithObstacle(player.PlayerBox.Left, newPlayerTop, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))
                            player.PlayerBox.Top += player.Playerspeed;
                    }
                    break;
                case "left":
                    if (player.PlayerBox.Left - player.Playerspeed > mapControl.mapMinX)
                    {
                        int newPlayerLeft = player.PlayerBox.Left - player.Playerspeed;

                        // Check if the new position collides with any obstacle
                        if (!CollidesWithObstacle(newPlayerLeft, player.PlayerBox.Top, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))

                            player.PlayerBox.Left -= player.Playerspeed;
                    }
                    break;
                case "right":
                    if (player.PlayerBox.Left + player.Playerspeed < mapControl.mapMaxX)
                    {
                        int newPlayerLeft = player.PlayerBox.Left + player.Playerspeed;
                        if (!CollidesWithObstacle(newPlayerLeft, player.PlayerBox.Top, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))
                            player.PlayerBox.Left += player.Playerspeed;
                    }
                    break;
            }
        }

        private bool CollidesWithObstacle(int x, int y, int width, int height, Map mapControl)
        {
            Rectangle playerRect = new Rectangle(x, y, width, height);

            foreach (Wall obstacle in mapControl.obstacles)
            {
                if (playerRect.IntersectsWith(obstacle.Bounds))
                {
                    return true; // Collision detected
                }
            }

            return false; // No collision detected
        }
    }
}
