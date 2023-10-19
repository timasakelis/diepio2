using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using FinaleSignalR_Client.Builder;
using System.Reflection;
using System.IO;

namespace FinaleSignalR_Client.Factory
{
    public interface IPellet
    {
        PictureBox PelletPictureBox { get; }
        int ID { get; }
        int HP { get; set; }
        int EXP { get; set; }
        bool IsDestroyed();
    }

    public class PelletFactory
    {
        public IPellet CreatePellet(int id, int x, int y, int type, bool cangeColor)
        {
            ColorPal colors = new ColorPal();
            if (cangeColor)
            {
                Builder.Builder builder = new Builder.ConBuilder();
                Builder.Director director = new Builder.Director(builder);
                colors = director.Construct();
            }
            switch (type)
            {
                case 0:
                    return new SquarePellet(id, x, y, colors);
                case 1:
                    return new TrianglePellet(id, x, y, colors);
                case 2:
                    return new OctagonPellet(id, x, y);
                default:
                    throw new ArgumentException("Invalid pellet type", nameof(type));
            }
        }
    }

    public class SquarePellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public int ID { get; }
        public int HP { get; set; } = 5;  // example value
        public int EXP { get; set; } = 10; // example value

        public SquarePellet(int id, int x, int y, ColorPal collors)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = collors.squarePelletColor,
                Location = new Point(x, y),
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }

    public class TrianglePellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public ColorPalette ColorPalette { get; set; }
        public int ID { get; }
        public int HP { get; set; } = 6;  // example value
        public int EXP { get; set; } = 18; // example value

        public TrianglePellet(int id, int x, int y, ColorPal colors)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(14, 14), // adjusted size
                BackColor = Color.Transparent,
                Location = new Point(x, y),
            };

            PelletPictureBox.Paint += (s, e) =>
            {
                // Points to define the triangle with doubled size
                Point point1 = new Point(7, 0);
                Point point2 = new Point(0, 14);
                Point point3 = new Point(14, 14);

                // Define a polygon (here a triangle) and fill it
                e.Graphics.FillPolygon(colors.trianglePelletColor, new Point[] { point1, point2, point3 });
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }


    public class OctagonPellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public ColorPalette ColorPalette { get; set; }
        public int ID { get; }
        public int HP { get; set; } = 2;  // example value
        public int EXP { get; set; } = 20; // example value

        public OctagonPellet(int id, int x, int y)
        {
            ID = id;

            PelletPictureBox = new PictureBox
            {
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                Location = new Point(x, y),
            };

            PelletPictureBox.Paint += PelletPictureBox_Paint;
        }

        private void PelletPictureBox_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Point[] octagonPoints = new Point[]
            {
            new Point(10, 0),
            new Point(20, 5),
            new Point(20, 15),
            new Point(10, 20),
            new Point(0, 15),
            new Point(0, 5),
            new Point(10, 0),
            };

            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(octagonPoints);
            g.FillPath(Brushes.Purple, path);
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }
    }
}
