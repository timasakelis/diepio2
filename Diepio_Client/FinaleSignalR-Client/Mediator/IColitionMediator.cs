using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using System.Collections.Generic;

namespace FinaleSignalR_Client.Mediator
{
    public interface IColitionMediator
    {
        void Interact(Map map, List<IBullet> bulletsToRemove);

    }
}
