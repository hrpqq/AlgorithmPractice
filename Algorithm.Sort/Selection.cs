using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class Selection
    {
        public static void Sort(IList<IComparable> source)
        {
            var minIdx = 0;
            for (int i = 0; i < source.Count - 1; i++)
            {
                minIdx = i;
                for (int j = i + 1; j < source.Count; j++)
                {
                    if (source[j].CompareTo(source[minIdx]) < 0)
                    {
                        minIdx = j;
                    }
                }
                if (i != minIdx)
                {
                    Swap(source, i, minIdx);
                }
            }
            
        }
    }
}
