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
    public class Graph<T>
    {
        // do I want the user the ability to change IsWeighted whenever?
        // I feel like it should only be changed/set at the very beginning
        // if so, I would make this readonly
        public bool IsWeighted { get; private set; }

        // do I want to have 2 separate lists or 1 combined list of edges?
        // List<Edge<T>> outgoing;
        // List<Edge<T>> ingoing;
        // List<Edge<T>> edges;

        // is there a particular reason reflection is avoided?
        // reflection is slow, expensive and generally avoided
        // useful for finding out information not available at compile time

        // how should I implement IsDirected? a bool as well?
        // if it's undirected and 1 list, then I would automatically generate the second edge based on what it's given 
        // if it's undirected and 2 lists, then I would add to the second list
        // what benefits does using 2 lists have over 1?


        public Graph(bool isWeighted)
        {
            IsWeighted = isWeighted;
        }

        public Edge<T> CreateEdge()
        {
            return new Edge<T>(IsWeighted);
        }
        public Vertex<T> CreateVertex()
        {
            return new Vertex<T>();
        }
    }
}
