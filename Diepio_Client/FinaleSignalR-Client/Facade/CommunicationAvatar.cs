using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Web
{
    public class CommunicationAvatar
    {
        HubConnection connection;
        string id;
        List<string> logger = new List<string>();

        public CommunicationAvatar(HubConnection hub, string id)
        {
            this.connection = hub;
            this.id = id;
        }

        public async void canICreateAvatar(string pClass)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Request|CreateAvatar|" + pClass);
            }
            catch (Exception ex)
            {
                logger.Add(ex.Message);
            }
        }

        public async void RemoveAvatar(string sentId)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Remove|" + sentId);
            }
            catch (Exception ex)
            {
                logger.Add(ex.Message);
            }
        }
    }
}
