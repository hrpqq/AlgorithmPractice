using Algorithm.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Test.Sort
{
    [TestClass]
    public class BubbleTest
    {
        [TestMethod]
        public void ShouldHasBeenSorted()
        {
            var source = new List<int>() { -2, 1, 7, 2, 5, -9, 11, 11, -2, -2 };
            var a = new List<int>(source);
            var b = new List<int>(source);
            a = bubble<int>.Sort(a).ToList();
            b.Sort();
            Assert.AreEqual(true, ElementEqual(a, b));
        }
    }
}
