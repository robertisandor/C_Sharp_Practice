using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * single list of edges that specifies the beginning and end vertices
 * two lists of edges that specify just the beginning or just the end
 * weighted & unweighted
 * unweighted - set all of the weights to the same # - 1
 * how can we enforce the entire graph to be unweighted?
 * how can we enforce the entire graph to be directed or undirected?
 * */
namespace GraphLib
{
    
    

    public class Graph
    {
        public bool IsWeighted { get; private set; }

        public Graph(bool isWeighted)
        {
            IsWeighted = isWeighted;
        }

        public Edge CreateEdge()
        {
            return new Edge(IsWeighted);
        }
    }
}
