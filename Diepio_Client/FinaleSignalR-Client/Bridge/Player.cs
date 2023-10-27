﻿using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Decorator;
using FinaleSignalR_Client.Stategy;
using FinaleSignalR_Client.Prototype;
using FinaleSignalR_Client.Web;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinaleSignalR_Client.Bridge;
using FinaleSignalR_Client.Adapter;
using System.Numerics;

namespace FinaleSignalR_Client.Objects
{
    public class Player
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Playerspeed { get; set; }
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
        public PictureBox PlayerBox { get; set; }
        //--------------Bridge--------------------
        protected IWepon weapon;
        
        protected IInteractioBehavior _implementation;
        //-----------------------------------------
        private Color color;
        private Point startingPoint;
        public Player() { }
        public Player(string id, string name, Color color, Point startingPoint, IInteractioBehavior implementation, IWepon wepon) {

            this.Id = id;
            this.Name = name;
            this.weapon = wepon; // Default weapon

            this.PlayerBox = new PictureBox { 
                BackColor = color,
                Location = startingPoint,
                Name = id,
            };
            this.PlayerBox.TabIndex = 0;
            this.PlayerBox.TabStop = false;


            this._implementation = implementation;
        }

        public Player(string id, string name, Color color, Point startingPoint)
        {
            Id = id;
            Name = name;
            this.color = color;
            this.startingPoint = startingPoint;
        }

        public Player(string id, string name, Color color, Point startingPoint, IInteractioBehavior behavior) : this(id, name, color, startingPoint)
        {
        }

        public virtual void Move(string dirrection, Map mapControl)
        {
            _implementation.Move(dirrection,this,mapControl);
            //moveAlgorithm?.behaveDiffrentley(dirrection, this, mapControl);
        }

        public virtual List<IBullet> Fire(int x, int y, Vector2 Direction, string id)
        {
            return this.weapon.Fire(x, y, Direction, id);
        }

        public virtual void Buff(string type)
        {
            switch(type)
            {
                case "speed":
                    this.weapon.SpeedBoost();
                    break;
                case "size":
                    this.weapon.SizeBoost();
                    break;
                case "default":
                    this.weapon.Default();
                    break;
            }
        }

        public virtual int GetWeopenSpeed()
        {
            return this.weapon.Speed;
        }

        public virtual int GetWeopenSize()
        {
            return this.weapon.Size;
        }


        public void LvlUp(LvlUp stats)
        {
            this.Playerspeed = Playerspeed + stats.Playerspeed;
            this.MaxHP = MaxHP + stats.MaxHP;
        }

        public virtual void TakeDamage(int damage)
        {
            this.CurrentHP -= damage;
        }

        public void Shoot()
        {

        }

    }
}
