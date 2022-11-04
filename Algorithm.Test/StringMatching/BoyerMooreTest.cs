using Algorithm.StringMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.StringMatching
{
    [TestClass]
    public class BoyerMooreTest
    {
        [TestMethod]
        public void shouldFindOneMatchingAtIndexSeven()
        {
            var source = "abccbaddxxfbfyybfxxfbffq".ToCharArray();
            var pattern = "dxxfbfyybfxxfbf".ToCharArray();
            var index = BoyerMoore.MatchFirst(source, pattern);
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual(7, index);
        }

        [TestMethod]
        public void should_init_jump_table_correctly()
        {
            string pattern = "bcde" + "tttt" + "cde" + "de" + "tt" + "de" + "tt" + "abcde";
            char[] patternChars = pattern.ToCharArray();
            int[] expectJumpTab = new int[]
              {
                -1,-1,-1,-1,-1,
                -1,-1,-1,-1,-1,
                -1,-1,-1,-1,-1,
                -1,-1,-1,-1,-1,
                20, 13, 7, 7
              };
            var jumpTab = BoyerMoore.InitJumpTable(patternChars);
            for (int i = 0; i < Math.Max(patternChars.Length, expectJumpTab.Length); i++)
            {
                Assert.AreEqual(expectJumpTab[i], jumpTab[i]);
            }
        }

        [TestMethod]
        public void should_init_existhing_char_dictionary_correctly()
        {
            char[] patternChars = "acabbca".ToCharArray();
            var expectCharDic = new Dictionary<char, List<int>>
              {
                { 'a', new List<int> { 0, 2, 6} },
                { 'c', new List<int> { 1, 5} },
                { 'b', new List<int> { 3, 4} },
              };
            var charDic = BoyerMoore.InitExistingCharDic(patternChars);

            charDic.Keys.ToList().ForEach(key =>
            {
                var l1 = charDic[key];
                var l2 = expectCharDic[key];
                l1.Sort();
                l2.Sort();
                var l3 = l1.Zip(l2).ToList();
                l3.ForEach(pair => Assert.AreEqual(pair.First, pair.Second));
            });
        }
    }
}
