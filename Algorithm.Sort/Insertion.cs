using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Sort
{
    public class Insertion
    {
        public static void Sort(IList<IComparable> source)
        {
            for (int i = 1; i < source.Count; i++)
            {
                for (int j = i; j > 0 && Less(source[j], source[j - 1]); j--)
                {
                    Swap(source, j, j - 1);
                }
            }
        }
    }
}
