using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Command
{
    public class InputAWSD
    {
        public string handleInput(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                return "left";
            }
            if (e.KeyCode == Keys.D)
            {
                return "right";
            }
            if (e.KeyCode == Keys.W)
            {
                return "up";
            }
            if (e.KeyCode == Keys.S)
            {
                return "down";
            }
            return "";
        }
    }
}
