using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class Shell
    {
        public static void Sort(IList<IComparable> source)
        {
            int N = source.Count;
            int h = 1;
            while (h < N / 3) h = 3 * h + 1;
            while (h >= 1)
            {
                for (int i = h; i < N; i++)
                {
                    for (int j = i; j >= h && Less(source[j], source[j - h]); j -= h)
                    {
                        Swap(source, j, j - h);
                    }
                }
                h /= 3;
            }
        }
    }
}
