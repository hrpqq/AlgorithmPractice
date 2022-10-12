using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm.Sort;

namespace Algorithm.Test.Sort
{
    [TestClass]
    public class UtilitiesTest
    {
        private IList<IComparable> TestData = new List<IComparable> { 1, 4, 7, 123, 22, -10 };

        [TestMethod]
        public void A1ShouldLessThanB2()
        {
            int a = 1;
            int b = 2;
            var isLess = Utilities.Less(a, b);
            Assert.AreEqual(true, isLess);
        }

        [TestMethod]
        public void A2ShouldNotLessThanB1()
        {
            int a = 2;
            int b = 1;
            var isLess = Utilities.Less(a, b);
            Assert.AreEqual(false, isLess);
        }

        [TestMethod]
        public void A1ShouldNotLessThanB1()
        {
            int a = 1;
            int b = 1;
            var isLess = Utilities.Less(a, b);
            Assert.AreEqual(false, isLess);
        }


        [TestMethod]
        public void ShouldSwapItem0AndItem4()
        {
            var local = new List<IComparable>(TestData);
            Utilities.Swap(local, 0, 4);
            Assert.AreEqual(22, local[0]);
            Assert.AreEqual(1, local[4]);
        }

        [TestMethod]
        public void ShouldSwapItem2AndItem3()
        {
            var local = new List<IComparable>(TestData);
            Utilities.Swap(local, 2, 3);
            Assert.AreEqual(123, local[2]);
            Assert.AreEqual(7, local[3]);
        }

        [TestMethod]
        public void ShouldReturnTrueWhileListWasSorted()
        {
            var local = new List<IComparable>() { -10, 1, 5, 9, 11};
            var res = Utilities.IsSort(local);
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ShouldFalseTrueWhileListWasSorted()
        {
            var local = new List<IComparable>() { 0, -10, 1, 5, 9, 11 };
            var res = Utilities.IsSort(local);
            Assert.AreEqual(false, res);
        }

        [TestMethod]
        public void ShouldReturnTrueIfElementsAreEqual()
        {
            var local = new List<IComparable>() { 0, -10, 1, 5, 9, 11 };
            var target = new List<IComparable>(local);
            var res = Utilities.ElementEqual(local, target);
            Assert.AreEqual(true, res);
        }

        [TestMethod]
        public void ShouldReturnFalseIfElementsAreEqual()
        {
            var local = new List<IComparable>() { 0, -10, 1, 5, 9, 11 };
            var target = new List<IComparable>(local);
            local.Sort();
            var res = Utilities.ElementEqual(local, target);
            Assert.AreEqual(false, res);
        }
    }
}
