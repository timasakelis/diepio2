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
    public class SizeDecorator : BulletDecorator
    {
        //private IBullet bullet;
        //public Size Size { get; }
        //public string playerid { get; }

        //public PictureBox BulletPictureBox => bullet.BulletPictureBox;
        //public Vector2 Direction => bullet.Direction;
        //public string playerid {  get; set; }
        public SizeDecorator(IBullet bullet): base(bullet) { 
            //this.playerid = bullet.playerid;
        }

        public override PictureBox GetPictureBox()
        {
            PictureBox temp = base.GetPictureBox();
            temp.Size = new Size(base.GetPictureBox().Size.Width + 5, base.GetPictureBox().Size.Height + 5);
            return temp;
        }

        public override float GetSpeed()
        {
            return base.GetSpeed();
        }
    }
}
