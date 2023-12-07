using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using FinaleSignalR_Client.Template_Method;

namespace FinaleSignalR_Client.Factory
{
    public interface IPellet
    {
        PictureBox PelletPictureBox { get; }
        int ID { get; }
        int HP { get; set; }
        int EXP { get; set; }
        bool IsDestroyed();
        void Accept(IPelletVisitor visitor);
    }

    public interface IPelletVisitor
    {
        void Visit(SquarePellet pellet);
        void Visit(TrianglePellet pellet);
        void Visit(OctagonPellet pellet);
    }

    public class PelletFactory
    {
        private Dictionary<int, ColorPal> colorCache = new Dictionary<int, ColorPal>();

        public IPellet CreatePellet(int id, int x, int y, int type, bool changeColor)
        {
            if (!colorCache.TryGetValue(type, out ColorPal colors))
            {
                ColorTemplate colorTemplate = changeColor ? (ColorTemplate)new DesertColors() : new ArcticColors();
                colors = colorTemplate.PrepareColors();
                colorCache[type] = colors;
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
        public int HP { get; set; } = 5;
        public int EXP { get; set; } = 10;

        public SquarePellet(int id, int x, int y, ColorPal colors)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(10, 10),
                BackColor = colors.squarePelletColor,
                Location = new Point(x, y),
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }

        public void Accept(IPelletVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class TrianglePellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public int ID { get; }
        public int HP { get; set; } = 6;
        public int EXP { get; set; } = 18;

        public TrianglePellet(int id, int x, int y, ColorPal colors)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(14, 14),
                BackColor = Color.Transparent,
                Location = new Point(x, y),
            };

            PelletPictureBox.Paint += (s, e) =>
            {
                Point point1 = new Point(7, 0);
                Point point2 = new Point(0, 14);
                Point point3 = new Point(14, 14);
                e.Graphics.FillPolygon(colors.trianglePelletColor, new Point[] { point1, point2, point3 });
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }

        public void Accept(IPelletVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class OctagonPellet : IPellet
    {
        public PictureBox PelletPictureBox { get; }
        public int ID { get; }
        public int HP { get; set; } = 2;
        public int EXP { get; set; } = 20;

        public OctagonPellet(int id, int x, int y)
        {
            ID = id;
            PelletPictureBox = new PictureBox
            {
                Size = new Size(20, 20),
                BackColor = Color.Transparent,
                Location = new Point(x, y),
            };

            PelletPictureBox.Paint += (s, e) =>
            {
                Point[] octagonPoints = new Point[]
                {
                    new Point(10, 0), new Point(20, 5),
                    new Point(20, 15), new Point(10, 20),
                    new Point(0, 15), new Point(0, 5),
                    new Point(10, 0)
                };
                GraphicsPath path = new GraphicsPath();
                path.AddPolygon(octagonPoints);
                e.Graphics.FillPath(Brushes.Purple, path);
            };
        }

        public bool IsDestroyed()
        {
            return HP <= 0;
        }

        public void Accept(IPelletVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
