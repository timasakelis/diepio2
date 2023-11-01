using FinaleSignalR_Client.Adapter;
using FinaleSignalR_Client.Decorator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace UnitTests2
{
    /// <summary>
    /// Summary description for AdapterTest
    /// </summary>
    [TestClass]
    public class AdapterTest
    {
        [TestMethod]
        public void Adapter_Create_ShotgunAdapter()
        {
            IWepon wepon = new ShotgunAdapt(new ShotGun());

            Assert.AreEqual(wepon.Speed, 2);
        }

        [TestMethod]
        public void Adapter_Shoot_ShotgunAdapter()
        {
            IWepon wepon = new ShotgunAdapt(new ShotGun());
            List<IBullet> bullet = wepon.Fire(0, 0, new Vector2(1, 1), "");

            Assert.AreEqual(bullet.Count, 2);
        }
    }
}
