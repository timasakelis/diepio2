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
    public class SizeDecorator : IBullet
    {
        private IBullet bullet;
        public Size Size { get; }
        public string playerid { get; }

        public PictureBox BulletPictureBox => bullet.BulletPictureBox;
        public Vector2 Direction => bullet.Direction;

        public SizeDecorator(IBullet bullet, Size size)
        {
            this.bullet = bullet;
            this.playerid = bullet.playerid;
            this.Size = size;
            this.bullet.BulletPictureBox.Size = size;
        }

        public void Move()
        {
            bullet.Move();
        }
        public void SetTragectory(Point startPosition, Vector2 direction)
        {
            bullet.SetTragectory(startPosition, direction);
        }
    }
}
