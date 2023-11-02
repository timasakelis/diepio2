using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinaleSignalR_Client.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Drawing;

namespace UnitTests2
{
    /// <summary>
    /// Summary description for BuilderTests
    /// </summary>
    [TestClass]
    public class BuilderTests
    {
        [TestMethod]
        public void Builder_ConBuilder_SquarePelletColor()
        {
            Builder builder = new ConBuilder();
            Director dir = new Director(builder);

            ColorPal pal = dir.Construct();

            Assert.AreEqual(pal.squarePelletColor, Color.Brown);
        }

        [TestMethod]
        public void Builder_ConBuilder_TrianglePelletColor()
        {
            Builder builder = new ConBuilder();
            Director dir = new Director(builder);

            ColorPal pal = dir.Construct();

            Assert.AreEqual(pal.trianglePelletColor, Brushes.LawnGreen);
        }

        [TestMethod]
        public void Builder_Con2Builder_SquarePelletColor()
        {
            Builder builder = new Con2Builder();
            Director dir = new Director(builder);

            ColorPal pal = dir.Construct();

            Assert.AreEqual(pal.squarePelletColor, Color.DarkOliveGreen);
        }

        [TestMethod]
        public void Builder_Con2Builder_TrianglePelletColor()
        {
            Builder builder = new Con2Builder();
            Director dir = new Director(builder);

            ColorPal pal = dir.Construct();

            Assert.AreEqual(pal.trianglePelletColor, Brushes.AliceBlue);
        }
    }
}
