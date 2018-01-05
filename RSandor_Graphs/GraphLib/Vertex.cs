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

        // is it beneficial to have neighbors for the vertex?
        // or could I use the edges within the graph to determine the neighbor?

        /// <summary>
        /// Vertex constructor
        /// </summary>
        /// <param name="value">Value of the vertex</param>
        /// <param name="neighbors"></param>
        internal Vertex(T value)
        {
            Value = value;
            Visited = false;
        }
    }
}
