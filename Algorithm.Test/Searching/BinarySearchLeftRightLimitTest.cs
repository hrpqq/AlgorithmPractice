using Algorithm.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Searching
{
    [TestClass]
    public class BinarySearchLeftRightLimitTest
    {
        [TestMethod]
        public void should_find_left_limit()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearchLeftRightLimit.SearchLeftLimit(source, 11);
            Assert.AreEqual(res, 4);
        }

        [TestMethod]
        public void should_find_right_limit()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearchLeftRightLimit.SearchRightLimit(source, 11);
            Assert.AreEqual(res, 5);
        }

        [TestMethod]
        public void should_not_find_left_limit()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearchLeftRightLimit.SearchLeftLimit(source, 21);
            Assert.AreEqual(res, -1);
        }

        [TestMethod]
        public void should_not_find_right_limit()
        {
            var source = new int[] { -1, 1, 2, 7, 11, 11, 20 };
            var res = BinarySearchLeftRightLimit.SearchRightLimit(source, 21);
            Assert.AreEqual(res, -1);
        }
    }
}
