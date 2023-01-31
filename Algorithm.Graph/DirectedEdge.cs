using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Graph
{
    public struct DirectedEdge: IEquatable<DirectedEdge>
    {
        public int From { get; }
        public int To { get; }
        public double Weight { get; }
        public DirectedEdge(int from, int to, double weight)
        {
            From = from;
            To = to;
            Weight = weight;
        }

        public bool Equals(DirectedEdge other)
        {
            if (From == other.From
                && To == other.To
                && Weight == other.Weight)
            { 
                return true;
            }
            return false;
        }
    }
}
