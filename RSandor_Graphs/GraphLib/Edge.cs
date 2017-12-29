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
    //  I should make this an IComparable at some point.
    // I will need to compare the edges to each other to most likely compare the weights
    public class Edge<T>
    {
        Vertex<T> Start;
        Vertex<T> End;

        bool isWeighted;

        // have an internal constructor so that an individual can't create an Edge by itself
        // (creating an Edge by itself doesn't make sense because it isn't used except in the context of a graph)
        // there is a CreateEdge function within the Graph class that the user can use to 
        internal Edge(bool isWeighted)
        {
            this.isWeighted = isWeighted;
        }

        float weight;
        public float Weight
        {
            get => weight;
            set
            {
                if (!isWeighted)
                {
                    throw new InvalidOperationException("Cannot change weight in an unweighted graph");
                }

                weight = value;
            }
        }
    }
}
