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
            {
                this.ShotGun.Speed++;
                Refresh();
            }
        }

        public void SizeBoost()
        {
            if (this.ShotGun.Size < 8)
            {
                this.ShotGun.Size++;
                Refresh();
            }
        }

        public void Default()
        {
            this.ShotGun.Size = 0;
            this.ShotGun.Speed = 0;
            Refresh();
        }
        public void Refresh()
        {
            this.Size = this.ShotGun.Size;
            this.Speed = this.ShotGun.Speed;
        }
        public List<IBullet> Fire(int x, int y, Vector2 Direction, string id)
        {
            List<IBullet> bullets = this.ShotGun.Shoot(x, y, Direction, id);
                
            return bullets;
        }
    }
}
