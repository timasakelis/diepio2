using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Command
{
    public class SwitchToAWSDCommand : ISwitchCommand
    {
        private InputControl _inputControl;
        public Command _previousCommand;
        private InputAWSD inputAWSD;

        public SwitchToAWSDCommand(InputControl inputControl, Command previousCommand, InputAWSD inputAWSD)
        {
            _inputControl = inputControl;
            _previousCommand = previousCommand;
            this.inputAWSD = inputAWSD;
        }

        public void Execute()
        {
            // Switch to AWSD input handling
            _inputControl.setCommand(new CommandAWSD(inputAWSD));
        }

        public void Undo()
        {
            // Undo the switch (switch back to previous state)
            _inputControl.setCommand(_previousCommand);
        }
    }
}
