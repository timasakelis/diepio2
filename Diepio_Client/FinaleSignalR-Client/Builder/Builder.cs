using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Builder
{
    public abstract class Builder
    {
        public abstract ColorPal Build();
        public abstract Builder SetSquarePelletColor();
        public abstract Builder SetTrianglePelletColor();
    }
}
