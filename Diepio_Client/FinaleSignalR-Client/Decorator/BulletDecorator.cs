﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Decorator
{
    public class BulletDecorator : IBullet
    {
        private IBullet _bullet;
        string IBullet.playerid => _bullet.playerid;

        public Vector2 Direction => _bullet.Direction;

        public BulletDecorator(IBullet bullet)
        {
            _bullet = bullet;
        }

        public virtual PictureBox GetPictureBox()
        {
            return _bullet.GetPictureBox();
        }

        public virtual float GetSpeed()
        {
            return _bullet.GetSpeed();
        }
        public virtual Size GetSize()
        {
            return _bullet.GetSize();
        }
        public virtual void SetTragectory(Point startPosition, Vector2 direction)
        {
            _bullet.SetTragectory(startPosition, direction);
        }

        public void MoveBullet()
        {
            this.GetPictureBox().Left += (int)(Direction.X * GetSpeed());
            this.GetPictureBox().Top += (int)(Direction.Y * GetSpeed());
        }
    }
}
