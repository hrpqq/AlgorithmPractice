using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class Quick
    {
        public static void Sort(IList<IComparable> source)
        {
            InnerSort(source, 0, source.Count - 1);
        }

        private static void InnerSort(IList<IComparable> source, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                var mid = Split(source, startIndex, endIndex);
                if (mid - startIndex > 1) InnerSort(source, startIndex, mid - 1);
                if (endIndex - mid > 1) InnerSort(source, mid + 1, endIndex);
            }
        }

        private static int Split(IList<IComparable> source, int startIndex, int endIndex)
        {
            var midIndex = startIndex;
            for (int i = startIndex + 1; i <= endIndex; i++)
            {
                var cmpRes = source[startIndex].CompareTo(source[i]);
                if (cmpRes > 0)
                {
                    Swap(source, midIndex + 1, i);
                    midIndex++;
                }
            }
            Swap(source, startIndex, midIndex);
            return midIndex;
        }
    }
}
