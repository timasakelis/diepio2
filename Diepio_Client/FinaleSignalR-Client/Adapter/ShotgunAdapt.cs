using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FinaleSignalR_Client.Decorator;

namespace FinaleSignalR_Client.Adapter
{
    public class ShotgunAdapt : IWepon
    {
        public int Speed { get; set; }
        public int Size { get; set; }
        ShotGun ShotGun { get; set; }

        public ShotgunAdapt(ShotGun gun)
        {
            this.ShotGun = gun;
            this.Speed = 2;
        }

        public void SpeedBoost()
        {
            if (this.Speed < 5)
            {
                this.Speed++;
                Refresh();
            }
        }

        public void SizeBoost()
        {
            if (this.Size < 8)
            {
                this.Size++;
                Refresh();
            }
        }

        public void Default()
        {
            this.Size = 0;
            this.Speed = 0;
            Refresh();
        }
        public void Refresh()
        {
            this.Size = this.Size;
            this.Speed = this.Speed;
        }
        public List<IBullet> Fire(int x, int y, Vector2 Direction, string id)
        {
            List<IBullet> bullets = this.ShotGun.Shoot(x, y, Direction, id);
                
            return bullets;
        }
    }
}
