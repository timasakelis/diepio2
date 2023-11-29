using FinaleSignalR_Client.Composite;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Factory;
using FinaleSignalR_Client.Objects;
using FinaleSignalR_Client.Proxy;
using System.Collections.Generic;

namespace FinaleSignalR_Client.Mediator
{
    public interface IColitionMediator
    {
        void Interact(Map map, List<IBullet> bulletsToRemove, Player player, List<IPellet> pelletsToRemove,
            CommunicationProxy commProxy, Node rootNode);

    }
}
