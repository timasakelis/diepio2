using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Controls
{
    public interface IMapThemeFactory
    {
        IBackgroundColor CreateBackgroundColor();
        IObstacleColor CreateObstacleColor();
        IObstacles GenerateObstacles(int count);
    }

    public interface IBackgroundColor
    {
        Color GetColor();
    }

    public interface IObstacleColor
    {
        Color GetColor();
    }

    public interface IObstacles
    {
        List<Rectangle> GetObstacles();
    }

    // Concrete implementations for Desert Theme
    public class DesertBackgroundColor : IBackgroundColor
    {
        public Color GetColor() => Color.Khaki;
    }

    public class DesertObstacleColor : IObstacleColor
    {
        public Color GetColor() => Color.Brown;
    }

    public class DesertObstacles : IObstacles
    {
        public List<Rectangle> GetObstacles()
        {
            List<Rectangle> desertObstacles = new List<Rectangle>();
            Random random = new Random();
            for (int i = 0; i < 10; i++) // I've set a default count for simplicity.
            {
                int x = random.Next(10, 790);
                int y = random.Next(10, 590);
                desertObstacles.Add(new Rectangle(x, y, 20, 20));
            }
            return desertObstacles;
        }
    }

    public class DesertThemeFactory : IMapThemeFactory
    {
        public IBackgroundColor CreateBackgroundColor() => new DesertBackgroundColor();
        public IObstacleColor CreateObstacleColor() => new DesertObstacleColor();
        public IObstacles GenerateObstacles(int count) => new DesertObstacles();
    }

    // Concrete implementations for Arctic Theme
    public class ArcticBackgroundColor : IBackgroundColor
    {
        public Color GetColor() => Color.LightBlue;
    }

    public class ArcticObstacleColor : IObstacleColor
    {
        public Color GetColor() => Color.SkyBlue;
    }

    public class ArcticObstacles : IObstacles
    {
        public List<Rectangle> GetObstacles()
        {
            List<Rectangle> arcticObstacles = new List<Rectangle>();
            Random random = new Random();
            for (int i = 0; i < 10; i++) // I've set a default count for simplicity.
            {
                int x = random.Next(10, 790);
                int y = random.Next(10, 590);
                arcticObstacles.Add(new Rectangle(x, y, 30, 30));
            }
            return arcticObstacles;
        }
    }

    public class ArcticThemeFactory : IMapThemeFactory
    {
        public IBackgroundColor CreateBackgroundColor() => new ArcticBackgroundColor();
        public IObstacleColor CreateObstacleColor() => new ArcticObstacleColor();
        public IObstacles GenerateObstacles(int count) => new ArcticObstacles();
    }

    // Map implementation
    public class Map : Panel
    {

        public int mapMinX { get; set; }
        public int mapMinY { get; set; }
        public int mapMaxX { get; set; }
        public int mapMaxY { get; set; }
        public List<Rectangle> obstacles { get; private set; } = new List<Rectangle>();
        private IMapThemeFactory themeFactory;
        private Graphics g;
        private Pen p = new Pen(Brushes.Blue);
        private bool isShooting;

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
            g.Clear(themeFactory.CreateBackgroundColor().GetColor());
            foreach (Rectangle obstacle in obstacles)
            {
                g.FillRectangle(new SolidBrush(themeFactory.CreateObstacleColor().GetColor()), obstacle);
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
                g.FillRectangle(new SolidBrush(themeFactory.CreateObstacleColor().GetColor()), obstacle);
                obstacles.Add(obstacle);
            }
        }

        public void GenerateAndSetObstacles(int count)
        {
            SetObstacles(themeFactory.GenerateObstacles(count).GetObstacles());
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
