using System.Text;

namespace Algorithm.StringMatching
{
    public class RabinKarp
    {
        public const uint PRIME_NUM = 9973;
        public static int MatchFirst(string source, string pattern)
        {
            int result = -1;
            if (source.Length >= pattern.Length && pattern.Length > 0)
            {
                int patternLength = pattern.Length;
                uint? patternHash = GetStringHashCode(pattern);

                string lastWindowString = String.Empty;
                uint? lastWindowHash = null;
                for (int i = 0; i < source.Length - patternLength; i++)
                {
                    var curWindowString = source.Substring(i, patternLength);
                    uint? curWindowHash = lastWindowHash == null ? GetStringHashCode(curWindowString) : null;

                    if (lastWindowHash != null
                        && !string.IsNullOrEmpty(lastWindowString))
                    {
                        curWindowHash = GetRollingHashCode(lastWindowString,
                                                            lastWindowHash.Value, 
                                                            source[i + patternLength - 1].ToString());
                    }
                    if (curWindowHash == patternHash
                        && pattern == curWindowString)
                    {
                        result = i;
                        break;
                    }

                    lastWindowString = curWindowString;
                    lastWindowHash = curWindowHash;
                }
            }
            return result;
        }

        public static uint? GetRollingHashCode(string prevStr, uint prevStrHash, string nextChar)
        {
            var prevBytes = String2Bytes(prevStr);
            var prevHeadBytes = String2Bytes(prevStr.Substring(0, 1));
            var nextCharBytes = String2Bytes(nextChar);
            return GetBytesRollingHashCode(prevBytes, prevHeadBytes, prevStrHash, nextCharBytes);
        }


        public static uint? GetBytesRollingHashCode(byte[] prevBytes, byte[] prevHeadBytes, uint prevBytesHash, byte[] nextBytes)
        {
            uint? result;

            int baseBytesLength = prevBytes.Length - prevHeadBytes.Length;
            var baseHash = GetBytesHashCode(CreateBaseBytes(baseBytesLength));
            var headValue = ParseBytes2Uint(prevHeadBytes);

            var headHash = (headValue * baseHash % PRIME_NUM);
            var restHash = prevBytesHash >= headHash 
                            ? prevBytesHash - headHash
                            : PRIME_NUM - headHash + prevBytesHash;

            var nextBytesHash = GetBytesHashCode(nextBytes);
            var nextBaseValue = ParseBytes2Uint(CreateBaseBytes(nextBytes.Length));

            var newHash = (restHash * nextBaseValue + nextBytesHash) % PRIME_NUM;
            result = newHash;
            
            return result;
        }

        public static uint? GetStringHashCode(string s)
        {
            if (s.Length > 0)
                return GetBytesHashCode(String2Bytes(s));
            return null;
        }

        public static uint? GetBytesHashCode(byte[] bytes)
        {
            uint? result = null;
            uint GetBaseHash(int count)
            {
                ulong tempHash = 1;
                ulong baseValue = 1ul << 32;
                for (int i = 0; i < count; i++)
                {
                    tempHash = tempHash * baseValue % PRIME_NUM;
                }
                return (uint)tempHash;
            }

            if (bytes.Length > 0)
            {
                uint prevHash = 0;
                for (int i = bytes.Length - 1, j = 0; i >= 0; i -= 4, j++)
                {
                    ulong curVal = (
                                bytes[i]
                                | (i - 1 >= 0 ? (((uint)bytes[i - 1]) << 8) : 0)
                                | (i - 2 >= 0 ? (((uint)bytes[i - 2]) << 16) : 0)
                                | (i - 3 >= 0 ? (((uint)bytes[i - 3]) << 24) : 0)
                               );
                    prevHash = (uint)((curVal * GetBaseHash(j) + prevHash) % PRIME_NUM);
                }
                result = prevHash;
            }
            return result;
        }

        private static uint? ParseBytes2Uint(byte[] bytes)
        {
            if (bytes.Length > 0)
            {
                int i = bytes.Length - 1;
                return bytes[i]
                        | (i - 1 >= 0 ? (((uint)bytes[i - 1]) << 8) : 0)
                        | (i - 2 >= 0 ? (((uint)bytes[i - 2]) << 16) : 0)
                        | (i - 3 >= 0 ? (((uint)bytes[i - 3]) << 24) : 0);
            }
            return null;
        }

        private static byte[] String2Bytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        private static byte[] CreateBaseBytes(int repeat)
        {
            return new byte[] { 1 }.Concat(Enumerable.Repeat<byte>(0, repeat)).ToArray();
        }
        
    }
}
