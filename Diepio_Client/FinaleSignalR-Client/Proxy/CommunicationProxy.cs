using FinaleSignalR_Client.Facade;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Proxy
{
    public class CommunicationProxy
    {
        Form1 form;
        ListBox messages;
        CommunicationFacade comm;
        int debugLevel;
        /* Explanation of debugging level
         * 0 - No proxy related debugging information
         * 1 - Basic connection messages (ParseMessage, StopConnection, canICreateAvatar, RemoveAvatar)
         * 2 - Adds chat message sending (SendChatMessage)
         * 3 - Adds realtime information sending (SendBulletInformation, SendCoordinates)
         */

        public CommunicationProxy(Form1 form, ListBox messages, int level) { 
            this.form = form;
            this.messages = messages;
            this.debugLevel = level;
            comm = new CommunicationFacade(form);
        }

        public void ParseMessage(string myClass)
        {
            if (debugLevel >= 1)
                messages.Items.Add("Iniating parsing for " + myClass);
            comm.ParseMessage(myClass);
        }

        public void StopConnection()
        {
            if (debugLevel >= 1)
                messages.Items.Add("Closing connection");
            comm.StopConnection();
        }

        public void canICreateAvatar(string pClass)
        {
            if (debugLevel >= 1)
                messages.Items.Add("Requesting avatar creation for " + pClass);
            comm.canICreateAvatar(pClass);
        }

        public void RemoveAvatar(string sentId)
        {
            if (debugLevel >= 1)
                messages.Items.Add("Removing avatar with id " + sentId);
            comm.RemoveAvatar(sentId);
        }

        public void SendChatMessage(string text)
        {
            if (debugLevel >= 2)
                messages.Items.Add("Sending chat message: " + text);
            comm.SendChatMessage(text);
        }

        public void SendBulletInformation(int x, int y, float directionX, float directionY, string id, string speed, string size)
        {
            if (debugLevel >= 3)
                messages.Items.Add("Sending bullet information: x="+x+" y="+y+" directionX="+directionX.ToString()+" directionY="+directionY.ToString()+" id="+id+" speed="+speed+" size="+size);
            comm.SendBulletInformation(x, y, directionX, directionY, id, speed, size);
        }

        public void SendCoordinates(int left, int top)
        {
            if (debugLevel >= 3)
                messages.Items.Add("Sending coordinate information: left="+left.ToString()+" top"+top.ToString());
            comm.SendCoordinates(left, top);
        }
    }
}
