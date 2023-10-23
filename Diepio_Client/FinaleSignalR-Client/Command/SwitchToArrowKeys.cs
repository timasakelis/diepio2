using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Command
{
    public class SwitchToArrowKeysCommand : ISwitchCommand
    {
        private InputControl _inputControl;
        private Command _previousCommand;
        private InputArrowKeys inputArrowKeys;

        public SwitchToArrowKeysCommand(InputControl inputControl, Command previousCommand, InputArrowKeys inputArrowKeys)
        {
            _inputControl = inputControl;
            _previousCommand = previousCommand;
            this.inputArrowKeys = inputArrowKeys;
        }

        public void Execute()
        {
            // Switch to Arrow key input handling
            _inputControl.setCommand(new CommandArrowKeys(inputArrowKeys));
        }

        public void Undo()
        {
            // Undo the switch (switch back to previous state)
            _inputControl.setCommand(_previousCommand);
        }
    }
}
