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
        public int mapMinX {  get; set; }
        public int mapMinY { get; set; }
        public int mapMaxX { get; set; }
        public int mapMaxY { get; set; }

        public List<Rectangle> obstacles = new List<Rectangle>();
        Graphics g;
        Pen p = new Pen(Brushes.Blue);
        public bool isShooting;


        public MapControl()
        {
            //this.BackColor = Color.Green;
            this.Size = new Size(800, 600);
            this.BorderStyle = BorderStyle.FixedSingle;
            //obstacles.Add(new Rectangle(100, 100, 400, 10)); // Obstacle at (100, 100) with size 50x50
            //obstacles.Add(new Rectangle(200, 200, 10, 90));
            //obstacles.Add(new Rectangle(500, 300, 10, 200));
            //obstacles.Add(new Rectangle(100, 400, 100, 10));

            this.MouseDown += MapControl_MouseDown;
            this.MouseUp += MapControl_MouseUp;

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Create a graphics object to draw on the map control
            g = e.Graphics;

            // Draw each obstacle in the obstacles list
            foreach (Rectangle obstacle in obstacles)
            {
                g.FillRectangle(Brushes.Gray, obstacle);
            }
        }

        public void SetObstacle(int x, int y, int w, int h) 
        {
            Rectangle o = new Rectangle(x, y, w, h);
            //g.DrawRectangle(p, o); 
            obstacles.Add(o);
        }

        public void SetObstacles(List<Rectangle> obst)
        {
            foreach (Rectangle obstacle in obst)
            {
                g.FillRectangle(Brushes.Gray, obstacle);
                obstacles.Add(obstacle);
            }
        }

        public void MapControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isShooting = true;
            }
        }

        public void MapControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isShooting = false;
            }
        }

        public bool IsShooting()
        {
            return isShooting;
        }

    }
}
