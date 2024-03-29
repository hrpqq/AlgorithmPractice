﻿using Algorithm.StringMatching;
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

            Assert.AreEqual(root, 
                root.Children['a'].Fallback);
            Assert.AreEqual(root.Children['b'], 
                root.Children['a'].Children['b'].Fallback);
            Assert.AreEqual(root.Children['b'].Children['c'], 
                root.Children['a'].Children['b'].Children['c'].Fallback);
            Assert.AreEqual(root.Children['b'].Children['c'].Children['d'],
                root.Children['a'].Children['b'].Children['c'].Children['d'].Fallback);

            Assert.AreEqual(root,
                root.Children['b'].Fallback);
            Assert.AreEqual(root.Children['c'],
                root.Children['b'].Children['c'].Fallback);
            Assert.AreEqual(root,
               root.Children['b'].Children['c'].Children['d'].Fallback);

            Assert.AreEqual(root,
                root.Children['c'].Fallback);
        }

        [TestMethod]
        public void should_find_multiple_targets_in_source_string()
        {
            // "cnc" + "dnn" + "abcd" + "nn" +
            var source = ("cnc" + "dnn" + "abcd" + "nn" + "abce" + "nn" + "bcd" + "ncnccd" + "bcd" + "c").ToCharArray();
            var word1 = new char[] { 'a', 'b', 'c', 'd' };
            var word2 = new char[] { 'b', 'c', 'd' };
            var word3 = new char[] { 'c' };

            var res = AhoCorasick.MatchTargets(source, new List<char[]> { word1, word2, word3 });
            Assert.AreEqual(10, res.Count);
        }
    }
}
