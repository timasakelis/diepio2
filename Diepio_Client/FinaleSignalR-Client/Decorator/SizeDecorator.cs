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
        public int Magnatude {  get; set; }
        public SizeDecorator(IBullet bullet, int magnatude): base(bullet) { 
            Magnatude = magnatude;
        }

        public override PictureBox GetPictureBox()
        {
            PictureBox temp = base.GetPictureBox();
            temp.Size = this.GetSize();
            return temp;
        }

        public override Size GetSize()
        {
            var temp = base.GetSize();
            temp.Width += Magnatude;
            temp.Height += Magnatude;
            return temp;
        }

        public override float GetSpeed()
        {
            return base.GetSpeed();
        }
    }
}
