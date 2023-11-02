using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Builder
{
    public class Con2Builder : Builder
    {
        private ColorPal colors = new ColorPal();

        public override ColorPal Build()
        {
            return colors;
        }
        public override Builder SetSquarePelletColor()
        {
            colors.squarePelletColor = Color.DarkOliveGreen;
            return this;
        }

        public override Builder SetTrianglePelletColor()
        {
            colors.trianglePelletColor = Brushes.AliceBlue;
            return this;
        }
    }
}
