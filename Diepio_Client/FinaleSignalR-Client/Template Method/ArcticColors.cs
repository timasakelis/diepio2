using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Template_Method
{
    public class ArcticColors : ColorTemplate
    {
        public override ColorPal SetSquarePelletColor(ColorPal colors)
        {
            colors.squarePelletColor = Color.Blue;
            return colors;
        }

        public override ColorPal SetTrianglePelletColor(ColorPal colors)
        {
            colors.trianglePelletColor = Brushes.AliceBlue;
            return colors;
        }
    }
}
