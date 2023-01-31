using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Immutable;

namespace Algorithm.Graph
{
    public class DijkstraSP
    {
        private double[] _distTo;

        private List<DirectedEdge?> _pathTo;

        private int _source;

        private WeightedDigraph _digraph;

        private IndexedPriorityQueue _pq;

        public DijkstraSP(WeightedDigraph dg, int source)
        {
            _digraph = dg;
            _source = source;
            _distTo = Enumerable.Repeat(double.PositiveInfinity, dg.VectorCount).ToArray();
            _distTo[source] = 0;
            _pathTo = Enumerable.Repeat<DirectedEdge?>(null, dg.VectorCount).ToList();
            _pq = new IndexedPriorityQueue(dg.VectorCount);
            CalculateDistance();
        }

        private void CalculateDistance()
        {
            _pq.Set(_source, 0);
            while (_pq.Count > 0)
            {
                Relax(_pq.DeleteMin().idx);
            }
        }

        private void Relax(int t)
        {
            foreach (var e in _digraph.AdjcentEdges(t))
            {
                if (_distTo[e.To] > _distTo[t] + e.Weight)
                {
                    _distTo[e.To] = _distTo[t] + e.Weight;
                    _pathTo[e.To] = e;
                    _pq.Set(e.To, e.Weight);
                }
            }
        }

        public bool HasPathTo(int t) 
        {
            if (t < _digraph.VectorCount 
                && _distTo[t] != double.PositiveInfinity)
            {
                return true;
            }
            return false;
        }

        public IEnumerable<DirectedEdge> PathTo(int t)
        {
            var returnVal = new List<DirectedEdge>();
            for (DirectedEdge? e = _pathTo[t]; e != null; e = _pathTo[e.Value.From])
            {
                returnVal.Add(e.Value);
            }
            returnVal.Reverse();
            return returnVal;
        }

        public double DistanceTo(int t)
        {
            if (t > -1 && t < _digraph.VectorCount)
                return _distTo[t];
            return double.PositiveInfinity;
        }
    }
}
