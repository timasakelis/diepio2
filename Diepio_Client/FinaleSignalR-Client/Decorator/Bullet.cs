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
    public class BasicBullet : IBullet
    {
        public PictureBox BulletPictureBox { get; set; }
        public Vector2 Direction { get; set; }
        public string playerid { get;}
        public BasicBullet(string playerid){
            BulletPictureBox = new PictureBox{
                Size = new Size(10, 10),
                BackColor = Color.Red
            };
            this.playerid = playerid;
        }
        public void SetTragectory(Point startPosition, Vector2 direction)
        {
            BulletPictureBox.Location = startPosition;
            Direction = direction;
        }

        public void Move()
        {
            BulletPictureBox.Left += (int)(Direction.X * 5);
            BulletPictureBox.Top += (int)(Direction.Y * 5);
        }
    }

}
