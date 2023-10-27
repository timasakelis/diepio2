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
        }

        public void SpeedBoost()
        {
            if (this.ShotGun.Speed < 5)
                this.ShotGun.Speed++;
        }

        public void SizeBoost()
        {
            if (this.ShotGun.Size < 10)
                this.ShotGun.Size++;
        }

        public void Default()
        {
            this.ShotGun.Size = 0;
            this.ShotGun.Speed = 0;
        }

        public List<IBullet> Fire(int x, int y, Vector2 Direction, string id)
        {
            List<IBullet> bullets = this.ShotGun.Shoot(x, y, Direction, id);

            return bullets;
        }
    }
}
