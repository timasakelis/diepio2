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
    public class BlueBullet : BulletDecorator
    {

        private PictureBox blueBullet;

        public BlueBullet(IBullet bullet) : base(bullet)
        {
            blueBullet = base.GetPictureBox();
            blueBullet.BackColor = Color.Blue;
        }

        public override PictureBox GetPictureBox()
        {

            return blueBullet;
        }

    }

}

