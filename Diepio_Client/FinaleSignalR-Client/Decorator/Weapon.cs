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

        public async void Fire(string x, string y, string directionX, string directionY, Map mapControl, List<IBullet> bullets, string playerid)
        {
            Vector2 bulletDirection = new Vector2(float.Parse(directionX), float.Parse(directionY));
            Point startPoint = new Point(int.Parse(x), int.Parse(y));
            IBullet bullet = new Bullet(playerid);
            for (int i = 0; i < this.Speed; i++)
            {
                bullet = new SpeedDecorator(bullet);
            }
            
            bullet = new SizeDecorator(bullet, this.Size);

            bullet.SetTragectory(startPoint, bulletDirection);
            mapControl.Controls.Add(bullet.GetPictureBox());
            bullets.Add(bullet);

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
            if (this.Speed < 5)
                this.Speed++;
        }
        public void SizeBoost()
        {
            if (this.Size < 10)
                this.Size++;
        }
        public void Default()
        {
            this.Size = 0;
            this.Speed = 0;
        }
    }
}
