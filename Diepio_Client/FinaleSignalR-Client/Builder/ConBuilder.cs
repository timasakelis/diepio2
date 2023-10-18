using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Builder
{
    public class ConBuilder : Builder
    {
        private ColorPal colors = new ColorPal();

        public override ColorPal Build()
        {
            return colors;
        }
        public override Builder SetSquarePelletColor(Color color)
        {
            colors.squarePelletColor = color;
            return this;
        }

        public override Builder SetTrianglePelletColor(Brush color)
        {
            colors.trianglePelletColor = color;
            return this;
        }
    }
}
