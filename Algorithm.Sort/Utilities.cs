using System.Text;

namespace Algorithm.Sort
{
    public class Utilities
    {
        public static bool Less(IComparable lift, IComparable right) => lift.CompareTo(right) < 0;

        public static void Swap<T>(IList<T> source, int a, int b)
        {
            var temp = source[a];
            source[a] = source[b];
            source[b] = temp;
        }


        public static bool IsSort(IList<IComparable> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (list[i].CompareTo(list[i + 1]) > 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ElementEqual(IEnumerable<IComparable> lift, IEnumerable<IComparable> right)
        {
            if(lift.Count() != right.Count())
                return false;
            for (int i = 0; i < lift.Count(); i++)
            {
                if (lift.ElementAt(i).CompareTo(right.ElementAt(i)) != 0)
                    return false;
            }
            return true;
        }

        public static bool ElementEqual<T>(IEnumerable<T> lift, IEnumerable<T> right) where T : IComparable<T>
        {
            if (lift.Count() != right.Count())
                return false;
            for (int i = 0; i < lift.Count(); i++)
            {
                if (lift.ElementAt(i).CompareTo(right.ElementAt(i)) != 0)
                    return false;
            }
            return true;
        }

        public static void Show(IList<IComparable> list) 
        {
            StringBuilder sb = new StringBuilder();
            new List<IComparable>(list).ForEach(it => sb.Append(it.ToString()).Append(" "));
            Console.WriteLine(
$@"
start
{sb.ToString()}
end
");
        } 
    }
} 