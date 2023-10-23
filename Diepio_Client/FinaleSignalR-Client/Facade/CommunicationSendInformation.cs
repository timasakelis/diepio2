﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Web
{
    public class CommunicationSendInformation
    {
        HubConnection connection;
        string id;
        ListBox messages;

        public CommunicationSendInformation(HubConnection hub, string id, ListBox messages) 
        {
            this.connection = hub;
            this.id = id;
            this.messages = messages;
        }

        

        public async void SendCoordinates(int left, int top)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, $"Coords|{left}|{top}");
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        public async void SendBulletInfo(int x, int y, float directionX, float directionY, string id, string speed, string size)
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, $"BULLET|{x}|{y}|{directionX}|{directionY}|{id}|{speed}|{size}");
            }
            catch (Exception ex)
            {
                messages.Items.Add($"Error sending bullet data: {ex.Message}");
            }
        }
    }
}
