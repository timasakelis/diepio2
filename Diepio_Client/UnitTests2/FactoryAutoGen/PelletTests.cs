namespace UnitTests2.Factory
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using FinaleSignalR_Client.Builder;
    using FinaleSignalR_Client.Factory;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PelletFactoryTests
    {
        private PelletFactory _testClass;

        [TestInitialize]
        public void SetUp()
        {
            _testClass = new PelletFactory();
        }


        // Fixed this method
        [TestMethod]
        public void CanCallCreatePelletType0()
        {
            // Arrange
            var id = 1091371296;
            var x = 2138376741;
            var y = 1837186450;
            var @type = 0;
            var cangeColor = false;

            // Act
            var result = _testClass.CreatePellet(id, x, y, type, cangeColor);

            // Assert
            Assert.AreEqual(result.HP, 5);
        }
    }

    [TestClass]
    public class SquarePelletTests
    {
        private SquarePellet _testClass;
        private int _id;
        private int _x;
        private int _y;
        private ColorPal _collors;

        [TestInitialize]
        public void SetUp()
        {
            _id = 1716205748;
            _x = 1769409314;
            _y = 147083980;
            _collors = new ColorPal
            {
                wallColor = Brushes.White,
                squarePelletColor = Color.Red,
                trianglePelletColor = Brushes.Magenta
            };
            _testClass = new SquarePellet(_id, _x, _y, _collors);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new SquarePellet(_id, _x, _y, _collors);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void IDIsInitializedCorrectly()
        {
            Assert.AreEqual(_id, _testClass.ID);
        }

        [TestMethod]
        public void CanSetAndGetHP()
        {
            // Arrange
            var testValue = 1109948925;

            // Act
            _testClass.HP = testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.HP);
        }

        [TestMethod]
        public void CanSetAndGetEXP()
        {
            // Arrange
            var testValue = 1698937001;

            // Act
            _testClass.EXP = testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.EXP);
        }
    }

    [TestClass]
    public class TrianglePelletTests
    {
        private TrianglePellet _testClass;
        private int _id;
        private int _x;
        private int _y;
        private ColorPal _colors;

        [TestInitialize]
        public void SetUp()
        {
            _id = 297712749;
            _x = 1611704516;
            _y = 1127592772;
            _colors = new ColorPal
            {
                wallColor = Brushes.Green,
                squarePelletColor = Color.Teal,
                trianglePelletColor = Brushes.Cyan
            };
            _testClass = new TrianglePellet(_id, _x, _y, _colors);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new TrianglePellet(_id, _x, _y, _colors);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void IDIsInitializedCorrectly()
        {
            Assert.AreEqual(_id, _testClass.ID);
        }

        [TestMethod]
        public void CanSetAndGetHP()
        {
            // Arrange
            var testValue = 1863964020;

            // Act
            _testClass.HP = testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.HP);
        }

        [TestMethod]
        public void CanSetAndGetEXP()
        {
            // Arrange
            var testValue = 1700993764;

            // Act
            _testClass.EXP = testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.EXP);
        }
    }

    [TestClass]
    public class OctagonPelletTests
    {
        private OctagonPellet _testClass;
        private int _id;
        private int _x;
        private int _y;

        [TestInitialize]
        public void SetUp()
        {
            _id = 1151770943;
            _x = 1034132637;
            _y = 200359366;
            _testClass = new OctagonPellet(_id, _x, _y);
        }

        [TestMethod]
        public void CanConstruct()
        {
            // Act
            var instance = new OctagonPellet(_id, _x, _y);

            // Assert
            Assert.IsNotNull(instance);
        }

        [TestMethod]
        public void IDIsInitializedCorrectly()
        {
            Assert.AreEqual(_id, _testClass.ID);
        }

        [TestMethod]
        public void CanSetAndGetHP()
        {
            // Arrange
            var testValue = 756611489;

            // Act
            _testClass.HP = testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.HP);
        }

        [TestMethod]
        public void CanSetAndGetEXP()
        {
            // Arrange
            var testValue = 841312841;

            // Act
            _testClass.EXP = testValue;

            // Assert
            Assert.AreEqual(testValue, _testClass.EXP);
        }
    }
}