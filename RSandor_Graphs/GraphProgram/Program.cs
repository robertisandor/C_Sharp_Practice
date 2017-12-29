using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSandor_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph(true);
            var edge = graph.CreateEdge();
            edge.Weight = 3;

        }
    }
}
