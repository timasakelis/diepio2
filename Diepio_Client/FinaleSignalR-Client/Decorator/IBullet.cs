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
    public interface IBullet
    {
        string playerid { get; }
        Vector2 Direction { get; }

        PictureBox GetPictureBox();
        float GetSpeed();
        Size GetSize();
        void MoveBullet();
        void SetTragectory(Point startPosition, Vector2 direction);

    }
}
