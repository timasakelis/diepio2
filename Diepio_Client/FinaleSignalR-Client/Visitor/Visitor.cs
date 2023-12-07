using FinaleSignalR_Client.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace FinaleSignalR_Client.Visitor
{
    public class PelletVisitor : IPelletVisitor
    {
        public void Visit(SquarePellet pellet)
        {
            pellet.HP *= 3;
            pellet.EXP += 15;
        }

        public void Visit(TrianglePellet pellet)
        {
            pellet.HP += 3;
            pellet.EXP += 10;
        }

        public void Visit(OctagonPellet pellet)
        {
            pellet.EXP += 10;
            pellet.HP *= 6;
        }
    }
}
