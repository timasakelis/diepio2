using FinaleSignalR_Client.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinaleSignalR_Client.Web;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Forms;
using Microsoft.AspNet.SignalR.Infrastructure;
using System.Threading;
using Microsoft.AspNet.SignalR;
using FinaleSignalR_Client.Facade;
using FinaleSignalR_Client.Web;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinaleSignalR_Client;
using Microsoft.AspNet.SignalR.Hosting;

namespace FinalSignalR_Client.UnitTests
{
    [TestClass]
    public class CommunicationTests
    {
        [TestMethod]
        public void Communication_Receive_ChatMessage()
        {
            ListBox messages = new ListBox();
            CommunicationFacade comm = new CommunicationFacade(messages, new Form1());
            comm.SendChatMessage("First Message");
            comm.SendChatMessage("Second Message");
            comm.SendChatMessage("Third Message");
            Thread.Sleep(100);
            Assert.AreEqual("3", messages.Items.Count.ToString()); 

            /*
            string response = "";
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7181/chatHub")
                .WithAutomaticReconnect()
                .Build();

            var serverResponseTask = Parser(connection, "0", result =>
            {
                response = result;
            });
            canICreateAvatar("0", connection);
            Thread.Sleep(1000);

            if (response == "")
            {
                Assert.Fail("Server was not online or the response timed out");
            }
            Assert.AreEqual("RequestAccepted", response);*/
        }
        /*
        [TestMethod]
        public void Communication_Receive_Coordinates()
        {
            string response = "";
            HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7181/chatHub")
                .WithAutomaticReconnect()
                .Build();
            int left = 5;
            int top = 10;

            var serverResponseTask = Parser(connection, "0", result =>
            {
                response = result;
            });
            SendCoordinates(left, top);
            Thread.Sleep(1000);

            if (response == "")
            {
                Assert.Fail("Server was not online or the response timed out");
            }
            Assert.AreEqual("RequestAccepted", response);
        }*/
    }
}
