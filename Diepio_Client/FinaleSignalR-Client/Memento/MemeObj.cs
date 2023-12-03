using FinaleSignalR_Client.Objects;
using Microsoft.Diagnostics.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Memento
{
    internal class MemeObj
    {
        public int x {  get; set; }
        public int y { get; set; }

        public MemeObj(Player p)
        {
            x = p.PlayerBox.Left; 
            y = p.PlayerBox.Top;  
        }
    }
}
