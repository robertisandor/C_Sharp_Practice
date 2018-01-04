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

        /// <summary>
        /// Vertex constructor
        /// </summary>
        /// <param name="value">Value of the vertex</param>
        /// <param name="neighbors"></param>
        // internal Vertex(T value, IEnumerable<Vertex<T>> neighbors = null)
        internal Vertex(T value)
        {
            Value = value;
        }
    }
}
