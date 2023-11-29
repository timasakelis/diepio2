using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Objects
{
    public class Wall
    {
        public Rectangle Bounds { get; set; }
        public Wall(Rectangle rec) {
            this.Bounds = rec;
        }
    }
}
