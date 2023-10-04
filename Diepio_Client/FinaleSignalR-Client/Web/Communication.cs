﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinaleSignalR_Client.Controls;
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

        public async void canICreateAvatar()
        {
            try
            {
                await connection.InvokeAsync("SendMessage", id, "Request|CreateAvatar");
            }
            catch (Exception ex)
            {
                messages.Items.Add(ex.Message);
            }
        }

        public async void ParseMessage()
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
                                    form.RequestAccepted();
                                    break;
                                case "EnemyCreated":
                                    if (parsedMessage[2] != id)
                                    {
                                        form.createPlayer(parsedMessage[2]);
                                    }
                                    break;
                                case "Coords":
                                    form.moveEnemy(parsedMessage[2], int.Parse(parsedMessage[3]), int.Parse(parsedMessage[4]));
                                    break;
                                case "Obstacles":
                                    form.mapControl.SetObstacle(int.Parse(parsedMessage[2]), int.Parse(parsedMessage[3]), int.Parse(parsedMessage[4]), int.Parse(parsedMessage[5]));
                                    form.Refresh();
                                    break;


                            }
                        }

                    }
                    var newMessage = $"{user}: {message}";
                    messages.Items.Add(newMessage);
                });
            });

            try
            {
                await connection.StartAsync();
                messages.Items.Add("Connection Started");
                canICreateAvatar();
                form.GameStartButtonStuff();
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


    }
}