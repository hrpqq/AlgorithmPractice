using Algorithm.StringMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.StringMatching
{
    [TestClass]
    public class AhoCorasickTest
    {
        [TestMethod]
        public void should_build_trie_tree_correctly()
        {
            var word1 = new char[] { 'a', 'b', 'c', 'd' };
            var word2 = new char[] { 'b', 'c', 'd' };
            var word3 = new char[] { 'c' };

            var root = AhoCorasick.BuildTire(new List<char[]> { word1, word2, word3 });
            Assert.AreNotEqual(null, root);
        }
    }
}
