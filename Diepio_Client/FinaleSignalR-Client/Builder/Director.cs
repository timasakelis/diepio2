using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Builder
{
    internal class Director
    {
        private Builder builder;
        public Director(Builder builder)
        {
            this.builder = builder;
        }

        public ColorPal Construct()
        {
            return builder.SetSquarePelletColor().SetTrianglePelletColor().Build();
        }
    }
}
