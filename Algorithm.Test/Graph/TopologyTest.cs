using Algorithm.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Graph
{
    [TestClass]
    public class TopologyTest
    {
        private WeightedDigraph g1;
        private WeightedDigraph g2;
        public TopologyTest() 
        {
            g1 = new WeightedDigraph(13);
            g1.AddEdge(new DirectedEdge(0, 1, 1));
            g1.AddEdge(new DirectedEdge(0, 5, 1));
            g1.AddEdge(new DirectedEdge(0, 6, 1));

            g1.AddEdge(new DirectedEdge(2, 0, 1));
            g1.AddEdge(new DirectedEdge(2, 3, 1));

            g1.AddEdge(new DirectedEdge(3, 5, 1));

            g1.AddEdge(new DirectedEdge(5, 4, 1));

            g1.AddEdge(new DirectedEdge(6, 4, 1));
            g1.AddEdge(new DirectedEdge(6, 9, 1));

            g1.AddEdge(new DirectedEdge(7, 6, 1));

            g1.AddEdge(new DirectedEdge(8, 7, 1));

            g1.AddEdge(new DirectedEdge(9, 10, 1));
            g1.AddEdge(new DirectedEdge(9, 11, 1));
            g1.AddEdge(new DirectedEdge(9, 12, 1));

            g1.AddEdge(new DirectedEdge(11, 12, 1));


            g2 = new WeightedDigraph(5);
            g2.AddEdge(new DirectedEdge(2, 3, 1));
            g2.AddEdge(new DirectedEdge(3, 4, 1));
            g2.AddEdge(new DirectedEdge(4, 0, 1));
            g2.AddEdge(new DirectedEdge(0, 1, 1));
        }

        [TestMethod]
        public void should_calculate_the_Topology_correctlly_g2()
        {
            var topo = new Topological(g2);
            var res = topo.OutPutResult();
        }

        [TestMethod]
        public void should_calculate_the_Topology_correctlly()
        {
            var topo = new Topological(g1);
            var res = topo.OutPutResult();
        }

    }
}
