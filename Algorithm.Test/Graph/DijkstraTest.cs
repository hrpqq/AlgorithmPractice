using Algorithm.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Graph
{
    [TestClass]
    public class DijkstraTest
    {
        private WeightedDigraph gWithoutLoop; 
        private WeightedDigraph g2;

        public DijkstraTest() 
        {
            gWithoutLoop = new WeightedDigraph(8);
            gWithoutLoop.AddEdge(new DirectedEdge(0, 1, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(1, 2, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(2, 3, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(3, 4, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(4, 5, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(5, 6, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(6, 7, 1));

            g2 = new WeightedDigraph(8);
            g2.AddEdge(new DirectedEdge(5, 4, 0.35));
            g2.AddEdge(new DirectedEdge(4, 7, 0.37));
            g2.AddEdge(new DirectedEdge(5, 7, 0.28));
            g2.AddEdge(new DirectedEdge(5, 1, 0.32));
            g2.AddEdge(new DirectedEdge(4, 0, 0.38));

            g2.AddEdge(new DirectedEdge(0, 2, 0.26));
            g2.AddEdge(new DirectedEdge(3, 7, 0.39));
            g2.AddEdge(new DirectedEdge(1, 3, 0.29));
            g2.AddEdge(new DirectedEdge(7, 2, 0.34));
            g2.AddEdge(new DirectedEdge(6, 2, 0.40));

            g2.AddEdge(new DirectedEdge(3, 6, 0.52));
            g2.AddEdge(new DirectedEdge(6, 0, 0.58));
            g2.AddEdge(new DirectedEdge(6, 4, 0.93));
        }

        [TestMethod]
        public void should_build_find_all_shortest_paths_for_every_destinations()
        {
            var dij = new DijkstraSP(gWithoutLoop, 0);
            var path = dij.PathTo(5);
            var res = ComparePath(path,
                new List<DirectedEdge>
                {
                    new DirectedEdge(0,1,1),
                    new DirectedEdge(1,2,1),
                    new DirectedEdge(2,3,1),
                    new DirectedEdge(3,4,1),
                    new DirectedEdge(4,5,1)
                });
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void should_build_find_all_shortest_paths_for_every_destinations_4_g2()
        {
            var dij = new DijkstraSP(g2, 5);
            var path = dij.PathTo(6);
            var res = ComparePath(path,
                new List<DirectedEdge>
                {
                    new DirectedEdge(5,1,0.32),
                    new DirectedEdge(1,3,0.29),
                    new DirectedEdge(3,6,0.52)
                });
            Assert.IsTrue(res);
        }

        private bool ComparePath(IEnumerable<DirectedEdge> left, IEnumerable<DirectedEdge> right)
        {
            foreach (var pair in left.Zip(right))
            {
                if (!pair.First.Equals(pair.Second))
                    return false;
            }
            return true;
        }
    }
}
