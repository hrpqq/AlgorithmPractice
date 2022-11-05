using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.StringMatching
{
    public class KnuthMorrisPratt
    {
        public static int MatchFirst(char[] source, char[] pattern)
        {
            int sl = source.Length;
            int pl = pattern.Length;
            var jumpTab = InitJumpTable(pattern);
            for (int i = 0; i <= sl - pl; )
            {
                for (int j = 0; j < pl; j++)
                {
                    if (source[i + j] != pattern[j])
                    {
                        i += jumpTab[j];
                        break;
                    }
                    if (j == (pl - 1))
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        public static int[] InitJumpTable(char[] pattern)
        {
            int[] jumpTab = new int[pattern.Length];
            int pl = pattern.Length;
            jumpTab[0] = -1;
            /*
            if source chars are "a b c d a b c"
            and we try to get the first three chars' jump num
             */
            int prefixLastCharIndex = -1;
            for (int lastCharIndex = 1; lastCharIndex < pl; lastCharIndex++)
            {
                /* for current sub string -- [0, lastCharIndex]
                 * if both last char of current_prefix_string and current_sub_string are not matching any more
                 * then we can try to find a new prefix_string inside current_prefix_string -- By visiting jumpTab[prefixLastCharIndex]
                 * if its value is't -1, then it means a new shorter prefix_string can be provide to give another try
                 * if it does match, then update jumpTab[lastCharIndex], and move to next iteration (in the outter loop).
                 * otherwise we go more deep inside this short prefix_string, and find a even more shorter prefix_string, 
                 * untill we can't find a prefix_string anymore. (By runing the inner loop)
                 */
                while (prefixLastCharIndex != -1
                    && pattern[prefixLastCharIndex + 1] != pattern[lastCharIndex])
                {
                    prefixLastCharIndex = jumpTab[prefixLastCharIndex];
                }
                // increase prefixLastCharIndex when last chars are the same
                // so we cam move to next last char matching test
                if (pattern[prefixLastCharIndex + 1] == pattern[lastCharIndex])
                {
                    prefixLastCharIndex++;
                }
                jumpTab[lastCharIndex] = prefixLastCharIndex;
            }
            for (int i = 0; i < pl; i++)
            {
                jumpTab[i] = jumpTab[i] == -1 
                                ? i == 0 ? 1 : i
                                : i - jumpTab[i];
            }
            return jumpTab;
        }
    }
}
