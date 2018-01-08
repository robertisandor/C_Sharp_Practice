using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * difference between a library and an executable is the lack of an entry point
 * difference between a dll and a static library is that the dll only loads the parts that you use
 * static library loads the entire thing
 * build - only adds the difference from your last build
 * clean - removes everything in the bin and obj folders
 * rebuild - cleans, then builds; useful if you change the structure/organization of your directory
 */
namespace GraphLib
{
    public class Edge<T> where T : IComparable<T>
    {
        #region Class variables

        public Vertex<T> Start;
        public Vertex<T> End;

        public readonly bool IsWeighted;

        #endregion

        #region Class properties

        float weight;
        public float Weight
        {
            get => weight;
            set
            {
                if (!IsWeighted)
                {
                    throw new InvalidOperationException("Cannot change weight in an unweighted graph");
                }

                weight = value;
            }
        }
    
        #endregion

        #region Edge constructors
        /// <summary>
        /// Weighted edge constructor
        /// </summary>
        /// <param name="start">The start vertex of the edge</param>
        /// <param name="end">The end vertex of the edge</param>
        /// <param name="isWeighted"></param>
        /// <param name="weight"></param>
        internal Edge(Vertex<T> start, Vertex<T> end, bool isWeighted, float weight)
        {
            IsWeighted = isWeighted;
            if(!IsWeighted)
            {
                if(Math.Abs(weight - 1.0f) > 0.000001)
                {
                    throw new InvalidOperationException("Unweighted edges can't be given a weight");
                } 
            }
            else
            {
                Weight = weight;
            }
            Start = start;
            End = end;
        }

        /// <summary>
        /// Unweighted edge constructor
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="isWeighted"></param>
        internal Edge(Vertex<T> start, Vertex<T> end, bool isWeighted)
        {
            IsWeighted = isWeighted;

            if(isWeighted)
            {
                throw new InvalidOperationException("Weighted edges must be given a weight");
            }

            Start = start;
            End = end;
        }

        #endregion
    }
}
