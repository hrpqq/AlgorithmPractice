using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class Merge
    {
        private static IList<IComparable> _copy;
        public static void Sort(IList<IComparable> source)
        {
            _copy = new List<IComparable>(source);
            InnerSort(source, 0, source.Count - 1);
        }

        private static void InnerSort(IList<IComparable> source, int startIdx, int endIdx)
        {
            if (startIdx >= endIdx) return;
            int midIdx = startIdx + (endIdx - startIdx) / 2;
            InnerSort(source, startIdx, midIdx);
            InnerSort(source, midIdx + 1, endIdx);
            InnerMerge(source, startIdx, midIdx, endIdx);
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
