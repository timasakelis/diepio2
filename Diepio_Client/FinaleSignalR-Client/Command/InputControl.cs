using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Command
{
    internal class InputControl
    {
        Command slot;
        public InputControl() { }

        public void setCommand(Command command)
        {
            slot = command;
        }

        public string inputDetected(KeyEventArgs e)
        {
            return slot.Execute(e);
        }
    }
}
