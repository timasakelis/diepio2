using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Web
{
    public class CommunicationChat
    {
        HubConnection connection;
        string id;
        List<string> logger = new List<string>();

        public CommunicationChat(HubConnection hub, string id)
        {
            this.connection = hub;
            this.id = id;
        }

        public async void SendChatMessage(string text)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Chat|" + text);
            }
            catch (Exception ex)
            {
                logger.Add(ex.Message);
            }
        }
    }
}
