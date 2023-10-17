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
    public class SpeedDecorator : BulletDecorator
    {
        //public string playerid { get; set; }
        public SpeedDecorator(IBullet bullet) : base(bullet)
        {
            //this.playerid = bullet.playerid;
        }
        public override PictureBox GetPictureBox()
        {
            return base.GetPictureBox();
        }

        public override float GetSpeed()
        {
            return base.GetSpeed() + 5;
        }
    }
}
