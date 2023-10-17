using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Decorator
{
    public  class SpeedDecorator : IBullet
    {
        private IBullet bullet;
        public int Speed { get; }
        public string playerid { get; }
        public PictureBox BulletPictureBox => bullet.BulletPictureBox;
        public Vector2 Direction => bullet.Direction;

        public SpeedDecorator(IBullet bullet, int speed)
        {
            this.bullet = bullet;
            this.Speed = speed;
            this.playerid = bullet.playerid;
        }

        public void Move()
        {
            //bullet.BulletPictureBox.Left += (int)(bullet.Direction.X * 2);
            //bullet.BulletPictureBox.Top += (int)(bullet.Direction.Y * 2);
            bullet.Move();
        }

        public void SetTragectory(Point startPosition, Vector2 direction)
        {
            bullet.SetTragectory(startPosition, direction);
        }
    }
}
