using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Command
{
    public class CommandArrowKeys : Command
    {
        InputArrowKeys IAK;
        public CommandArrowKeys(InputArrowKeys sentIAK) 
        { 
            this.IAK = sentIAK;
        }

        public string Execute(KeyEventArgs e)
        {
            return IAK.handleInput(e);
        }
    }
}
