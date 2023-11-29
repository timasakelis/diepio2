using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Factory;
using FinaleSignalR_Client.Iterator;
using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Mediator
{
    public class BulletWallMediator : IColitionMediator
    {
        private List<Wall> walls;
        private List<IBullet> bullets;

        public BulletWallMediator(List<Wall> wall, List<IBullet> bullet)
        {
            this.walls = wall;
            this.bullets = bullet;
        }

        public void Interact(Map map, List<IBullet> bulletsToRemove)
        {
            ListIterator<IBullet> bulletIterator = new ListIterator<IBullet>(bullets);
            while (!bulletIterator.IsDone())
            {
                IBullet bullet = bulletIterator.CurrentItem();
                //bullet.MoveBullet();
                ListIterator<Wall> wallIterator = new ListIterator<Wall>(walls);

                while (!wallIterator.IsDone())
                {
                    Wall wl = wallIterator.CurrentItem();
                    if ( bullet.GetPictureBox().Bounds.IntersectsWith(wl.Bounds))
                    {
                        
                        bulletsToRemove.Add(bullet);
                        map.Controls.Remove(bullet.GetPictureBox());
                    }
                    wallIterator.Next();
                }


                bulletIterator.Next();
            }

        }
    }
}
