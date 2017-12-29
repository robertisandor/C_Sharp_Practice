using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphLib
{
    public class Edge
    {
        Vertex Start;
        Vertex End;

        bool isWeighted;

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
