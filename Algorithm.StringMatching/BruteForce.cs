namespace Algorithm.StringMatching
{
    public class BruteForce
    {
        public static int MatchFirst(string source, string pattern)
        {
            int returnVal = -1;
            int sl, pl;
            sl = source.Length;
            pl = pattern.Length;
            if (sl >= pl && pl > 0)
            {
                bool find = false;
                for (int i = 0; i < sl - pl; i++)
                {
                    for (int j = 0; j < pl; j++)
                    {
                        if (source[i + j] != pattern[j])
                            break;
                        else   
                        {
                            if (j == pl - 1)
                            {
                                returnVal = i;
                                find = true;
                                break;
                            }
                        }
                    }
                    if (find) break;
                }
            }
            return returnVal;
        }
    }
}