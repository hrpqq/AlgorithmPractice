using Algorithm.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Searching
{
    [TestClass]
    public class BinarySearchTreeTest
    {
        [TestMethod]
        public void should_add_node_2_tree_correctly()
        {
            var binTree = new BinarySearchTree<int>();
            binTree.Put(5);
            binTree.Put(6);
            binTree.Put(7);
            binTree.Put(1);
            binTree.Put(2);
            binTree.Put(3);
            binTree.Put(-1);
            Assert.AreEqual(7, binTree.Size);
        }

        [TestMethod]
        public void should_get_null_when_value_not_exist()
        {
            var binTree = new BinarySearchTree<int>();
            binTree.Put(5);
            binTree.Put(6);
            binTree.Put(7);
            binTree.Put(1);
            binTree.Put(2);
            binTree.Put(3);
            binTree.Put(-1);
            var value = binTree.Get(10);
            Assert.AreEqual(default(int), value);
        }

        [TestMethod]
        public void should_get_value_when_key_exit()
        {
            var binTree = new BinarySearchTree<int>();
            binTree.Put(5);
            binTree.Put(6);
            binTree.Put(7);
            binTree.Put(1);
            binTree.Put(2);
            binTree.Put(3);
            binTree.Put(-1);
            var value = binTree.Get(1);
            Assert.AreEqual(1, value);
        }

        [TestMethod]
        public void should_delete_value_when_key_exit()
        {
            var binTree = new BinarySearchTree<int>();
            binTree.Put(5);
            binTree.Put(6);
            binTree.Put(7);
            binTree.Put(1);
            binTree.Put(2);
            binTree.Put(3);
            binTree.Put(-1);
            var a1 = binTree.Print();
            binTree.Delete(6);
            var value = binTree.Get(6);
            a1 = binTree.Print();
            Assert.AreEqual(default(int), value);
            binTree.Delete(5);
            value = binTree.Get(5);
            a1 = binTree.Print();
            Assert.AreEqual(default(int), value);
        }
    }
}
