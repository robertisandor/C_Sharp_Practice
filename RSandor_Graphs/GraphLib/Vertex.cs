using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public class Vertex<T> where T : IComparable<T>
    {
        public readonly T Value;
        public bool Visited { get; set; }
        public List<Vertex<T>> Neighbors = null;

        // is it beneficial to have neighbors for the vertex?
        // or could I use the edges within the graph to determine the neighbor?

        /// <summary>
        /// Vertex constructor
        /// </summary>
        /// <param name="value">Value of the vertex</param>
        /// <param name="neighbors"></param>
        internal Vertex(T value, IEnumerable<Vertex<T>> neighbors = null)        
        {
            Value = value;
            Visited = false;
            Neighbors = neighbors?.ToList() ?? new List<Vertex<T>>(neighbors);   
        }

        internal Vertex(T value, params Vertex<T>[] neighbors) : this(value, (IEnumerable<Vertex<T>>)neighbors)
        {

        }

        public void AddEdge(Vertex<T> neighbor)
        {
            Neighbors.Add(neighbor);
        }

        public void AddEdges(IEnumerable<Vertex<T>> neighbors)
        {
            Neighbors.AddRange(neighbors);
        }

        public bool RemoveEdge(Vertex<T> neighbor)
        {
            return Neighbors.Remove(Neighbors.Find(vertex => vertex.Value.Equals(neighbor.Value)));
        }
    }
}
