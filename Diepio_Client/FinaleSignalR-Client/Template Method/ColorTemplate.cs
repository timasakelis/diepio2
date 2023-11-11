using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Template_Method
{
    public abstract class ColorTemplate
    {
        private ColorPal colors;
        public ColorPal PrepareColors()
        {
            colors = new ColorPal();
            SetSquarePelletColor(colors);
            SetTrianglePelletColor(colors);
            return colors;
        }
        public abstract ColorPal SetSquarePelletColor(ColorPal colors);
        public abstract ColorPal SetTrianglePelletColor(ColorPal colors);
    }
}
