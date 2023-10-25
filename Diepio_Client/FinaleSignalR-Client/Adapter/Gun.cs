using FinaleSignalR_Client.Adapter;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Decorator
{
    public class Gun : IWepon
    {
        public int Speed { get; set; }
        public int Size { get; set; }
        public Gun(){}

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

        public List<IBullet> Fire(int x, int y, Vector2 Direction, string id)
        {
            List<IBullet> bullets = new List<IBullet>();

            IBullet bullet = new Bullet(id);
            bullet = new BlueBullet(bullet);
            Point startPoint = new Point(x, y);

            bullet.SetTragectory(startPoint, Direction);

            bullets.Add(bullet);

            return bullets;
        }
    }
}
