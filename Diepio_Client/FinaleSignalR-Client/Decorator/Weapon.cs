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
        public IBullet Bullet { get; set; }
        public string type { get; set; }
        public Weapon(string playerid)
        {
            Bullet = new BasicBullet(playerid);
            this.type = Bullet.GetType().Name;
        }

        public void Fire(string x, string y, string directionX, string directionY, Map mapControl, List<IBullet> bullets)
        {
            Vector2 bulletDirection = new Vector2(float.Parse(directionX), float.Parse(directionY));
            Point startPoint = new Point(int.Parse(x), int.Parse(y));
            
            switch (this.type)
            {
                case "BasicBullet":
                    BasicBullet bullet = new BasicBullet(Bullet.playerid);
                    bullet.SetTragectory(startPoint, bulletDirection);
                    //bullet.BulletPictureBox.BringToFront();
                    mapControl.Controls.Add(bullet.BulletPictureBox);
                    bullets.Add(bullet);
                    break;
                case "SizeDecorator":
                    SizeDecorator bullet2 = new SizeDecorator(Bullet, new Size(15, 15));
                    bullet2.SetTragectory(startPoint, bulletDirection);
                    //bullet.BulletPictureBox.BringToFront();
                    mapControl.Controls.Add(bullet2.BulletPictureBox);
                    bullets.Add(bullet2);
                    break;
                case "SpeedDecorator":
                    SpeedDecorator bullet3 = new SpeedDecorator(Bullet, 10);
                    bullet3.SetTragectory(startPoint, bulletDirection);
                    //bullet.BulletPictureBox.BringToFront();
                    mapControl.Controls.Add(bullet3.BulletPictureBox);
                    bullets.Add(bullet3);
                    break;
            }
            //Limit on client to keep from lagging out
            if (bullets.Count >= 30)
            {
                // Remove the first (oldest) bullet
                if (bullets[0].BulletPictureBox != null)
                {
                    mapControl.Controls.Remove(bullets[0].BulletPictureBox);
                    bullets[0].BulletPictureBox.Dispose();
                }
                bullets.RemoveAt(0);
            }
        }

        public void SpeedBoost() {
            this.type = "SpeedDecorator";
            //this.Bullet = new SpeedDecorator(Bullet, 10);
        }
        public void SizeBoost()
        {
            this.type = "SizeDecorator";
            //this.Bullet = new SizeDecorator(Bullet, new Size(15, 15));
        }
        public void Default()
        {
            this.type = "BasicBullet";
            //this.Bullet = new SizeDecorator(Bullet, new Size(15, 15));
        }
    }
}
