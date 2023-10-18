using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Command
{
    public class CommandAWSD : Command
    {
        InputAWSD IAWSD;
        public CommandAWSD(InputAWSD sentIAWSD) 
        { 
            this.IAWSD = sentIAWSD;
        }

        public string Execute(KeyEventArgs e)
        {
            return IAWSD.handleInput(e);
        }
    }
}
