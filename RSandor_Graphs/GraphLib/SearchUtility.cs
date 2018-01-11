using System;
using System.Collections.Generic;

namespace GraphLib
{
    public class Cell
    {
        public double F { get; set; }
        public double G { get; set; }

        public readonly double X;
        public readonly double Y;
        public readonly bool Blocked;
        public (double x, double y) Parent;

        public Cell(Vertex<(double x, double y)> node, bool blocked)
        {
            X = node.Value.x;
            Y = node.Value.y;
            Blocked = blocked;
            F = double.MaxValue;
            G = double.MaxValue;
            Parent = (Double.MinValue, Double.MinValue);
        }
    }

    public class CellComparer : Comparer<Cell>
    {
        public override int Compare(Cell left, Cell right)
        {
            return left.F.CompareTo(right.F);
        }
    }

    public static class SearchUtility<T> where T : struct, IComparable<T>
    {
        #region Breadth-First Search method

        public static Queue<Vertex<T>> BFS(Graph<T> graph, Vertex<T> start)
        {
            for(int index = 0; index < graph.Vertices.Count; index++)
            {
                graph.Vertices[index].Visited = false;
            }

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

        #endregion

        #region Depth-First Search methods

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

        // TODO: finish this function
        public static Queue<Vertex<T>> DFS_Iterative(Graph<T> graph, Vertex<T> start)
        {
            for (int index = 0; index < graph.Vertices.Count; index++)
            {
                graph.Vertices[index].Visited = false;
            }
            var queue = new Queue<Vertex<T>>();
            return queue;
        }
        #endregion

        #region Dijkstra search method & helper functions
        public static Queue<Vertex<T>> DijkstraSearch(Graph<T> graph, Vertex<T> start, Vertex<T> end)
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

            // sets the distances to (effectively) infinity and indicates
            // that the distances of all of the vertices aren't finalized
            // (since we just started)
            for (int index = 0; index < graph.Vertices.Count; index++)
            {
                // what value would I give to indicate that a given vertex is the root? default?
                parents[index] = default(T);
                distancesToNodes[index] = double.MaxValue;
                shortestPathTreeSet[index] = false;
            }

            List<Vertex<T>> traveledVertices = new List<Vertex<T>>();

            int indexOfStart = graph.Vertices.FindIndex(vertex => vertex.Value.Equals(start.Value));
            // setting the distance of 0 for the start
            // ensures that we start the algorithm at the designated start vertex
            parents[indexOfStart] = default(T);
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
                        edge => edge.Start.Value.Equals(graph.Vertices[minimumDistanceVertexIndex].Value) && 
                        edge.End.Value.Equals(graph.Vertices[vertexIndex].Value));

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
            Queue<Vertex<T>> shortestPath = new Queue<Vertex<T>>(traveledVertices);
            return shortestPath;
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
            int indexOfCurrent = graph.Vertices.FindIndex(vertex => vertex.Value.Equals(current.Value));

            if(parents[indexOfCurrent].Equals(default(T)))
            {
                Vertex<T> startVertex = graph.Vertices.Find(vertex => vertex.Value.Equals(current.Value)); 
                path.Add(startVertex);
                return path;
            }

            Vertex<T> parentOfCurrent = graph.Vertices.Find(vertex => vertex.Value.Equals(parents[indexOfCurrent]));
            path.AddRange(getPath(graph, parents, parentOfCurrent));
            path.Add(current);
            return path;
        }

        #endregion

        #region A* Search method & heuristics
        // use a heap
        // TODO: make a custom heap class
        // known, estimated, total
        // minheap based on total distance
        // estimated based off of heuristic
        // assumes coordinate plane
        // Manhattan, Euclidean, diagonal, octile, Chebyshev
        // use delegate to determine which heuristic to use
        // draw on bitmap
        // pacman, LoL
        // undirected algorithm
        // TODO: fix and finish this function
        // public static Queue<Vertex<(double, double)>> AStarSearch(Graph<(double, double)> graph, Vertex<(double, double)> start, Vertex<(double, double)> end, Func<(double, double), (double, double), double> heuristic)
        // do I want a queue of vertices or cells? does it make that much of a difference?
        public static Queue<Cell> AStarSearch(Cell[,] graph, Vertex<(double x, double y)> start, Vertex<(double x, double y)> end, Func<(double x, double y), (double x, double y), double> heuristic)
        {
            // would it be easier to add stuff to the vertex class
            // or create the cell class and pass an array/list to the A* search function?
            if (start == null)
            {
                throw new InvalidOperationException("Start point isn't valid.");
            }

            if (end == null)
            {
                throw new InvalidOperationException("End point isn't valid.");
            }

            if (start.Value.Equals(end.Value))
            {
                throw new InvalidOperationException("The start is also the destination.");
            }

            int startRowIndex = -1;
            int startColumnIndex = -1;
            // TODO: replace size with something else
            int size = 4;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    if (graph[row, column].X == start.Value.x && graph[row, column].Y == start.Value.y)
                    {
                        startRowIndex = row;
                        startColumnIndex = column;
                    }
                }
            }

            int endRowIndex = -1;
            int endColumnIndex = -1;
            for (int row = 0; row < size; row++)
            {
                // TODO: find a better variable than graph.Length; 
                // this assumes that the height and width are the same
                for (int column = 0; column < size; column++)
                {
                    if (graph[row, column].X == end.Value.x && graph[row, column].Y == end.Value.y)
                    {
                        endRowIndex = row;
                        endColumnIndex = column;
                    }
                }
            }

            if (graph[startRowIndex, startColumnIndex].Blocked || graph[endRowIndex, endColumnIndex].Blocked)
            {
                throw new InvalidOperationException("The start or the end point is blocked.");
            }

            bool [,] closedList = new bool[graph.Length, graph.Length];
            for(int row = 0; row < size; row++)
            {
                for(int column = 0; column < size; column++)
                {
                    closedList[row, column] = false;
                }
            }

            Queue<Cell> path = new Queue<Cell>();

            graph[startRowIndex, startColumnIndex].F = 0;
            graph[startRowIndex, startColumnIndex].G = 0;
            // the parent of the start node is the start node itself?
            graph[startRowIndex, startColumnIndex].Parent = (graph[startRowIndex, startColumnIndex].X, graph[startRowIndex, startColumnIndex].Y);

            MinHeap<Cell> openList = new MinHeap<Cell>(new CellComparer());
            openList.Add(graph[startRowIndex, startColumnIndex]);

            bool foundDestination = false;
            
            while(openList.Count > 0)
            {
                Cell recentValue = openList.ExtractDominating();
                double columnIndexOfRecentValue = recentValue.X;
                double rowIndexOfRecentValue = recentValue.Y;
                closedList[(int)rowIndexOfRecentValue, (int)columnIndexOfRecentValue] = true;

                double newGValue = -1;
                double newFValue = -1;
                double newHValue = -1;

                // do something similar to the if/else if statement below for 8 cases 
                // (each node around the original node)

                // check if the node directly above the node that was popped off
                // is a valid node
                if(rowIndexOfRecentValue - 1 < size && columnIndexOfRecentValue < size)
                {
                    // if it is a valid node AND it's the destination
                    if (rowIndexOfRecentValue == endRowIndex && endColumnIndex == columnIndexOfRecentValue)
                    {
                        graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].Parent = (rowIndexOfRecentValue, columnIndexOfRecentValue);
                        // trace path? - uses the parent attribute and a stack to go through
                        // the path until it hits the "root"
                        path = tracePath(graph, end);
                        foundDestination = true;
                    }
                }
                // otherwise if it's not part of the closed list and it's not blocked...
                else if (!closedList[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue] && !graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].Blocked)
                {
                    newGValue = graph[(int)rowIndexOfRecentValue, (int)columnIndexOfRecentValue].G + 1;
                    // maybe I should pair the x and y values in the cell?
                    newHValue = heuristic((graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].X, graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].Y), end.Value);
                    newFValue = newGValue + newHValue;

                    // this is way too verbose... 
                    // TODO: refactor so it's less verbose
                    if(graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].F == double.MaxValue ||
                        newFValue < graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].F)
                    {
                        openList.Add(graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue]);

                        graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].F = newFValue;
                        graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].G = newGValue;
                        graph[(int)rowIndexOfRecentValue - 1, (int)columnIndexOfRecentValue].Parent = (rowIndexOfRecentValue, columnIndexOfRecentValue);
                    }
                }
            }

            if (!foundDestination)
            {
                return null;
            }
            // create a closed list - how large should it be?
            // create a 2d array to hold details of each cell in the map
            // do I need a separate class for that?
            // set the h (heuristic), g (cost to get to a given node/goal), 
            // and f (sum of g and h) to the max value
            // calculate H on-the-fly, F is needed 
            // because the minheap is sorted using that value

            // Ryan implements it so that it's one list, just the open one?
            // what does the closed list do?

            // initialize the starting node with f, g, and h values of 0



            // create an open list that has the cell location and f cost associated with it
            // insert the starting node into the open list
            // would the open list be a heap?

            // check all 8 directions? <- necessary?
            return path;
        }

        private static Queue<Cell> tracePath(Cell[,] graph, Vertex<(double x, double y)> destination)
        {
            int columnIndex = (int)destination.Value.x;
            int rowIndex = (int)destination.Value.y;
            Stack<Cell> reversedPath = new Stack<Cell>();
            Queue<Cell> path = new Queue<Cell>();
            while(!(graph[rowIndex, columnIndex].Parent.Equals(destination.Value)))
            {
                reversedPath.Push(graph[rowIndex, columnIndex]);
                rowIndex = (int)graph[rowIndex, columnIndex].Parent.y;
                columnIndex = (int)graph[rowIndex, columnIndex].Parent.x;
            }

            while(reversedPath.Count > 0)
            {
                path.Enqueue(reversedPath.Pop());
            }
            return path;
        }
        /*
        public static Queue<Vertex<T>> AStarSearch(Graph<T> graph, Func<(T,T), (T,T), T> heuristic)
        {
            return null;
        }
        */

        // TODO: fix and finish this function
        public static double CalculateManhattanDistance((double x, double y) start, (double x, double y) end)
        {
            return Math.Abs(start.x - end.x) + Math.Abs(start.y - end.y);
        }

        // created alternative version using value tuples and dynamic types
        // TODO: check how "good" this is - unit tests?
        public static (T, bool) CalculateManhattanDistance((T x, T y) start, (T x, T y) end)
        {
            dynamic firstNumeric = (dynamic)start;
            dynamic secondNumeric = (dynamic)end;
            dynamic returnValue;
            try
            {
                returnValue = (firstNumeric.x - secondNumeric.x) + (firstNumeric.y - secondNumeric.y);
                return (returnValue, true);
            }
            catch
            {
                return (default(T), false);
            }
        }

        // TODO: fix and finish this function
        public static double CalculateEuclideanDistance((double x, double y) start, (double x, double y) end)
        {
            return Math.Sqrt(Math.Pow((double)(start.x - end.x), 2.0) + Math.Pow((double)(start.y - end.y), 2.0));
        }

        #endregion 
    }
}
