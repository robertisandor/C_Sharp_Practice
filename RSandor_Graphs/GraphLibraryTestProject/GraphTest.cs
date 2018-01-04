using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphLib;
using System.Collections.Generic;

namespace GraphLibraryTestProject
{
    [TestClass]
    public class GraphTest
    {
        [TestMethod]
        public void GraphConstructorTest()
        {
            var unweightedUndirectedGraph = new Graph<int>(false, false);
            Assert.AreEqual(false, unweightedUndirectedGraph.IsWeighted);
            Assert.AreEqual(false, unweightedUndirectedGraph.IsDirected);
            Assert.AreEqual(0, unweightedUndirectedGraph.Vertices.Count);
            Assert.AreEqual(0, unweightedUndirectedGraph.Edges.Count);

            var unweightedDirectedGraph = new Graph<int>(false, true);
            Assert.AreEqual(false, unweightedDirectedGraph.IsWeighted);
            Assert.AreEqual(true, unweightedDirectedGraph.IsDirected);
            Assert.AreEqual(0, unweightedDirectedGraph.Vertices.Count);
            Assert.AreEqual(0, unweightedDirectedGraph.Edges.Count);

            var weightedUndirectedGraph = new Graph<int>(true, false);
            Assert.AreEqual(true, weightedUndirectedGraph.IsWeighted);
            Assert.AreEqual(false, weightedUndirectedGraph.IsDirected);
            Assert.AreEqual(0, weightedUndirectedGraph.Vertices.Count);
            Assert.AreEqual(0, weightedUndirectedGraph.Edges.Count);

            var weightedDirectedGraph = new Graph<int>(true, true);
            Assert.AreEqual(true, weightedDirectedGraph.IsWeighted);
            Assert.AreEqual(true, weightedDirectedGraph.IsDirected);
            Assert.AreEqual(0, weightedDirectedGraph.Vertices.Count);
            Assert.AreEqual(0, weightedDirectedGraph.Edges.Count);
        }

        [TestMethod]
        public void GraphCreateVertexTest()
        {
            var unweightedUndirectedGraph = new Graph<int>(false, false);
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(0));
            Assert.AreEqual(0, unweightedUndirectedGraph.Vertices[0].Value);
            Assert.AreEqual(1, unweightedUndirectedGraph.Vertices.Count);

            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(1));
            Assert.AreEqual(1, unweightedUndirectedGraph.Vertices[1].Value);
            Assert.AreEqual(2, unweightedUndirectedGraph.Vertices.Count);

            var unweightedDirectedGraph = new Graph<int>(false, true);
            unweightedDirectedGraph.Vertices.Add(unweightedDirectedGraph.CreateVertex<int>(0));
            Assert.AreEqual(0, unweightedDirectedGraph.Vertices[0].Value);
            Assert.AreEqual(1, unweightedDirectedGraph.Vertices.Count);

            unweightedDirectedGraph.Vertices.Add(unweightedDirectedGraph.CreateVertex<int>(1));
            Assert.AreEqual(1, unweightedDirectedGraph.Vertices[1].Value);
            Assert.AreEqual(2, unweightedDirectedGraph.Vertices.Count);

            var weightedUndirectedGraph = new Graph<int>(true, false);
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(0));
            Assert.AreEqual(0, weightedUndirectedGraph.Vertices[0].Value);
            Assert.AreEqual(1, weightedUndirectedGraph.Vertices.Count);

            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(1));
            Assert.AreEqual(1, weightedUndirectedGraph.Vertices[1].Value);
            Assert.AreEqual(2, weightedUndirectedGraph.Vertices.Count);

            var weightedDirectedGraph = new Graph<int>(true, true);
            weightedDirectedGraph.Vertices.Add(weightedDirectedGraph.CreateVertex<int>(0));
            Assert.AreEqual(0, weightedDirectedGraph.Vertices[0].Value);
            Assert.AreEqual(1, weightedDirectedGraph.Vertices.Count);

            weightedDirectedGraph.Vertices.Add(weightedDirectedGraph.CreateVertex<int>(1));
            Assert.AreEqual(1, weightedDirectedGraph.Vertices[1].Value);
            Assert.AreEqual(2, weightedDirectedGraph.Vertices.Count);
        }

        [TestMethod]
        public void GraphCreateEdgeTest()
        {
            var unweightedUndirectedGraph = new Graph<int>(false, false);
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(0));
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(1));
            unweightedUndirectedGraph.Edges.AddRange(unweightedUndirectedGraph.CreateEdge(unweightedUndirectedGraph.Vertices[0], unweightedUndirectedGraph.Vertices[1]));
            Assert.AreEqual(0, unweightedUndirectedGraph.Edges[0].Start.Value);
            Assert.AreEqual(1, unweightedUndirectedGraph.Edges[0].End.Value);
            Assert.AreEqual(false, unweightedUndirectedGraph.Edges[0].IsWeighted);
            Assert.AreEqual(0.0f, unweightedUndirectedGraph.Edges[0].Weight);

            Assert.AreEqual(1, unweightedUndirectedGraph.Edges[1].Start.Value);
            Assert.AreEqual(0, unweightedUndirectedGraph.Edges[1].End.Value);
            Assert.AreEqual(false, unweightedUndirectedGraph.Edges[1].IsWeighted);
            Assert.AreEqual(0.0f, unweightedUndirectedGraph.Edges[1].Weight);

            Assert.AreEqual(2, unweightedUndirectedGraph.Edges.Count);

            var unweightedDirectedGraph = new Graph<int>(false, true);
            unweightedDirectedGraph.Vertices.Add(unweightedDirectedGraph.CreateVertex<int>(0));
            unweightedDirectedGraph.Vertices.Add(unweightedDirectedGraph.CreateVertex<int>(1));
            unweightedDirectedGraph.Edges.AddRange(unweightedDirectedGraph.CreateEdge(unweightedDirectedGraph.Vertices[0], unweightedDirectedGraph.Vertices[1]));
            Assert.AreEqual(0, unweightedUndirectedGraph.Edges[0].Start.Value);
            Assert.AreEqual(1, unweightedUndirectedGraph.Edges[0].End.Value);
            Assert.AreEqual(false, unweightedUndirectedGraph.Edges[0].IsWeighted);
            Assert.AreEqual(0.0f, unweightedUndirectedGraph.Edges[0].Weight);

            Assert.AreEqual(1, unweightedDirectedGraph.Edges.Count);

            var weightedUndirectedGraph = new Graph<int>(true, false);
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(0));
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(1));
            weightedUndirectedGraph.Edges.AddRange(weightedUndirectedGraph.CreateEdge(weightedUndirectedGraph.Vertices[0], weightedUndirectedGraph.Vertices[1], 4.0f));
            Assert.AreEqual(0, weightedUndirectedGraph.Edges[0].Start.Value);
            Assert.AreEqual(1, weightedUndirectedGraph.Edges[0].End.Value);
            Assert.AreEqual(true, weightedUndirectedGraph.Edges[0].IsWeighted);
            Assert.AreEqual(4.0f, weightedUndirectedGraph.Edges[0].Weight);

            Assert.AreEqual(1, weightedUndirectedGraph.Edges[1].Start.Value);
            Assert.AreEqual(0, weightedUndirectedGraph.Edges[1].End.Value);
            Assert.AreEqual(true, weightedUndirectedGraph.Edges[1].IsWeighted);
            Assert.AreEqual(4.0f, weightedUndirectedGraph.Edges[1].Weight);

            Assert.AreEqual(2, weightedUndirectedGraph.Edges.Count);
        }

        [TestMethod]
        /*
        [ExpectedException(typeof(InvalidOperationException), 
            "The weight of an unweighted graph was inappropriately changed.")]
            */
        public void UnweightedGraphEdgeWeightChangeTest()
        {
            var unweightedUndirectedGraph = new Graph<int>(false, false);
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(0));
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(1));
            unweightedUndirectedGraph.Edges.AddRange(
                unweightedUndirectedGraph.CreateEdge(
                    unweightedUndirectedGraph.Vertices[0], 
                    unweightedUndirectedGraph.Vertices[1]));

            try
            {
                unweightedUndirectedGraph.Edges[0].Weight = 2.0f;
                Console.WriteLine("The weight of an unweighted graph was inappropriately changed.");
            }
            catch(InvalidOperationException exception)
            {
             
            }
        }

        [TestMethod]
        public void WeightedGraphCreateEdgeWithNoWeightTest()
        {
            var weightedUndirectedGraph = new Graph<int>(true, false);
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(0));
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(1));
            
            try
            {
                weightedUndirectedGraph.Edges.AddRange(
                weightedUndirectedGraph.CreateEdge(
                    weightedUndirectedGraph.Vertices[0],
                    weightedUndirectedGraph.Vertices[1]));
                Console.WriteLine("Weighted edges must be given a weight");
            }
            catch(InvalidOperationException exception)
            {

            }
        }

        [TestMethod]
        public void RemoveEdgeTest()
        {
            var unweightedUndirectedGraph = new Graph<int>(false, false);
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(0));
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(1));
            unweightedUndirectedGraph.Edges.AddRange(
                unweightedUndirectedGraph.CreateEdge(
                    unweightedUndirectedGraph.Vertices[0],
                    unweightedUndirectedGraph.Vertices[1]));

            List<Edge<int>> edgesToRemove = new List<Edge<int>>(unweightedUndirectedGraph.RemoveEdge(unweightedUndirectedGraph.Edges[0]));
            Assert.AreEqual(0, edgesToRemove[0].Start.Value);
            Assert.AreEqual(1, edgesToRemove[0].End.Value);
            Assert.AreEqual(0, unweightedUndirectedGraph.Edges.Count);

            unweightedUndirectedGraph.Edges.AddRange(
                unweightedUndirectedGraph.CreateEdge(
                    unweightedUndirectedGraph.Vertices[0],
                    unweightedUndirectedGraph.Vertices[1]));

            List<Edge<int>> otherEdgesToRemove = new List<Edge<int>>(
                unweightedUndirectedGraph.RemoveEdge(
                    unweightedUndirectedGraph.Vertices[0],
                    unweightedUndirectedGraph.Vertices[1]));
            Assert.AreEqual(0, otherEdgesToRemove[0].Start.Value);
            Assert.AreEqual(1, otherEdgesToRemove[0].End.Value);
            Assert.AreEqual(0, unweightedUndirectedGraph.Edges.Count);
        }
    }
}
