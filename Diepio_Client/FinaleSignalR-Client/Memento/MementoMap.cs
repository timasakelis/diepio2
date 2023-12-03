using FinaleSignalR_Client.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinaleSignalR_Client.Memento
{
    public class MementoMap
    {
        Stack<List<MemeObj>> memoryObjs;

        public MementoMap()
        {
            memoryObjs = new Stack<List<MemeObj>>();
        }

        public void Save(List<Player> players)
        {
            List<MemeObj> list = new List<MemeObj>();
            foreach (Player player in players)
            {
                MemeObj meme = new MemeObj(player);
                list.Add(meme);
            }
            memoryObjs.Push(list);
        }

        public List<Player> Restore(List<Player> p, int index = 0)
        {
            if (memoryObjs.Count() < 1)
            {
                return p;
            }

            for (int i = 0; i < index; i++)
            {
                memoryObjs.Pop();
            }

            List <MemeObj> map = memoryObjs.Pop();
            for (int i = 0; i < p.Count; i++)
            {
                if (map.Count > i)
                {
                    p[i].PlayerBox.Left = map[i].x;
                    p[i].PlayerBox.Top = map[i].y;
                }
            }

            return p;
        }
    }
}
