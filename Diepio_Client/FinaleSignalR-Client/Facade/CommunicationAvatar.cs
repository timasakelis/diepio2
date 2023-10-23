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
        ListBox messages;

        public CommunicationAvatar(HubConnection hub, string id, ListBox messages)
        {
            this.connection = hub;
            this.id = id;
            this.messages = messages;
        }

        public async void canICreateAvatar(string pClass)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Request|CreateAvatar|" + pClass);
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        public async void RemoveAvatar(string text)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Remove|" + text);
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }
    }
}
