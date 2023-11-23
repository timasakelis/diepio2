using BenchmarkDotNet.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Composite
{
    public class CompositeButton : Node
    {
        private List<Node> nodes = new List<Node>();
        private List<Node> neighbour = new List<Node>();

        public List<Node> Nodes
        {
            get { return nodes; }
        }

        public List<Node> Neighbours
        {
            get { return neighbour; }
        }

        public CompositeButton(string name, Button button) 
        {
            this.name = name;
            this.button = button;
            this.button.Visible = false;
            this.button.Enabled = false;
        }

        public override void AddNode(Node node)
        {
            nodes.Add(node);
        }

        public override Node getNode(int i)
        {
            return nodes[i];
        }

        public void AddNeighbour(Node node)
        {
            neighbour.Add(node);
        }

        public override bool IsLeaf()
        {
            return false;
        }

        public override void EnableNodes()
        {
            this.DeActivate();

            foreach (var node in neighbour)
            {
                node.DeActivate();
            }

            foreach (var node in nodes) 
            {
                node.Activate();
            }
        }

    }
}
