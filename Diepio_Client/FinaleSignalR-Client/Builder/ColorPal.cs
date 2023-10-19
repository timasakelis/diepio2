using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Builder
{
    public class ColorPal
    {
        public Brush wallColor {  get; set; }
        public Color squarePelletColor { get; set; }
        public Brush trianglePelletColor { get; set; }
        //public Brush octagonPelletColor { get; set; }
        public ColorPal()
        {
            wallColor = Brushes.Gray;
            squarePelletColor = Color.Green;
            trianglePelletColor = Brushes.Yellow;
            //octagonPelletColor = Brushes.Purple;
        }
    }
}
