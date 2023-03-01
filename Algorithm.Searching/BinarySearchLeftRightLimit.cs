using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Searching
{
    public class BinarySearchLeftRightLimit
    {
        public static int SearchLeftLimit(int[] source, int target)
        {
            var startIdx = 0;
            var endIdx = source.Length - 1;
            while (startIdx <= endIdx)
            {
                var midIdx = (startIdx + endIdx) / 2;
                if (source[midIdx] == target)
                    endIdx = midIdx - 1;
                else if (source[midIdx] < target)
                    startIdx = midIdx + 1;
                else
                    endIdx = midIdx - 1;
            }
            if (startIdx >= source.Length || source[startIdx] != target)
                return -1;
            return startIdx;
        }

        public static int SearchRightLimit(int[] source, int target)
        {
            var startIdx = 0;
            var endIdx = source.Length - 1;
            while (startIdx <= endIdx)
            {
                var midIdx = (startIdx + endIdx) / 2;
                if (source[midIdx] == target)
                    startIdx = midIdx + 1;
                else if (source[midIdx] < target)
                    startIdx = midIdx + 1;
                else
                    endIdx = midIdx - 1;
            }
            if (endIdx < 0 || source[endIdx] != target)
                return -1;
            return endIdx;
        }
    }
}
