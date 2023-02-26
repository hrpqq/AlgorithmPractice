using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class bubble<T> where T : IComparable<T>
    {
        public static IEnumerable<T> Sort(IList<T> source)
        {
            int firstSwapIdx = 0;
            for (int i = source.Count() - 1; i > 0; i--)
            {
                bool first = false;
                for (int j = firstSwapIdx; j < i; j++)
                {
                    var res = source[j].CompareTo(source[j + 1]);
                    if (res > 0)
                    {
                        Swap(source, j, j + 1);
                        if (!first)
                        {
                            first = true;
                            firstSwapIdx = Max(j - 1, 0);
                        }
                            
                    }
                }
            }
            return source;
        }

        private static int Max(int right, int left)
            => right > left ? right : left;

        private static int Min(int right, int left)
            => right < left ? right : left;
    }
}
