using System;
using System.Collections.Generic;

namespace GraphLib
{
    public static class SearchUtility<T> where T : struct, IComparable<T>
    {
        // TODO: revise function signature - what should it return? a list or queue?
        public static Queue<Vertex<T>> BFS(Graph<T> graph, Vertex<T> start)
        {
            // I should probably ensure that the entire list isn't visited
            for(int index = 0; index < graph.Vertices.Count; index++)
            {
                graph.Vertices[index].Visited = false;
            }

            // do I need a queue here or a list? most likely queue
            Queue<Vertex<T>> nodesToVisit = new Queue<Vertex<T>>();
            Queue<Vertex<T>> traversedPath = new Queue<Vertex<T>>();
            int indexOfStart = graph.Vertices.FindIndex(vertex => vertex.Value.Equals(start.Value));
            graph.Vertices[indexOfStart].Visited = true;

            nodesToVisit.Enqueue(graph.Vertices[indexOfStart]);

            while(nodesToVisit.Count > 0)
            {
                Vertex<T> visitedNode = nodesToVisit.Dequeue();
                // find the visitedNode's neighbors
                // look through the edge list where that vertex is the start vertex

                traversedPath.Enqueue(visitedNode);

                // perhaps I can make a function out of this?
                List<Vertex<T>> neighbors = new List<Vertex<T>>();
                // find the neighbors
                // go through each edge to find neighbors
                foreach(var edge in graph.Edges)
                {
                    // if the start of the edge matches the visited node,
                    // then we've found an outgoing edge (neighbor)
                    if(edge.Start.Value.Equals(visitedNode.Value))
                    {
                        neighbors.Add(edge.End);
                        continue;
                    }
                }

                foreach(var neighbor in neighbors)
                {
                    if(neighbor.Visited)
                    {
                        continue;
                    }

                    neighbor.Visited = true;
                    nodesToVisit.Enqueue(neighbor);
                }
            }

            return traversedPath;
        }

        // TODO: revise function signature
        public static Queue<Vertex<T>> DFS(Graph<T> graph, Vertex<T> start)
        {
            for (int index = 0; index < graph.Vertices.Count; index++)
            {
                graph.Vertices[index].Visited = false;
            }

            List<Vertex<T>> visitedNodes = new List<Vertex<T>>();

            visitedNodes.AddRange(dfs_Recursive(graph, start));
            var queue = new Queue<Vertex<T>>(visitedNodes);
            return queue;
        }

        private static List<Vertex<T>> dfs_Recursive(Graph<T> graph, Vertex<T> current)
        {
            current.Visited = true;
           
            List<Vertex<T>> visitedNodes = new List<Vertex<T>>();
            visitedNodes.Add(current);

            List<Vertex<T>> neighbors = new List<Vertex<T>>();
            // find the neighbors
            // go through each edge to find neighbors
            foreach (var edge in graph.Edges)
            {
                // if the start of the edge matches the visited node,
                // then we've found an outgoing edge (neighbor)
                if (edge.Start.Value.Equals(current.Value))
                {
                    neighbors.Add(edge.End);
                    continue;
                }
            }

            // for each neighbor
            for (int index = 0; index < neighbors.Count; index++)
            {
                if(neighbors[index].Visited)
                {
                    continue;
                }
                visitedNodes.AddRange(dfs_Recursive(graph, neighbors[index]));
            }

            return visitedNodes;
        }

        // TODO: implement Dijkstra Search
        public static List<Vertex<T>> DijkstraSearch(Graph<T> graph, Vertex<T> start, Vertex<T> end)
        {
            if (!graph.IsWeighted)
            {
                throw new InvalidOperationException("Can't run Dijkstra's algorithm on an unweighted graph.");
            }

            // could this be combined into a tuple? what would I name it?
            double[] distancesToNodes = new double[graph.Vertices.Count];

            // indicates whether the indicated vertex is part of the shortest path
            // or if the shortest distance from the source to that vertex is finalized
            bool[] shortestPathTreeSet = new bool[graph.Vertices.Count];

            // var tupleExample = (Name: "Robert", Age: 26);
            // Tuple<int, bool> distancesToNodesAnd

            // is this the right datatype?
            List<T> parents = new List<T>(graph.Vertices.Count);
            foreach(var vertex in graph.Vertices)
            {
                parents.Add(vertex.Value);
            }

            parents[0] = default(T);
            // sets the distances to (effectively) infinity and indicates
            // that the distances of all of the vertices aren't finalized
            // (since we just started)
            for (int index = 0; index < graph.Vertices.Count; index++)
            {
                // what value would I give to indicate that a given vertex is the root? default?
               
                distancesToNodes[index] = double.MaxValue;
                shortestPathTreeSet[index] = false;
            }

            List<Vertex<T>> traveledVertices = new List<Vertex<T>>();

            int indexOfStart = graph.Vertices.FindIndex(vertex => vertex.Value.Equals(start.Value));
            // setting the distance of 0 for the start
            // ensures that we start the algorithm at the designated start vertex
            distancesToNodes[indexOfStart] = 0;

            for(int index = 0; index < graph.Vertices.Count - 1; index++)
            {
                // could a minheap/priority queue be used rather than the minimum distance function?
                int minimumDistanceVertexIndex = calculateMinimumDistance(graph, distancesToNodes, shortestPathTreeSet);

                // marks this particular index as processed
                shortestPathTreeSet[minimumDistanceVertexIndex] = true;

                
                for(int vertexIndex = 0; vertexIndex < graph.Vertices.Count; vertexIndex++)
                {
                    Edge<T> edgeBetweenMinimumAndCurrent = graph.Edges.Find(
                        edge => edge.Start.Equals(parents[minimumDistanceVertexIndex]) && 
                        edge.End.Equals(parents[vertexIndex]));

                    if (!shortestPathTreeSet[vertexIndex] && edgeBetweenMinimumAndCurrent != null &&
                        distancesToNodes[indexOfStart] != double.MaxValue &&
                        distancesToNodes[minimumDistanceVertexIndex] + edgeBetweenMinimumAndCurrent.Weight < distancesToNodes[vertexIndex])
                    {
                        parents[vertexIndex] = graph.Vertices[minimumDistanceVertexIndex].Value;
                        distancesToNodes[vertexIndex] = distancesToNodes[indexOfStart] + edgeBetweenMinimumAndCurrent.Weight;
                    }
                }
            }

            traveledVertices.AddRange(getPath(graph, parents, end));

            return traveledVertices;
        }

        private static int calculateMinimumDistance(Graph<T> graph, double[] distances, bool[] shortestPathTreeSet)
        {
            double minimumDistance = double.MaxValue;
            int minimumIndex = 0;

            for(int index = 0; index < graph.Vertices.Count; index++)
            {
                if(!shortestPathTreeSet[index] && distances[index] <= minimumDistance)
                {
                    minimumDistance = distances[index];
                    minimumIndex = index;
                }
            }

            return minimumIndex;
        }

        private static List<Vertex<T>> getPath(Graph<T> graph, List<T> parents, Vertex<T> current)
        {
            List<Vertex<T>> path = new List<Vertex<T>>();
            int indexOfCurrent = parents.FindIndex(value => value.Equals(current.Value));

            if(parents[indexOfCurrent].Equals(default(T)))
            {
                Vertex<T> startVertex = graph.Vertices.Find(vertex => vertex.Value.Equals(parents[indexOfCurrent])); 
                path.Add(startVertex);
                return path;
            }

            Vertex<T> parentOfCurrent = graph.Vertices.Find(vertex => vertex.Value.Equals(parents[indexOfCurrent]));
            path.AddRange(getPath(graph, parents, parentOfCurrent));
            return path;
        }
    }
}
