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
    public class Vertex
    {
        int value;
        List<Edge> outgoing;

    }

    public class Edge
    {
        Vertex Start;
        Vertex End;

        bool isWeighted;

        internal Edge(bool isWeighted)
        {
            this.isWeighted = isWeighted;
        }

        float weight;
        public float Weight
        {
            get => weight;
            set
            {
                if(!isWeighted)
                {
                    throw new InvalidOperationException("Cannot change weight in an unweighted graph");
                }

                weight = value;
            }
        }
    }

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
