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
            /*
            graph.Edges.AddRange(new Edge<int>[] {
                graph.CreateEdge()
            });
            */
        }
    }
}
