using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Prototype
{
    public class LvlUp
    {
        public int Playerspeed { get; set; }
        public int MaxHP { get; set; }

        public LvlUp() 
        {
            this.Playerspeed = 1;
            this.MaxHP = 5;
        }
    }
}
