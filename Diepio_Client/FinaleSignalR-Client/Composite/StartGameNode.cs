using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FinaleSignalR_Client.Composite
{
    public class StartGameNode : Node
    {
        public StartGameNode(Button button)
        {
            this.button = button;
            this.button.Visible = false;
            this.button.Enabled = false;
        }
        public override bool IsLeaf()
        {
            return true;   
        }
    }
}
