using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;
using Microsoft.AspNetCore.SignalR.Client;

namespace FinaleSignalR_Client.Web
{
    public class Communication
    {
        HubConnection connection;
        ListBox messages;
        string id;
        Form1 form;
        public Communication(ListBox message, Form1 form)
        {
            this.messages = message;
            this.id = form.id;
            this.form = form;

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7181/chatHub")
                .WithAutomaticReconnect()
                .Build();

            // These define what happens during their scenarios
            connection.Reconnecting += (sender) =>
            {
                messages.Invoke((MethodInvoker)delegate
                {
                    var newMessage = "Attempting to connect to the server...";
                    messages.Items.Add(newMessage);
                });

                return Task.CompletedTask;
            };

            connection.Reconnected += (sender) =>
            {
                messages.Invoke((MethodInvoker)delegate
                {
                    var newMessage = "Reconnected to the server";
                    messages.Items.Clear();
                    messages.Items.Add(newMessage);
                });

                return Task.CompletedTask;
            };

            connection.Closed += (sender) =>
            {
                messages.Invoke((MethodInvoker)delegate
                {
                    var newMessage = "Connection Closed";
                    messages.Items.Add(newMessage);
                    form.GameClosedButtonStuff();
                });

                return Task.CompletedTask;
            };
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


        public async void ParseMessage(string pClass)
        {
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.messages.Invoke((MethodInvoker)delegate
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

                            }
                        }
                    }
                    //var newMessage = $"{user}: {message}";
                    //messages.Items.Add(newMessage);
                });
            });

            try
            {
                await connection.StartAsync();
                messages.Items.Add("Connection Started");
                canICreateAvatar(pClass);
                form.GameStartButtonStuff();
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
