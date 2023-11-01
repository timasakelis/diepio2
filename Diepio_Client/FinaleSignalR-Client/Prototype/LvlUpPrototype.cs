using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinaleSignalR_Client.Controls;
using FinaleSignalR_Client.Objects;

namespace FinaleSignalR_Client.Prototype
{
    public class LvlUpPrototype : IPrototype
    {
        public LvlUp stats { get; set; }
        public IPrototype Clone()
        {
            return new LvlUpPrototype { stats = this.stats };
        }

        public IPrototype DeepClone()
        {
            return new LvlUpPrototype(this.stats.Playerspeed, this.stats.MaxHP);
        }

        public LvlUpPrototype() 
        {
            this.stats = new LvlUp();
        }

        public LvlUpPrototype(int speed, int hp)
        {
            this.stats = new LvlUp();
            this.stats.MaxHP = hp;
            this.stats.Playerspeed = speed;
        }

        public void ChangeHp (int hp)
        {
            this.stats.MaxHP = hp;
        }

        public void ChangeSpeed (int speed) 
        { 
            this.stats.Playerspeed = speed;
        }
    }
}
