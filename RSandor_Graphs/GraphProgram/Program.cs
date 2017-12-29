using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Numerics;

namespace RSandor_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var graph = new Graph<int>(true);
            var edge = graph.CreateEdge();
            edge.Weight = 3;
            */
            
            var graph = new Graph<int>(false, false);
            graph.CreateVertex<int>(0);
            graph.CreateVertex<int>(1);
            graph.CreateVertex<int>(2);
            //graph.CreateEdge()
        }
    }
}
