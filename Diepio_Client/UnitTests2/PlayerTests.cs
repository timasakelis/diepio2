using FinaleSignalR_Client;
using FinaleSignalR_Client.Adapter;
using FinaleSignalR_Client.Bridge;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Stategy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTests2
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Player_ExecuteLowHPStrategy_MoveWithReducedSpeed()
        {
            // Arrange

            // Map map = new Map(new DesertThemeFactory());
            Map map = new Map(new ArcticThemeFactory());

            Player player = new Tank("1", "test player",  SystemColors.ControlDark, new Point(666, 422), new TankBehavior(), new ShotgunAdapt(new ShotGun()));
            int exSpeed = player.Playerspeed/2;
            player.TakeDamage(player.CurrentHP);
            // Act
            var oldLoc = player.PlayerBox.Location;
            player.Move("up", map); // Choose a direction

            // Assert
            Assert.AreEqual(exSpeed, player.Playerspeed); // LowHP strategy reduces speed
            Assert.AreNotEqual(oldLoc, player.PlayerBox.Location);
        }

        [TestMethod]
        public void Player_ExecuteHighHPStrategy_MoveWithNormalSpeedWithoutCollision()
        {
            // Arrange

            // Map map = new Map(new DesertThemeFactory());
            Map map = new Map(new ArcticThemeFactory());

            Player player = new Tank("1", "test player", SystemColors.ControlDark, new Point(666, 422), new TankBehavior(), new ShotgunAdapt(new ShotGun()));
            int exSpeed = player.Playerspeed;
            // Act
            var oldLoc = player.PlayerBox.Location;
            player.Move("up", map); // Choose a direction

            // Assert
            Assert.AreEqual(exSpeed, player.Playerspeed); // HighHP strategy does not reduce speed
            Assert.AreNotEqual(oldLoc, player.PlayerBox.Location); // HighHP strategy does not reduce speed
                                                          // You may add more assertions for movement and collision logic if needed
        }

        //[TestMethod]
        //public void Player_ExecuteHighHPStrategy_MoveWithCollision()
        //{

        //    //Arrange
        //    Form1 form = new Form1();
        //    string player = "test player";
        //    // Act
        //    form.createPlayer(player);

        //    // Assert

        //    Assert.Equals(form.players[0].Id, player);
        //}
    }
}
