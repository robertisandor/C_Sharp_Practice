using GraphLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RSandor_Graphs
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var graph = new Graph<int>(true);
            var edge = graph.CreateEdge();
            edge.Weight = 3;
            */
            
            var unweightedUndirectedGraph = new Graph<int>(false, false);
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(0));
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(1));
            unweightedUndirectedGraph.Vertices.Add(unweightedUndirectedGraph.CreateVertex<int>(2));
            unweightedUndirectedGraph.Edges.AddRange(unweightedUndirectedGraph.CreateEdge(unweightedUndirectedGraph.Vertices[0], unweightedUndirectedGraph.Vertices[1]));
            foreach (var vertex in unweightedUndirectedGraph.Vertices)
            {
                Console.WriteLine($"vertex.Value = {vertex.Value}");
            }
            foreach (var edge in unweightedUndirectedGraph.Edges)
            {
                Console.WriteLine($"edge.Start = {edge.Start.Value} & edge.End = {edge.End.Value}");
            }

            var weightedUndirectedGraph = new Graph<int>(true, false);
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(0));
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(1));
            weightedUndirectedGraph.Vertices.Add(weightedUndirectedGraph.CreateVertex<int>(2));
            unweightedUndirectedGraph.Edges.AddRange(unweightedUndirectedGraph.CreateEdge(unweightedUndirectedGraph.Vertices[0], unweightedUndirectedGraph.Vertices[1]));
            Console.ReadKey();
        }
    }
}
