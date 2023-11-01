using FinaleSignalR_Client.Decorator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using System.Numerics;

namespace FinalSignalR_Client.UnitTests
{
    [TestClass]
    public class BulletTests
    {
        [TestMethod]
        public void Bullet_SetTrajectory_SetsLocationAndDirection()
        {
            // Arrange
            IBullet bullet = new Bullet("player1");
            Point startPosition = new Point(0, 0);
            Vector2 direction = new Vector2(1, 1);

            // Act
            bullet.SetTragectory(startPosition, direction);
            // Assert

            Assert.AreEqual(startPosition, bullet.GetPictureBox().Location);
            Assert.AreEqual(direction, bullet.Direction);
        }

        [TestMethod]
        public void SizeDecorator_GetSize_AdjustedSize()
        {
            // Arrange
            IBullet bullet = new Bullet("player1");
            int magnitude = 5;
            IBullet decoratedBullet = new SizeDecorator(bullet, magnitude);

            // Act
            Size size = decoratedBullet.GetSize();

            // Assert
            Assert.AreEqual(new Size(10 + magnitude, 10 + magnitude), size);
        }

        [TestMethod]
        public void SpeedDecorator_GetSpeed_IncreasedSpeed()
        {
            // Arrange
            IBullet bullet = new Bullet("player1");
            IBullet decoratedBullet = new SpeedDecorator(bullet);

            // Act
            float speed = decoratedBullet.GetSpeed();

            // Assert
            Assert.AreEqual(7, speed); // Assuming base speed is 6
        }
    }
}
