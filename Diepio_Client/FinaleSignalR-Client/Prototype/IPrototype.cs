﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Prototype
{
    public interface IPrototype
    {
        LvlUp stats { get; }
        IPrototype Clone();

        IPrototype DeepClone();
    }
}
