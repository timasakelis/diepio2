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
        ListBox messages;

        public CommunicationChat(HubConnection hub, string id, ListBox messages)
        {
            this.connection = hub;
            this.id = id;
            this.messages = messages;
        }

        public async void SendChatMessage(string text)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Chat|" + text);
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }
    }
}
