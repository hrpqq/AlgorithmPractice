using Algorithm.Sort;
using Algorithm.StringMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.StringMatching
{
    [TestClass]
    public class BruteForceTest
    {
        [TestMethod]
        public void shouldFindOneMatchingAtIndexSeven()
        {
            string source = "abccbaddnbfq";
            string pattern = "dnb";
            var index = BruteForce.MatchFirst(source, pattern);
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual(7, index);
        }

        [TestMethod]
        public void shouldFindOneMatchingAtIndexZero()
        {
            string source = "abccbaddnbfq";
            string pattern = "abc";
            var index = BruteForce.MatchFirst(source, pattern);
            Assert.AreEqual(0, index);
        }

        [TestMethod]
        public void shouldFindNoMatching()
        {
            string source = "abccbaddnbfq";
            string pattern = "dns";
            var index = BruteForce.MatchFirst(source, pattern);
            Assert.AreEqual(-1, index);
        }
    }
}
