using FinaleSignalR_Client.Composite;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Factory;
using FinaleSignalR_Client.Iterator;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Prototype;
using FinaleSignalR_Client.Proxy;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Mediator
{
    public class BulletAllMediator : IColitionMediator
    {
        private List<Wall> walls;
        private List<IBullet> bullets;
        private List<Player> players;
        private List<IPellet> pellets;

        public BulletAllMediator(List<Wall> wall, List<IBullet> bullet, List<Player> players, List<IPellet> pellets)
        {
            this.walls = wall;
            this.bullets = bullet;
            this.players = players;
            this.pellets = pellets;
        }

        public void Interact(Map map, List<IBullet> bulletsToRemove, Player player, List<IPellet> pelletsToRemove, 
            CommunicationProxy commProxy, Node rootNode)
        {
            IPrototype prototype = new LvlUpPrototype();
            ListIterator<IBullet> bulletIterator = new ListIterator<IBullet>(bullets);

            while (!bulletIterator.IsDone())
            {
                IBullet bullet = bulletIterator.CurrentItem();
                bullet.MoveBullet();

                ListIterator<Wall> wallIterator = new ListIterator<Wall>(walls);

                while (!wallIterator.IsDone())
                {
                    Wall wl = wallIterator.CurrentItem();
                    if (bullet.GetPictureBox().Bounds.IntersectsWith(wl.Bounds))
                    {

                        bulletsToRemove.Add(bullet);
                        map.Controls.Remove(bullet.GetPictureBox());
                    }
                    wallIterator.Next();
                }

                ListIterator<Player> playerIterator = new ListIterator<Player>(players);
                while (!playerIterator.IsDone())
                {
                    Player pl = playerIterator.CurrentItem();
                    if (pl != null && pl.Id != bullet.playerid && bullet.GetPictureBox().Bounds.IntersectsWith(pl.PlayerBox.Bounds))
                    {
                        pl.TakeDamage(3);
                        if (pl.CurrentHP < 0)
                        {
                            if (player.Id == pl.Id) // Each player is responsible for themselves
                            {
                                commProxy.RemoveAvatar(player.Id);
                                rootNode.EnableNodes(); // This might not work correctly if called outside of UI thread
                            }
                        }
                        bulletsToRemove.Add(bullet);
                        map.Controls.Remove(bullet.GetPictureBox());
                    }
                    playerIterator.Next();
                }

                ListIterator<IPellet> pelletIterator = new ListIterator<IPellet>(pellets);
                while (!pelletIterator.IsDone())
                {
                    IPellet pellet = pelletIterator.CurrentItem();
                    if (bullet.GetPictureBox().Bounds.IntersectsWith(pellet.PelletPictureBox.Bounds))
                    {
                        pellet.HP--;
                        bulletsToRemove.Add(bullet);
                        map.Controls.Remove(bullet.GetPictureBox());
                        if (pellet.IsDestroyed())
                        {
                            var toLv = players.FirstOrDefault(p => p.Id == bullet.playerid);
                            if (toLv != null)
                                toLv.LvlUp(prototype.Clone().stats);
                            pelletsToRemove.Add(pellet);
                            map.Controls.Remove(pellet.PelletPictureBox);
                        }
                    }
                    pelletIterator.Next();
                }

                bulletIterator.Next();
            }

        }
    }
}
