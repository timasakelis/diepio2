using FinaleSignalR_Client.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnitTests2
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        public void Command_TestInput_AWSD()
        {
            InputControl inputControl = new InputControl();

            inputControl.SwitchToAWSD();

            KeyEventArgs e = new KeyEventArgs(Keys.None | (Keys)Enum.Parse(typeof(Keys), "A"));
            string direction = inputControl.inputDetected(e);
            Assert.AreEqual("left", direction);
        }

        [TestMethod]
        public void Command_TestInput_ArrowKeys()
        {
            InputControl inputControl = new InputControl();

            inputControl.SwitchToArrowKeys();

            KeyEventArgs e = new KeyEventArgs(Keys.None | (Keys)Enum.Parse(typeof(Keys), "Left"));
            string direction = inputControl.inputDetected(e);
            Assert.AreEqual("left", direction);
        }

        [TestMethod]
        public void Command_Undo_Undo3Commands()
        {
            InputControl inputControl = new InputControl();

            inputControl.SwitchToAWSD();
            inputControl.SwitchToArrowKeys();
            inputControl.SwitchToAWSD();

            KeyEventArgs e = new KeyEventArgs(Keys.None | (Keys)Enum.Parse(typeof(Keys), "A"));
            string direction = inputControl.inputDetected(e);
            Assert.AreEqual("left", direction);

            inputControl.UndoSwitch();

            e = new KeyEventArgs(Keys.None | (Keys)Enum.Parse(typeof(Keys), "D"));
            direction = inputControl.inputDetected(e);
            Assert.AreNotEqual("right", direction);

            inputControl.UndoSwitch();

            e = new KeyEventArgs(Keys.None | (Keys)Enum.Parse(typeof(Keys), "W"));
            direction = inputControl.inputDetected(e);
            Assert.AreEqual("up", direction);

        }

    }
}
