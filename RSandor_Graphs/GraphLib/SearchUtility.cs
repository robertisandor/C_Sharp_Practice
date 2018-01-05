using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public static class SearchUtility<T> where T : struct, IComparable<T>
    {
        // TODO: implement Dijkstra Search
        public static List<Edge<T>> DijkstraSearch(Vertex<T> start, Vertex<T> end)
        {

            return null;
        }

        // TODO: revise function signature
        public static List<T> BFS(Graph<T> graph, Vertex<T> start)
        {
            // I should probably ensure that the entire list isn't visited
            for(int index = 0; index < graph.Vertices.Count; index++)
            {
                graph.Vertices[index].Visited = false;
            }

            // do I need a queue here or a list? most likely queue
            Queue<Vertex<T>> nodesToVisit = new Queue<Vertex<T>>();

            int indexOfStart = graph.Vertices.FindIndex(vertex => vertex.Value.Equals(start.Value));
            graph.Vertices[indexOfStart].Visited = true;

            nodesToVisit.Enqueue(graph.Vertices[indexOfStart]);

            while(nodesToVisit.Count > 0)
            {

            }

            return null;
        }

        // TODO: revise function signature
        public static List<T> DFS(Graph<T> graph, Vertex<T> start)
        {
            for (int index = 0; index < graph.Vertices.Count; index++)
            {
                graph.Vertices[index].Visited = false;
            }

            return null;
        }
    }
}
