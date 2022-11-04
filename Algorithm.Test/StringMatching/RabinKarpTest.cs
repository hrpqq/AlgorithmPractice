using Algorithm.StringMatching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.StringMatching
{
    [TestClass]
    public class RabinKarpTest
    {
        [TestMethod]
        public void shouldFindOneMatchingAtIndexSeven()
        {
            string source = "abccbaddnbfq";
            string pattern = "dnb";
            var index = RabinKarp.MatchFirst(source, pattern);
            Assert.AreNotEqual(-1, index);
            Assert.AreEqual(7, index);
        }

        [TestMethod]
        public void shouldGenerateHashValue4Bytes()
        {
            byte[] inputBytes = new byte[] { 45, 22, 15, 99, 34, 0 };
            byte[] inputBytes2 = new byte[] { 0, 0, 45, 22, 15, 99, 34, 0 };
            var hash = RabinKarp.GetBytesHashCode(inputBytes);
            var hash2 = RabinKarp.GetBytesHashCode(inputBytes2);
            Assert.AreEqual(hash, hash2);


            inputBytes = new byte[] { 45, 22, 0, 0, 0, 1 };
            inputBytes2 = new byte[] { 0, 0, 45, 22, 0, 0, 0, 1 };
            hash = RabinKarp.GetBytesHashCode(inputBytes);
            hash2 = RabinKarp.GetBytesHashCode(inputBytes2);
            Assert.AreEqual(hash, hash2);
        }

        [TestMethod]
        public void shouldGenerateHashValue4String()
        {
            string inputBytes = "abcddbauf";
            string inputBytes2 = Encoding.UTF8.GetString(new byte[] { 0 }) + "abcddbauf";
            var hash = RabinKarp.GetStringHashCode(inputBytes);
            var hash2 = RabinKarp.GetStringHashCode(inputBytes2);
            Assert.AreEqual(hash, hash2);
        }

        [TestMethod]
        public void shouldGenerateRollingHash4Bytes()
        {
            byte[] prevBytes = new byte[] { 2, 45, 22, 0, 0, 0 };
            var hash1 = RabinKarp.GetBytesHashCode(prevBytes);

            byte[] curBytes = new byte[] { 45, 22, 0, 0, 0, 1 };
            var hashc = RabinKarp.GetBytesHashCode(curBytes);

            var hashRolling = RabinKarp.GetBytesRollingHashCode(new byte[] { 2, 45, 22, 0, 0, 0 }, 
                new byte[] { 2 }, 
                hash1.Value,
                new byte[] { 1 });

            Assert.AreEqual(hashc, hashRolling);
        }

    }
}
