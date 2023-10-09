using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Objects
{
    public class Player
    {
        public string Id { get; set; }
        public int Playerspeed { get; set; }
        PictureBox PlayerBox { get; set; }
        public Player() { }
    }
}
