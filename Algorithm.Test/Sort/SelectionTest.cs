﻿using Algorithm.Sort;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Algorithm.Sort.Utilities;

namespace Algorithm.Test.Sort
{
    [TestClass]
    public class SelectionTest
    {
        [TestMethod]
        public void ShouldHasBeenSorted()
        {
            var source = new List<IComparable>() { 1, 7, 2, 5 };
            var a = new List<IComparable>(source);
            var b = new List<IComparable>(source);
            Selection.Sort(a);
            b.Sort();
            Assert.AreEqual(true, ElementEqual(a, b));
        }
    }
}
