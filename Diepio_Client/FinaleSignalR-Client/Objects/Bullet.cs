using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Objects
{
    public class Bullet
    {
        public PictureBox BulletPictureBox { get; set; }
        public Vector2 Direction { get; set; }
        public float Speed { get; set; } = 10;

        public Bullet(Point startPosition, Vector2 direction)
        {
            BulletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = Color.Red,
                Location = startPosition,
            };
            Direction = direction;
        }

        public void Move()
        {
            BulletPictureBox.Left += (int)(Direction.X * Speed);
            BulletPictureBox.Top += (int)(Direction.Y * Speed);
        }
    }
}
