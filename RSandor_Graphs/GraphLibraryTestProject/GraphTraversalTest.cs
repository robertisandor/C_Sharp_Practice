using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLib;
using System.Collections.Generic;

namespace GraphLibraryTestProject
{
    [TestClass]
    public class GraphTraversalTest
    {
        [TestMethod]
        public void BFSTest()
        {
            Graph<int> graph = new Graph<int>(false, false);
            graph.Vertices.AddRange(new Vertex<int>[]{
                graph.CreateVertex(1), graph.CreateVertex(2), graph.CreateVertex(3),
                graph.CreateVertex(4), graph.CreateVertex(5), graph.CreateVertex(6)
            });

            graph.Edges.AddRange(graph.CreateEdge(1, 3));
            graph.Edges.AddRange(graph.CreateEdge(1, 2));
            graph.Edges.AddRange(graph.CreateEdge(2, 4));
            graph.Edges.AddRange(graph.CreateEdge(3, 4));
            graph.Edges.AddRange(graph.CreateEdge(3, 5));
            graph.Edges.AddRange(graph.CreateEdge(5, 6));

            var traveledNodes = SearchUtility<int>.BFS(graph, graph.Vertices[0]);
            Assert.AreEqual(6, traveledNodes.Count);
            Assert.AreEqual(1, traveledNodes.Dequeue().Value);
            Assert.AreEqual(3, traveledNodes.Dequeue().Value);
            Assert.AreEqual(2, traveledNodes.Dequeue().Value);
            Assert.AreEqual(4, traveledNodes.Dequeue().Value);
            Assert.AreEqual(5, traveledNodes.Dequeue().Value);
            Assert.AreEqual(6, traveledNodes.Dequeue().Value);
        }

        [TestMethod]
        public void DFSTest()
        {
            Graph<int> graph = new Graph<int>(false, false);
            graph.Vertices.AddRange(new Vertex<int>[]{
                graph.CreateVertex(1), graph.CreateVertex(2), graph.CreateVertex(3),
                graph.CreateVertex(4), graph.CreateVertex(5), graph.CreateVertex(6)
            });

            graph.Edges.AddRange(graph.CreateEdge(1, 3));
            graph.Edges.AddRange(graph.CreateEdge(1, 2));
            graph.Edges.AddRange(graph.CreateEdge(3, 4));
            graph.Edges.AddRange(graph.CreateEdge(3, 5));
            graph.Edges.AddRange(graph.CreateEdge(5, 6));

            var traveledNodes = SearchUtility<int>.DFS(graph, graph.Vertices[0]);
            Assert.AreEqual(6, traveledNodes.Count);
            Assert.AreEqual(1, traveledNodes.Dequeue().Value);
            Assert.AreEqual(3, traveledNodes.Dequeue().Value);
            Assert.AreEqual(4, traveledNodes.Dequeue().Value);
            Assert.AreEqual(5, traveledNodes.Dequeue().Value);
            Assert.AreEqual(6, traveledNodes.Dequeue().Value);
            Assert.AreEqual(2, traveledNodes.Dequeue().Value);
        }

        [TestMethod]
        public void DijkstraSearchUndirectedTest()
        {
            Graph<int> graph = new Graph<int>(true, false);
            graph.Vertices.AddRange(new Vertex<int>[]{
                graph.CreateVertex(1), graph.CreateVertex(2), graph.CreateVertex(3),
                graph.CreateVertex(4), graph.CreateVertex(5), graph.CreateVertex(6)
            });

            graph.Edges.AddRange(graph.CreateEdge(1, 3, 5.0f));
            graph.Edges.AddRange(graph.CreateEdge(1, 2, 10.0f));
            graph.Edges.AddRange(graph.CreateEdge(3, 4, 7.0f));
            graph.Edges.AddRange(graph.CreateEdge(3, 5, 3.0f));
            graph.Edges.AddRange(graph.CreateEdge(5, 6, 6.0f));

            var searchPath = SearchUtility<int>.DijkstraSearch(graph, graph.Vertices[0], graph.Vertices[5]);
            Assert.AreEqual(4, searchPath.Count);
            Assert.AreEqual(1, searchPath.Dequeue().Value);
            Assert.AreEqual(3, searchPath.Dequeue().Value);
            Assert.AreEqual(5, searchPath.Dequeue().Value);
            Assert.AreEqual(6, searchPath.Dequeue().Value);

            graph.Vertices.AddRange(new Vertex<int>[] {
                graph.CreateVertex(7), graph.CreateVertex(8)
            });

            graph.Edges.AddRange(graph.CreateEdge(5, 7, 1.0f));
            graph.Edges.AddRange(graph.CreateEdge(6, 7, 100.0f));
            graph.Edges.AddRange(graph.CreateEdge(7, 8, 20.0f));

            var secondSearchPath = SearchUtility<int>.DijkstraSearch(graph, graph.Vertices[0], graph.Vertices[7]);
            Assert.AreEqual(5, secondSearchPath.Count);
            Assert.AreEqual(1, secondSearchPath.Dequeue().Value);
            Assert.AreEqual(3, secondSearchPath.Dequeue().Value);
            Assert.AreEqual(5, secondSearchPath.Dequeue().Value);
            Assert.AreEqual(7, secondSearchPath.Dequeue().Value);
            Assert.AreEqual(8, secondSearchPath.Dequeue().Value);
        }

        [TestMethod]
        public void DijkstraSearchWithUnweightedGraph()
        {
            Graph<int> graph = new Graph<int>(false, false);
            graph.Vertices.AddRange(new Vertex<int>[]{
                graph.CreateVertex(1), graph.CreateVertex(2), graph.CreateVertex(3),
                graph.CreateVertex(4), graph.CreateVertex(5), graph.CreateVertex(6)
            });

            graph.Edges.AddRange(graph.CreateEdge(1, 3));
            graph.Edges.AddRange(graph.CreateEdge(1, 2));
            graph.Edges.AddRange(graph.CreateEdge(3, 4));
            graph.Edges.AddRange(graph.CreateEdge(3, 5));
            graph.Edges.AddRange(graph.CreateEdge(5, 6));
            try
            {
                var searchPath = SearchUtility<int>.DijkstraSearch(graph, graph.Vertices[0], graph.Vertices[5]);
                Console.WriteLine("A search algorithm requiring weights was improperly used on an unweighted graph.");
            }
            catch(InvalidOperationException exception)
            {

            }
        }

        [TestMethod]
        public void AStarSearchTest()
        {
            int size = 4;
            int blockedRowIndex = 1;
            int blockedHeightIndex = 1;

            Graph<(double x, double y)> graph = new Graph<(double x, double y)>(true, false);
            for(int y = 0; y < size; y++)
            {
                for(int x = 0; x < size; x++)
                {
                    graph.Vertices.Add(graph.CreateVertex((x, y)));
                }
            }

            graph.Edges.AddRange(graph.CreateEdge((0, 0), (0, 1), 4.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 1), (0, 2), 5.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 2), (0, 3), 6.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 3), (1, 3), 7.0f));
            graph.Edges.AddRange(graph.CreateEdge((1, 3), (2, 3), 8.0f));
            graph.Edges.AddRange(graph.CreateEdge((2, 3), (3, 3), 9.0f));

            graph.Edges.AddRange(graph.CreateEdge((0, 0), (1, 0), 1.0f));
            graph.Edges.AddRange(graph.CreateEdge((1, 0), (2, 0), 2.0f));
            graph.Edges.AddRange(graph.CreateEdge((2, 0), (3, 0), 3.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 0), (3, 1), 4.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 1), (3, 2), 5.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 2), (3, 3), 6.0f));

            var answer = SearchUtility<(double x, double y)>.AStarSearch(graph, graph.Vertices[0], graph.Vertices[graph.Vertices.Count - 1], SearchUtility<(double x, double y)>.CalculateManhattanDistance);
            Assert.AreEqual((0, 0), answer.Dequeue());
            // Assert.AreEqual(, answer.Dequeue());
        }

        [TestMethod]
        public void AStarSearchStartAndEndAreSame()
        {
            int size = 4;
            int blockedRowIndex = 1;
            int blockedHeightIndex = 1;

            Graph<(double x, double y)> graph = new Graph<(double x, double y)>(true, false);
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    graph.Vertices.Add(graph.CreateVertex((x, y)));
                }
            }

            graph.Edges.AddRange(graph.CreateEdge((0, 0), (0, 1), 4.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 1), (0, 2), 5.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 2), (0, 3), 6.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 3), (1, 3), 7.0f));
            graph.Edges.AddRange(graph.CreateEdge((1, 3), (2, 3), 8.0f));
            graph.Edges.AddRange(graph.CreateEdge((2, 3), (3, 3), 9.0f));

            graph.Edges.AddRange(graph.CreateEdge((0, 0), (1, 0), 1.0f));
            graph.Edges.AddRange(graph.CreateEdge((1, 0), (2, 0), 2.0f));
            graph.Edges.AddRange(graph.CreateEdge((2, 0), (3, 0), 3.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 0), (3, 1), 4.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 1), (3, 2), 5.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 2), (3, 3), 6.0f));

            try
            {
                var answer = SearchUtility<(double x, double y)>.AStarSearch(graph, graph.Vertices[0], graph.Vertices[0], SearchUtility<(double x, double y)>.CalculateManhattanDistance);
                Console.WriteLine("The A* search function is invalidly searching for a " +
                    "path where the start and the end are the same");
            }
            catch(InvalidOperationException exception)
            {

            }
        }

        [TestMethod]
        public void AStarSearchWithInvalidPoint()
        {
            int size = 4;
            int blockedRowIndex = 1;
            int blockedHeightIndex = 1;

            Graph<(double x, double y)> graph = new Graph<(double x, double y)>(true, false);
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    graph.Vertices.Add(graph.CreateVertex((x, y)));
                }
            }

            graph.Edges.AddRange(graph.CreateEdge((0, 0), (0, 1), 4.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 1), (0, 2), 5.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 2), (0, 3), 6.0f));
            graph.Edges.AddRange(graph.CreateEdge((0, 3), (1, 3), 7.0f));
            graph.Edges.AddRange(graph.CreateEdge((1, 3), (2, 3), 8.0f));
            graph.Edges.AddRange(graph.CreateEdge((2, 3), (3, 3), 9.0f));

            graph.Edges.AddRange(graph.CreateEdge((0, 0), (1, 0), 1.0f));
            graph.Edges.AddRange(graph.CreateEdge((1, 0), (2, 0), 2.0f));
            graph.Edges.AddRange(graph.CreateEdge((2, 0), (3, 0), 3.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 0), (3, 1), 4.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 1), (3, 2), 5.0f));
            graph.Edges.AddRange(graph.CreateEdge((3, 2), (3, 3), 6.0f));

            try
            {
                var answer = SearchUtility<(double x, double y)>.AStarSearch(graph, graph.Vertices[0], null, SearchUtility<(double x, double y)>.CalculateManhattanDistance);
                Console.WriteLine("The A* search function is invalidly searching for a " +
                    "path where the end doesn't exist");
            }
            catch (InvalidOperationException exception)
            {

            }

            try
            {
                var answer = SearchUtility<(double x, double y)>.AStarSearch(graph, null, graph.Vertices[0], SearchUtility<(double x, double y)>.CalculateManhattanDistance);
                Console.WriteLine("The A* search function is invalidly searching for a " +
                    "path where the start doesn't exist");
            }
            catch (InvalidOperationException exception)
            {

            }
        }
    }
}
