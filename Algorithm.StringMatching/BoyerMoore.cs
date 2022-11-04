using System.Collections.ObjectModel;

namespace Algorithm.StringMatching
{
    public class BoyerMoore
    {
        public static int MatchFirst(char[] source, char[] pattern)
        {
            int returnVal = -1;
            var jumpTab = InitJumpTable(pattern);
            var existingCharDic = InitExistingCharDic(pattern);

            for (int i = 0; i <= source.Length - pattern.Length; )
            {
                char? badChar = null;
                int badCharPatternIdx = -1;
                List<char> goodSuffix = new List<char>();

                // try to get bad char and goog suffix
                // j for source, k for pattern
                for (int j = i + pattern.Length - 1, k = pattern.Length - 1; 
                    j >= i; 
                    j--, k--)
                {
                    if (source[j] == pattern[k])
                    {
                        goodSuffix.Add(source[j]);
                    }
                    else
                    {
                        badChar = source[j];
                        badCharPatternIdx = k;
                        goodSuffix.Reverse();
                        break;
                    }
                }

                // no bad char means find a match, return current index
                if (badChar == null)
                {
                    returnVal = i;
                    break;
                }
                // otherwise, jump to new location that get from BC and GS, and matching all over again.
                else 
                {
                    int jumpByBC = MatchBadCharactor(pattern, badChar.Value, badCharPatternIdx, existingCharDic);
                    int jumpByGS = MatchGoodSuffix(pattern, jumpTab, goodSuffix);
                    int jumpByMax = Math.Max(jumpByBC, jumpByGS);
                    i += jumpByMax == -1 
                            ? 1 
                            : jumpByMax;
                }
            }
            return returnVal;
        }

        public static int MatchBadCharactor(char[] pattern,char badChar, 
            int badCharPatternIdx, IReadOnlyDictionary<char, List<int>> charDic)
        {
            int jumpBy = -1;
            if (charDic.ContainsKey(badChar))
            {
                var candidateIndexes = charDic[badChar];
                jumpBy = candidateIndexes.OrderByDescending(q => q).FirstOrDefault(q => q < badCharPatternIdx, -1);
            }
            else
            {
                jumpBy = pattern.Length;
            }
            return jumpBy;
        }

        public static int MatchGoodSuffix(char[] pattern, int[] jumpTab, List<char> goodSuffix)
        {
            int jumpBy = -1;
            var pl = pattern.Length;
            for (int i = pl - goodSuffix.Count; i < pl; i++)
            {
                jumpBy = jumpTab[i];
                if (jumpBy != -1)
                    break;
            }
            return jumpBy;
        }

        public static ReadOnlyDictionary<char, List<int>> InitExistingCharDic(char[] pattern)
        {
            var dic = new Dictionary<char, List<int>>();
            for (int i = 0; i < pattern.Length; i++)
            {
                if (!dic.ContainsKey(pattern[i]))
                    dic[pattern[i]] = new List<int>();
                dic[pattern[i]].Add(i);
            }
            return new ReadOnlyDictionary<char, List<int>>(dic);
        }

        public static int[] InitJumpTable(char[] pattern)
        {
            var totalLength = pattern.Length;
            var jumpTable = Enumerable.Repeat(-1, totalLength).ToArray();
            for (int i = totalLength - 1; i >= 1; i--)
            {
                var forwardIdx = FindFirstForward(i, totalLength - i);
                jumpTable[i] = forwardIdx == -1
                                ? forwardIdx
                                : i - forwardIdx;
            }

            #region Find Forward
            int FindFirstForward(int startIndex, int count)
            {
                int returnVal = -1;
                for (int j = startIndex - 1; j >= 0; j--)
                {
                    if (CompareInternalCharactors(pattern, startIndex, count, j, count))
                    {
                        returnVal = j;
                        break;
                    }  
                }
                return returnVal;
            }
            #endregion

            return jumpTable;
        }

        private static bool CompareInternalCharactors(char[] source, int lStart, int lCount, int rStart, int rCount)
        {
            bool returnVal = true;
            if (lCount == rCount)
            {
                for (int i = 0; i < lCount; i++)
                {
                    if (source[lStart + i] != source[rStart + i])
                    {
                        returnVal = false;
                        break;
                    }
                }
            }
            return returnVal;
        }

    }
}
