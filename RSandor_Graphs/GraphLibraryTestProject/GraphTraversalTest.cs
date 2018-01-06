using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLib;

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
    }
}
