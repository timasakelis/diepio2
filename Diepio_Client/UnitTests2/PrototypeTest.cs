using FinaleSignalR_Client.Prototype;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTests2
{
    [TestClass]
    public class PrototypeTest
    {
        [TestMethod]
        public void Prototype_Clone_MaxHP()
        {
            IPrototype prototype = new LvlUpPrototype();
            IPrototype newProt = prototype.Clone();
            prototype.stats.ChangeHP(2);
            Assert.AreEqual(prototype.stats.MaxHP, newProt.stats.MaxHP);
        }

        [TestMethod]
        public void Prototype_Clone_PlayerSpeed()
        {
            IPrototype prototype = new LvlUpPrototype();
            IPrototype newProt = prototype.Clone();
            prototype.stats.ChangeSpeed(2);
            Assert.AreEqual(prototype.stats.Playerspeed, newProt.stats.Playerspeed);
        }

        [TestMethod]
        public void Prototype_DeepClone_MaxHP()
        {
            IPrototype prototype = new LvlUpPrototype();
            IPrototype newProt = prototype.DeepClone();
            prototype.stats.ChangeHP(2);
            Assert.AreNotEqual(prototype.stats.MaxHP, newProt.stats.MaxHP);
        }

        [TestMethod]
        public void Prototype_DeepClone_PlayerSpeed()
        {
            IPrototype prototype = new LvlUpPrototype();
            IPrototype newProt = prototype.DeepClone();
            prototype.stats.ChangeSpeed(2);
            Assert.AreNotEqual(prototype.stats.Playerspeed, newProt.stats.Playerspeed);
        }
    }
}
