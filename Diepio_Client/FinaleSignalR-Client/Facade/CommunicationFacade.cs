using FinaleSignalR_Client.Web;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Facade
{
    public class CommunicationFacade
    {
        private readonly CommunicationAvatar _avatar;
        private readonly CommunicationSendInformation _sendInformation;
        private readonly CommunicationChat _chat;

        HubConnection connection;
        public ListBox connectionInvoker;
        string id;
        Form1 form;

        public CommunicationFacade(Form1 form) 
        {
            this.connectionInvoker = form.messages;
            this.id = form.id;
            this.form = form;

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7181/chatHub")
                .WithAutomaticReconnect()
                .Build();

            // These define what happens during their scenarios
            connection.Reconnecting += (sender) =>
            {
                connectionInvoker.Invoke((MethodInvoker)delegate
                {
                    form.AddMessage("Attempting to connect to the server...");
                });

                return Task.CompletedTask;
            };

            connection.Reconnected += (sender) =>
            {
                connectionInvoker.Invoke((MethodInvoker)delegate
                {
                    form.AddMessage("Reconnected to the server");
                });

                return Task.CompletedTask;
            };

            connection.Closed += (sender) =>
            {
                connectionInvoker.Invoke((MethodInvoker)delegate
                {
                    form.AddMessage("Connection Closed");
                    form.GameClosedButtonStuff();
                });

                return Task.CompletedTask;
            };

            _avatar = new CommunicationAvatar(connection, id);
            _chat = new CommunicationChat(connection, id);
            _sendInformation = new CommunicationSendInformation(connection, id);
        }

        public async void ParseMessage(string pClass)
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.connectionInvoker.Invoke((MethodInvoker)delegate
                {
                    var parsedMessage = message.Split('|');
                    if (parsedMessage.Length > 0)
                    {
                        if (parsedMessage[0] == "All" || parsedMessage[0] == id)
                        {
                            switch (parsedMessage[1])
                            {
                                case "RequestAccepted":
                                    form.RequestAccepted(parsedMessage[2]);
                                    break;
                                case "EnemyCreated":
                                    if (parsedMessage[2] != id)
                                    {
                                        form.createPlayer(parsedMessage[2], parsedMessage[3]);
                                    }
                                    break;
                                case "Coords":
                                    if (parsedMessage[2] != id)
                                    {
                                        form.moveEnemy(parsedMessage[2], int.Parse(parsedMessage[3]), int.Parse(parsedMessage[4]));
                                    }
                                    break;
                                case "Obstacles":
                                    form.mapControl.SetObstacle(int.Parse(parsedMessage[2]), int.Parse(parsedMessage[3]), int.Parse(parsedMessage[4]), int.Parse(parsedMessage[5]));
                                    form.Refresh();
                                    break;
                                case "BULLET":
                                    form.shootBullet(parsedMessage[2], parsedMessage[3], parsedMessage[4], parsedMessage[5], parsedMessage[6], parsedMessage[7], parsedMessage[8]);
                                    break;
                                case "PELLET":
                                    form.createPellet(int.Parse(parsedMessage[2]), int.Parse(parsedMessage[3]), int.Parse(parsedMessage[4]), int.Parse(parsedMessage[5]));
                                    break;
                                case "Remove":
                                    form.RemoveEnemy(parsedMessage[2]);
                                    break;
                                case "Chat":
                                    form.AddMessage(parsedMessage[2]);
                                    break;
                            }
                        }
                    }
                    //var newMessage = $"{user}: {message}";
                    //messages.Items.Add(newMessage);
                });
            });

            try
            {
                form.AddMessage("Connection Started");
                await connection.StartAsync();
                canICreateAvatar(pClass);
                form.GameStartButtonStuff();
            }
            catch (Exception ex)
            {
                connectionInvoker.Items.Add(ex.Message);
            }
        }

        public void StopConnection()
        {
            connection.StopAsync();
        }

        public void canICreateAvatar(string pClass)
        {
            _avatar.canICreateAvatar(pClass);
        }

        public void RemoveAvatar(string sentId)
        {
            _avatar.RemoveAvatar(sentId);
        }

        public void SendBulletInformation(int x, int y, float directionX, float directionY, string id, string speed, string size)
        {
            _sendInformation.SendBulletInfo(x, y, directionX, directionY, id, speed, size);
        }

        public void SendCoordinates(int left, int top)
        {
            _sendInformation.SendCoordinates(left, top);
        }

        public void SendChatMessage(string text)
        {
            _chat.SendChatMessage(text);
        }
    }
}
