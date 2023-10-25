using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Controls
{
    public interface IMapThemeFactory
    {
        Color GetObstacleColor();
        Color GetBackgroundColor();
        List<Rectangle> GenerateObstacles(int count);
    }

    public class DesertThemeFactory : IMapThemeFactory
    {
        public Color GetObstacleColor()
        {
            return Color.Brown;
        }

        public Color GetBackgroundColor()
        {
            return Color.Khaki;
        }

        public List<Rectangle> GenerateObstacles(int count)
        {
            List<Rectangle> desertObstacles = new List<Rectangle>();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(10, 790);
                int y = random.Next(10, 590);
                desertObstacles.Add(new Rectangle(x, y, 20, 20));
            }
            return desertObstacles;
        }
    }

    public class ArcticThemeFactory : IMapThemeFactory
    {
        public Color GetObstacleColor()
        {
            return Color.SkyBlue;
        }

        public Color GetBackgroundColor()
        {
            return Color.LightBlue;
        }

        public List<Rectangle> GenerateObstacles(int count)
        {
            List<Rectangle> arcticObstacles = new List<Rectangle>();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = random.Next(10, 790);
                int y = random.Next(10, 590);
                arcticObstacles.Add(new Rectangle(x, y, 30, 30));
            }
            return arcticObstacles;
        }
    }

    public class Map : Panel
    {
        public int mapMinX { get; set; }
        public int mapMinY { get; set; }
        public int mapMaxX { get; set; }
        public int mapMaxY { get; set; }
        public List<Rectangle> obstacles = new List<Rectangle>();
        Graphics g;
        Pen p = new Pen(Brushes.Blue);
        public bool isShooting;
        private IMapThemeFactory themeFactory;

        public Map(IMapThemeFactory factory)
        {
            themeFactory = factory;
            this.Size = new Size(800, 600);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.MouseDown += MapControl_MouseDown;
            this.MouseUp += MapControl_MouseUp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            g = e.Graphics;

            // Setting the background color
            g.Clear(themeFactory.GetBackgroundColor());

            foreach (Rectangle obstacle in obstacles)
            {
                g.FillRectangle(new SolidBrush(themeFactory.GetObstacleColor()), obstacle);
            }

        }

        public void SetObstacle(int x, int y, int w, int h)
        {
            Rectangle o = new Rectangle(x, y, w, h);
            obstacles.Add(o);
        }

        public void SetObstacles(List<Rectangle> obst)
        {
            foreach (Rectangle obstacle in obst)
            {
                g.FillRectangle(new SolidBrush(themeFactory.GetObstacleColor()), obstacle);
                obstacles.Add(obstacle);
            }
        }

        public void GenerateAndSetObstacles(int count)
        {
            SetObstacles(themeFactory.GenerateObstacles(count));
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
