using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public class Vertex<T>
    {
        // vertex should have some name or indicator or position
        // for a map, it would have a position and a name
        // the position and name would be in the T object though...
        
        // vertex should have an internal constructor so it can't be created by itself
        // because it shouldn't be created outside of the context of a graph
        internal Vertex()
        {

        }
    }
}
