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
        public readonly bool IsWeighted;
        public readonly bool IsDirected;
        // do I want to have 2 separate lists or 1 combined list of edges?
        // List<Edge<T>> outgoing;
        // List<Edge<T>> ingoing;
        public List<Vertex<T>> Vertices;
        public List<Edge<T>> Edges;

        // is there a particular reason reflection is avoided?
        // reflection is slow, expensive and generally avoided
        // useful for finding out information not available at compile time

        // how should I implement IsDirected? a bool as well?
        // if it's undirected and 1 list, then I would automatically generate the second edge based on what it's given 
        // if it's undirected and 2 lists, then I would add to the second list
        // what benefits does using 2 lists have over 1?


        public Graph(bool isWeighted, bool isDirected)
        {
            IsWeighted = isWeighted;
            IsDirected = isDirected;
            Vertices = new List<Vertex<T>>();
            Edges = new List<Edge<T>>();
        }

        public List<Edge<T>> CreateEdge(Vertex<T> firstVertex, Vertex<T> secondVertex, float weight)
        {
            List<Edge<T>> edgesCreated = new List<Edge<T>>();
            edgesCreated.Add(new Edge<T>(firstVertex, secondVertex, IsWeighted, weight));
            if(!IsDirected)
            {
                edgesCreated.Add(new Edge<T>(secondVertex, firstVertex, IsWeighted, weight));
            }
            return edgesCreated;
        }

        public List<Edge<T>> CreateEdge(Vertex<T> firstVertex, Vertex<T> secondVertex)
        {
            List<Edge<T>> edgesCreated = new List<Edge<T>>();
            edgesCreated.Add(new Edge<T>(firstVertex, secondVertex, IsWeighted));
            if (!IsDirected)
            {
                edgesCreated.Add(new Edge<T>(secondVertex, firstVertex, IsWeighted));
            }
            return edgesCreated;
        }

        /* creating separate functions for directed and undirected 
         * gives a chance for the graph and edge to be misaligned
         * but if I'm creating an undirected graph, I want to return all of the edges I'm creating
         * maybe always returning a list
         * */

        public Vertex<T> CreateVertex<T>(T value)
        {
            return new Vertex<T>(value);
        }
    }
}
