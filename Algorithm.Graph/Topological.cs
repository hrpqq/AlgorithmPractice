using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Graph
{
    public class Topological
    {
        public IEnumerable<int> ResTopo { get; private set; }
        public Topological(WeightedDigraph g)
        {
            if (!g.HasLoop())
            {
                CalculateTopo(g);
            }
            else
                throw new ArgumentException("input digraph contains loop, can't resolve its topology");
        }

        private void CalculateTopo(WeightedDigraph g)
        {
            var reversePost = new Stack<int>();
            var hasVisited = new bool[g.VectorCount];
            for (int i = 0; i < g.VectorCount; i++)
            {
                if (hasVisited[i]) continue;
                DepthFS(i, hasVisited, g, reversePost);

            }
            ResTopo = reversePost;
        }

        private void DepthFS(int v, bool[] hasVisited, WeightedDigraph g, Stack<int> vs)
        {
            hasVisited[v] = true;
            foreach (var e in g.AdjcentEdges(v))
            {
                if (hasVisited[e.To]) continue;
                DepthFS(e.To, hasVisited, g, vs);
            }
            vs.Push(v);
        }

        public string OutPutResult()
        {
            var sb = new StringBuilder();
            return string.Join(",", ResTopo.Select(q => q.ToString()));
        }
    }
}
