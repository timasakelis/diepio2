using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Composite
{
    public abstract class Node
    {
        public Button button;
        public string name;
        public Node() { }

        public virtual void AddNode(Node node) { }
        public virtual Node getNode(int i) { return null; }
        public virtual void EnableNodes() { }

        public abstract bool IsLeaf();

        public virtual void Activate()
        {
            this.button.Visible = true;
            button.Enabled = true;
        }

        public void DeActivate()
        {
            this.button.Visible = false;
            this.button.Enabled = false;
        }
    }
}
