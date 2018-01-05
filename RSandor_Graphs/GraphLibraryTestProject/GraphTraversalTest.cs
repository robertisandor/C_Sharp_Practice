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

        }

        [TestMethod]
        public void DijkstraSearchTest()
        {

        }
    }
}
