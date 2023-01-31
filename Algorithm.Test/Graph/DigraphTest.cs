using Algorithm.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Graph
{
    [TestClass]
    public class DigraphTest
    {
        private WeightedDigraph gWithLoop;

        private WeightedDigraph gWithoutLoop;

        public DigraphTest() 
        {
            gWithLoop = new WeightedDigraph(8);
            gWithLoop.AddEdge(new DirectedEdge(0, 1, 1));
            gWithLoop.AddEdge(new DirectedEdge(1, 2, 1));
            gWithLoop.AddEdge(new DirectedEdge(2, 3, 1));
            gWithLoop.AddEdge(new DirectedEdge(3, 4, 1));
            gWithLoop.AddEdge(new DirectedEdge(4, 5, 1));
            gWithLoop.AddEdge(new DirectedEdge(5, 6, 1));
            gWithLoop.AddEdge(new DirectedEdge(6, 7, 1));
            gWithLoop.AddEdge(new DirectedEdge(7, 5, 1));

            gWithoutLoop = new WeightedDigraph(8);
            gWithoutLoop.AddEdge(new DirectedEdge(0, 1, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(1, 2, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(2, 3, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(3, 4, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(4, 5, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(5, 6, 1));
            gWithoutLoop.AddEdge(new DirectedEdge(6, 7, 1));
        }

        [TestMethod]
        public void should_find_loop()
        {
            var res = gWithLoop.HasLoop();
            Assert.IsTrue(res);
        }

        [TestMethod]
        public void should_not_find_loop()
        {
            var res = gWithoutLoop.HasLoop();
            Assert.IsFalse(res);
        }
    }
}
