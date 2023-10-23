using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Command
{
    public class InputControl
    {
        Command slot;
        Stack<ISwitchCommand> switchHistory = new Stack<ISwitchCommand>();

        public InputControl() { }

        public void setCommand(Command command)
        {
            slot = command;
        }

        public string inputDetected(KeyEventArgs e)
        {
            return slot.Execute(e);
        }

        public void SwitchToAWSD()
        {
            ISwitchCommand switchCommand = new SwitchToAWSDCommand(this, slot, new InputAWSD());
            switchCommand.Execute();
            switchHistory.Push(switchCommand);
        }

        public void SwitchToArrowKeys()
        {
            ISwitchCommand switchCommand = new SwitchToArrowKeysCommand(this, slot, new InputArrowKeys());
            switchCommand.Execute();
            switchHistory.Push(switchCommand);
        }

        public void UndoSwitch()
        {
            if (switchHistory.Count > 0)
            {
                ISwitchCommand switchCommand = switchHistory.Pop();
                switchCommand.Undo();
            }
        }
    }
}
