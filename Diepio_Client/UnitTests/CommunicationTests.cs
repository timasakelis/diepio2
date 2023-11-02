using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;
using System.Threading;
using FinaleSignalR_Client.Facade;
using FinaleSignalR_Client;
using System.Linq;
using System;
using System.Collections.Generic;

namespace FinalSignalR_Client.UnitTests
{
    [TestClass]
    public class CommunicationTests
    {
        /*
        [TestMethod]
        public void Communication_Receive_RequestGranted()
        {
            /*
             * NOTHING WORKS NOTHING WORKS NOTHING WORKS
             * NOTHING WORKS NOTHING WORKS NOTHING WORKS
             * NOTHING WORKS NOTHING WORKS NOTHING WORKS
             * NOTHING WORKS NOTHING WORKS NOTHING WORKS
             * 
            //ListBox messages = new ListBox();
            Form1 f1 = new Form1();
            Form1 f2 = new Form1();
            CommunicationFacade comm = f1.commFacade;
            CommunicationFacade comm2 = f2.commFacade;
            f1.StartParsing();
            f2.StartParsing();
            Thread.Sleep(100);
            comm.canICreateAvatar(0.ToString());
            comm.SendChatMessage("2"); 
            comm.SendChatMessage("2");
            Thread.Sleep(100);
            ListBox messages = f2.commFacade.messages;
            string lastItem = messages.Items[messages.Items.Count - 1].ToString();
            Assert.AreEqual("3", messages.Items.Count);
            comm.StopConnection();
            /*
            Form1 form1 = new Form1();
            
            CommunicationFacade comm = form1.commFacade;
            int id = 0;
            //comm.canICreateAvatar(id.ToString());
            comm.SendChatMessage("1");

            Thread.Sleep(1000);
            ListBox messages = form1.ReturnMessageItems();
            string lastItem = "";
            lastItem = messages.Items[messages.Items.Count - 1].ToString();
            /*
            messages.Invoke(new Action(() =>
            {
                lastItem = messages.Items[messages.Items.Count - 1].ToString();
            }));
            Assert.AreEqual(id.ToString() + "|RequestGranted", lastItem);
            comm.StopConnection();*/
            /*
            ListBox messages = new ListBox();
            CommunicationFacade comm = new CommunicationFacade(messages, new Form1());
            int id = 0;
            Thread.Sleep(1000);
            //comm.SendChatMessage("Third Message");
            comm.canICreateAvatar(id.ToString());
            Thread.Sleep(100);
            Assert.AreEqual(id.ToString() + "|RequestGranted", messages.Items[messages.Items.Count-1].ToString());
            comm.StopConnection();
        }*/
    }
}
