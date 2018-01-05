using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    // one graph implementation is to have the list of edges within the vertex, NOT the graph
    // it's supposedly easier to find which vertices a vertex is connected to 
    public class Graph<T> where T : struct, IComparable<T>
    {
        public readonly bool IsWeighted;
        public readonly bool IsDirected;

        public List<Vertex<T>> Vertices;
        public List<Edge<T>> Edges;

        /// <summary>
        /// Graph constructor
        /// </summary>
        /// <param name="isWeighted">A boolean value to determine if the graph is weighted</param>
        /// <param name="isDirected">A boolean value to determine if the graph is directed</param>
        public Graph(bool isWeighted, bool isDirected)
        {
            IsWeighted = isWeighted;
            IsDirected = isDirected;
            Vertices = new List<Vertex<T>>();
            Edges = new List<Edge<T>>();
        }

        /// <summary>
        /// Creates a weighted edge
        /// </summary>
        /// <param name="firstVertex">The starting vertex of the edge</param>
        /// <param name="secondVertex">The ending vertex of the edge</param>
        /// <param name="weight">The weight of the edge</param>
        /// <returns>A list of the edge(s) created</returns>
        public List<Edge<T>> CreateEdge(Vertex<T> firstVertex, Vertex<T> secondVertex, float weight)
        {
            if (Edges.Find(edge => edge.Start == firstVertex && edge.End == secondVertex) != null)
            {
                throw new InvalidOperationException("Can't add an edge that already exists. Multigraphs aren't allowed.");
            }

            if(Vertices.Find(vertex => vertex.Value.Equals(firstVertex.Value)) == null ||
                Vertices.Find(vertex => vertex.Value.Equals(secondVertex.Value)) == null)
            {
                throw new InvalidOperationException("Can't add an edge to vertices that don't exist in the graph.");
            }

            List<Edge<T>> edgesCreated = new List<Edge<T>>();
            edgesCreated.Add(new Edge<T>(firstVertex, secondVertex, IsWeighted, weight));
            if(!IsDirected)
            {
                edgesCreated.Add(new Edge<T>(secondVertex, firstVertex, IsWeighted, weight));
            }
            return edgesCreated;
        }

        /// <summary>
        /// Creates an unweighted edge
        /// </summary>
        /// <param name="firstVertex">The starting vertex of the edge</param>
        /// <param name="secondVertex">The ending vertex of the edge</param>
        /// <returns>A list of the edge(s) created</returns>
        public List<Edge<T>> CreateEdge(Vertex<T> firstVertex, Vertex<T> secondVertex)
        {
            if(Edges.Find(edge => edge.Start == firstVertex && edge.End == secondVertex) != null)
            {
                throw new InvalidOperationException($"Can't add an edge starting at vertex {firstVertex.Value} and ending at {secondVertex.Value}. Multigraphs aren't allowed.");
            }
            
            if (Vertices.Find(vertex => vertex.Value.Equals(firstVertex.Value)) == null ||
                Vertices.Find(vertex => vertex.Value.Equals(secondVertex.Value)) == null)
            {
                throw new InvalidOperationException("Can't add an edge to vertices that don't exist in the graph.");
            }

            List<Edge<T>> edgesCreated = new List<Edge<T>>();
            edgesCreated.Add(new Edge<T>(firstVertex, secondVertex, IsWeighted));
            if (!IsDirected)
            {
                edgesCreated.Add(new Edge<T>(secondVertex, firstVertex, IsWeighted));
            }
            return edgesCreated;
        }

        /// <summary>
        /// Remove an edge from the graph
        /// </summary>
        /// <param name="edgeToRemove">The edge to be removed</param>
        /// <returns></returns>
        public bool RemoveEdge(Edge<T> edgeToRemove)
        {
            return RemoveEdge(edgeToRemove.Start, edgeToRemove.End);
        }

        /// <summary>
        /// Remove an edge from the graph given the specified vertices
        /// </summary>
        /// <param name="firstVertex">The start vertex of the edge to be removed</param>
        /// <param name="secondVertex">The end vertex of the edge to be removed</param>
        /// <returns></returns>
        public bool RemoveEdge(Vertex<T> firstVertex, Vertex<T> secondVertex)
        {
            Edge<T> edgeToRemove = Edges.Find(edge => edge.Start == firstVertex && edge.End == secondVertex);

            if (edgeToRemove != null)
            {   
                if (!IsDirected)
                {
                    Edge<T> secondEdgeToRemove = null;
                    secondEdgeToRemove = Edges.Find(edge => edge.Start == secondVertex && edge.End == firstVertex);

                    if (edgeToRemove != null)
                    {
                        Edges.Remove(edgeToRemove);
                        Edges.Remove(secondEdgeToRemove);
                    }
                    else
                    {
                        throw new InvalidOperationException("The second edge of the undirected graph could not be found. No edges were removed.");
                    }
                }
                else
                {
                    Edges.Remove(edgeToRemove);
                }
            }
            else
            {
                return false;
            }
           
            return true;
        }

        /// <summary>
        /// Create a vertex with the given value if it doesn't already exist in the graph
        /// </summary>
        /// <param name="value">Value of the vertex</param>
        /// <returns></returns>
        public Vertex<T> CreateVertex(T value) 
        {
            Vertex<T> vertexToAdd = Vertices.Find(vertex => vertex.Value.Equals(value));
            if(vertexToAdd != null)
            {
                throw new InvalidOperationException($"Can't add a vertex that already has {value} as the value.");
            }
            return new Vertex<T>(value);
        }

        /// <summary>
        /// Removes a vertex and all of its associated edges if the vertex is found
        /// </summary>
        /// <param name="value"></param>
        /// <returns>A successful or unsuccessful removal</returns>
        public bool RemoveVertex(T value) 
        {
            return RemoveVertex(Vertices.Find(vertex => vertex.Value.Equals(value)));
        }

        /// <summary>
        /// Removes a vertex and all of its associated edges if the vertex is found
        /// </summary>
        /// <param name="vertexToRemove"></param>
        /// <returns>A successful or unsuccessful removal</returns>
        public bool RemoveVertex(Vertex<T> vertexToRemove) 
        {
            Vertex<T> vertexToBeRemoved = Vertices.Find(vertex => vertex.Value.Equals(vertexToRemove.Value));
            if(vertexToBeRemoved != null)
            {
                for (int index = 0; index < Edges.Count; index++)
                {
                    if (Edges[index].Start.Equals(vertexToRemove) || Edges[index].End.Equals(vertexToRemove))
                    {
                        Edges.RemoveAt(index);
                    }
                }

                Vertices.Remove(vertexToBeRemoved);
            }
            else
            {
                return false;
            }
            
            return true;
        }
    }
}
