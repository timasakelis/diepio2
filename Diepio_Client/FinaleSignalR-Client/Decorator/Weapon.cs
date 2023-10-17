using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Decorator
{
    public class Weapon
    {
        public int Speed { get; set; }
        public int Size { get; set; }
        public Weapon(){}

        public void Fire(string x, string y, string directionX, string directionY, Map mapControl, List<IBullet> bullets, string playerid)
        {
            Vector2 bulletDirection = new Vector2(float.Parse(directionX), float.Parse(directionY));
            Point startPoint = new Point(int.Parse(x), int.Parse(y));
            IBullet bullet = new Bullet(playerid);
            if(this.Size > 0)
            {
                bullet = new SizeDecorator(bullet);
            }
            if (this.Speed > 2)
            {
                bullet = new SpeedDecorator(bullet);
            }
            if (this.Speed > 3)
            {
                bullet = new SpeedDecorator(bullet);
            }
            bullet.SetTragectory(startPoint, bulletDirection);
            //bullet.BulletPictureBox.BringToFront();
            mapControl.Controls.Add(bullet.GetPictureBox());
            bullets.Add(bullet);

            //switch (this.type)
            //{
            //    case "BasicBullet":
            //        IBullet bullet = new Bullet(Bullet.playerid);
            //        bullet.SetTragectory(startPoint, bulletDirection);
            //        //bullet.BulletPictureBox.BringToFront();
            //        mapControl.Controls.Add(bullet.BulletPictureBox);
            //        bullets.Add(bullet);
            //        break;
            //    //case "SizeDecorator":
            //    //    SizeDecorator bullet2 = new SizeDecorator(Bullet, new Size(15, 15));
            //    //    bullet2.SetTragectory(startPoint, bulletDirection);
            //    //    //bullet.BulletPictureBox.BringToFront();
            //    //    mapControl.Controls.Add(bullet2.BulletPictureBox);
            //    //    bullets.Add(bullet2);
            //    //    break;
            //    //case "SpeedDecorator":
            //    //    SpeedDecorator bullet3 = new SpeedDecorator(Bullet, 10);
            //    //    bullet3.SetTragectory(startPoint, bulletDirection);
            //    //    //bullet.BulletPictureBox.BringToFront();
            //    //    mapControl.Controls.Add(bullet3.BulletPictureBox);
            //    //    bullets.Add(bullet3);
            //    //    break;
            //}
            //Limit on client to keep from lagging out
            if (bullets.Count >= 30)
            {
                // Remove the first (oldest) bullet
                if (bullets[0].GetPictureBox() != null)
                {
                    mapControl.Controls.Remove(bullets[0].GetPictureBox());
                    bullets[0].GetPictureBox().Dispose();
                }
                bullets.RemoveAt(0);
            }
        }

        public void SpeedBoost() {
            this.Speed++;
        }
        public void SizeBoost()
        {
            this.Size++;
        }
        public void Default()
        {
            this.Size = 0;
            this.Speed = 0;
        }
    }
}
