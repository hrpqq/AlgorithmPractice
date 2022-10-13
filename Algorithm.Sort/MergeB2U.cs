using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class MergeB2U
    {
        private static IList<IComparable> _copy;
        public static void Sort(IList<IComparable> source)
        {
            _copy = new List<IComparable>(source);
            int N = source.Count;
            int subSize = 1;
            while (subSize < N)
            {
                for (int i = 0; i < N - subSize; i += 2 * subSize)
                {
                    InnerMerge(source, i, i + subSize - 1, Math.Min(i + 2 * subSize - 1, N - 1));
                }
                subSize *= 2;
            }
        }

        private static void InnerMerge(IList<IComparable> source, int startIdx, int midIdx, int endIdx)
        {
            int i = startIdx;
            int j = midIdx + 1;
            for (int k = startIdx; k <= endIdx; k++)
            {
                if (i > midIdx) source[k] = _copy[j++];
                else if (j > endIdx) source[k] = _copy[i++];
                else if (Less(_copy[i], _copy[j])) source[k] = _copy[i++];
                else source[k] = _copy[j++];
            }
            // update _copy
            for (int n = startIdx; n <= endIdx; n++)
            {
                _copy[n] = source[n];
            }
        }
    }
}
