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
    public class Bullet : IBullet
    {
        public PictureBox BulletPictureBox = new PictureBox
        {
            Size = new Size(10, 10),
            BackColor = Color.Red
        };
        public Vector2 Direction { get; set; }
        public string playerid { get;}
        public Bullet(string playerid)
        {
            this.playerid = playerid;
        }
        public void SetTragectory(Point startPosition, Vector2 direction)
        {
            BulletPictureBox.Location = startPosition;

            Direction = direction;
        }

        public PictureBox GetPictureBox()
        {
            return BulletPictureBox;
        }

        public Size GetSize()
        {
            BulletPictureBox.Size = new Size(10, 10);
            return BulletPictureBox.Size;
        }

        public float GetSpeed()
        {
            return 5;
        }
    }

}
