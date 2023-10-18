using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Command
{
    public class InputArrowKeys
    {
        public string handleInput(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                return "left";
            }
            if (e.KeyCode == Keys.Right)
            {
                return "right";
            }
            if (e.KeyCode == Keys.Up)
            {
                return "up";
            }
            if (e.KeyCode == Keys.Down)
            {
                return "down";
            }
            return "";
        }
    }
}
