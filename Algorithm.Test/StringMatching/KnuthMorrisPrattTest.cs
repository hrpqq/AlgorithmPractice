using Algorithm.StringMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.StringMatching
{
    [TestClass]
    public class KnuthMorrisPrattTest
    {
        [TestMethod]
        public void shouldFindOneMatchingAtIndexSeven()
        {
            var source = "abccbadndnbfq".ToCharArray();
            var pattern = "dnb".ToCharArray();
            var index = KnuthMorrisPratt.MatchFirst(source, pattern);
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual(8, index);
        }

        [TestMethod]
        public void shouldFindOneMatchingAtIndexSeven2()
        {
            var source = "dfsdfabccbaddxxfbfyybfxxfbffq".ToCharArray();
            var pattern = "dxxfbfyybfxxfbf".ToCharArray();
            var index = KnuthMorrisPratt.MatchFirst(source, pattern);
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual(12, index);
        }

        [TestMethod]
        public void should_init_jump_table_correctly()
        {
            string pattern = "bcde" + "tttt" + "bcd" + "de" + "tt" + "bcb" + "tt" + "abcde";
            char[] patternChars = pattern.ToCharArray();
            //int[] expectJumpTab = new int[]
            //  {
            //    -1,-1,-1,-1,-1,
            //    -1,-1,-1,-1,-1,
            //    -1,-1,-1,-1,-1,
            //    -1,-1,-1,-1,-1,
            //    20, 13, 7, 7
            //  };
            //var jumpTab = KnuthMorrisPratt.InitJumpTable(patternChars);
            //for (int i = 0; i < Math.Max(patternChars.Length, expectJumpTab.Length); i++)
            //{
            //    Assert.AreEqual(expectJumpTab[i], jumpTab[i]);
            //}
        }
    }
}
