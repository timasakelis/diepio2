using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FinaleSignalR_Client.Decorator;

namespace FinaleSignalR_Client.Adapter
{
    public interface IWepon
    {
        int Speed { get; set; }
        int Size { get; set; }
        void SpeedBoost();
        void SizeBoost();
        void Default();
        List<IBullet> Fire(int x, int y, Vector2 Direction, string id);
    }
}
