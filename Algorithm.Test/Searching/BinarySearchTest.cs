using Algorithm.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Searching
{
    [TestClass]
    public class BinarySearchTest
    {
        [TestMethod]
        public void should_find_target()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearch.Search(source, 7);
            Assert.AreEqual(res, 3);
        }

        [TestMethod]
        public void should_not_find_target()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearch.Search(source, 21);
            Assert.AreEqual(res, -1);
        }

        [TestMethod]
        public void should_not_find_first()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearch.Search(source, -1);
            Assert.AreEqual(res, 0);
        }

        [TestMethod]
        public void should_not_find_last()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearch.Search(source, 20);
            Assert.AreEqual(res, source.Length - 1);
        }

        [TestMethod]
        public void should_not_find_eleven()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearch.Search(source, 11);
            Assert.IsTrue(res == 5 || res == 6);
        }
    }
}
