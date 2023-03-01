using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Searching
{
    public class BinarySearch
    {
        public static int Search(int[] source, int target)
        {
            var retVal = -1;
            var startIdx = 0;
            var endIdx = source.Length - 1;
            while (startIdx <= endIdx)
            {
                var midIdx = (startIdx + endIdx) / 2;
                if (source[midIdx] == target)
                {
                    retVal = midIdx;
                    break;
                }
                else if (source[midIdx] < target)
                {
                    startIdx = midIdx + 1;
                }
                else
                {
                    endIdx = midIdx - 1;
                }
            }
            return retVal;
        }
    }
}
