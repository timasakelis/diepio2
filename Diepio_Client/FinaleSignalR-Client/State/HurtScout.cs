﻿using FinaleSignalR_Client.Controls;
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
    public class HurtScout : PlayerState
    {
        public override void Move(string dirrection,  Map mapControl)
        {
            if (player.CurrentHP < player.MaxHP * 0.30)
            {
                player.TransitionTo(new DyingScout());
            }
            else if (player.CurrentHP > player.MaxHP * 0.75)
            {
                player.TransitionTo(new FullScout());
            }

            switch (dirrection)
            {
                case "up":
                    if (player.PlayerBox.Top - player.Playerspeed / 2 > mapControl.mapMinY)
                    {
                        int newPlayerTop = player.PlayerBox.Top - player.Playerspeed / 2;

                        // Check if the new position collides with any obstacle
                        if (!CollidesWithObstacle(player.PlayerBox.Left, newPlayerTop, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))
                            player.PlayerBox.Top -= player.Playerspeed / 2;
                    }
                    break;
                case "down":
                    if (player.PlayerBox.Top + player.Playerspeed / 2 < mapControl.mapMaxY)
                    {
                        int newPlayerTop = player.PlayerBox.Top + player.Playerspeed / 2;

                        // Check if the new position collides with any obstacle
                        if (!CollidesWithObstacle(player.PlayerBox.Left, newPlayerTop, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))
                            player.PlayerBox.Top += player.Playerspeed / 2;
                    }
                    break;
                case "left":
                    if (player.PlayerBox.Left - player.Playerspeed / 2 > mapControl.mapMinX)
                    {
                        int newPlayerLeft = player.PlayerBox.Left - player.Playerspeed / 2;

                        // Check if the new position collides with any obstacle
                        if (!CollidesWithObstacle(newPlayerLeft, player.PlayerBox.Top, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))

                            player.PlayerBox.Left -= player.Playerspeed / 2;
                    }
                    break;
                case "right":
                    if (player.PlayerBox.Left + player.Playerspeed / 2 < mapControl.mapMaxX)
                    {
                        int newPlayerLeft = player.PlayerBox.Left + player.Playerspeed / 2;
                        if (!CollidesWithObstacle(newPlayerLeft, player.PlayerBox.Top, player.PlayerBox.Width, player.PlayerBox.Height, mapControl))
                            player.PlayerBox.Left += player.Playerspeed / 2;
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
