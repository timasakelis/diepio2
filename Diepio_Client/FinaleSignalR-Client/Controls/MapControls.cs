using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Controls
{
    public class MapControl : Panel
    {
        public List<Rectangle> obstacles = new List<Rectangle>();
        public MapControl()
        {
            //this.BackColor = Color.Green;
            this.Size = new Size(800, 600);
            this.BorderStyle = BorderStyle.FixedSingle;
            obstacles.Add(new Rectangle(100, 100, 400, 10)); // Obstacle at (100, 100) with size 50x50
            obstacles.Add(new Rectangle(200, 200, 10, 90));
            obstacles.Add(new Rectangle(500, 300, 10, 200));
            obstacles.Add(new Rectangle(100, 400, 100, 10));

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a graphics object to draw on the map control
            Graphics g = e.Graphics;

            // Draw each obstacle in the obstacles list
            foreach (Rectangle obstacle in obstacles)
            {
                g.FillRectangle(Brushes.Gray, obstacle);
            }
        }

    }
}
