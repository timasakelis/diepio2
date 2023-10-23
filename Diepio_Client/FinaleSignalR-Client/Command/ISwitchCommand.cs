using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Command
{
    public interface ISwitchCommand
    {
        void Execute();
        void Undo();
    }
}
