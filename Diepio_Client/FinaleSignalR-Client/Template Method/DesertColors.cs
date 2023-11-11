using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Template_Method
{
    public class DesertColors : ColorTemplate
    {
        public override ColorPal SetSquarePelletColor(ColorPal colors)
        {
            colors.squarePelletColor = Color.Brown;
            return colors;
        }

        public override ColorPal SetTrianglePelletColor(ColorPal colors)
        {
            colors.trianglePelletColor = Brushes.Orange;
            return colors;
        }
    }
}
