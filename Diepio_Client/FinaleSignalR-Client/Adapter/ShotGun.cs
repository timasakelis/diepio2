using FinaleSignalR_Client.Decorator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Adapter
{
    public class ShotGun
    {
        public int Speed { get; set; }
        public int Size { get; set; }
        public ShotGun() { }
        public List<IBullet> Shoot(int x, int y, Vector2 Direction, string id)
        {
            List<IBullet> bullets = new List<IBullet>();

            IBullet bullet = new Bullet(id);
            bullet = new BlueBullet(bullet);
            Point startPoint = new Point(x, y);

            bullet.SetTragectory(startPoint, Direction);
            bullets.Add(bullet);

            Direction = RotateVector(Direction, 10);

            IBullet bullet2 = new Bullet(id);
            bullet2 = new BlueBullet(bullet2);


            bullet2.SetTragectory(startPoint, Direction);
            bullets.Add(bullet2);

            return bullets;
        }

        Vector2 RotateVector(Vector2 vector, float angleInDegrees)
        {
            Matrix rotationMatrix = new Matrix();

            rotationMatrix.Rotate(angleInDegrees);

            PointF[] points = { new PointF(vector.X, vector.Y) };
            rotationMatrix.TransformPoints(points);

            return new Vector2(points[0].X, points[0].Y);
        }
    }
}
