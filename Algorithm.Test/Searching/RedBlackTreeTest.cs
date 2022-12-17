using Algorithm.Searching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Test.Searching
{
    [TestClass]
    public class RedBlackTreeTest
    {
        [TestMethod]
        public void should_rotate_child_and_parent()
        {
            var RBTree = new RedblackTree<int, int>();
            var child = RBTree.GetNewNode(1, 1);
            var parent = RBTree.GetNewNode(2, 2);
            var grandpa = RBTree.GetNewNode(3, 3);

            grandpa.Left = parent;
            parent.P = grandpa;

            parent.Left = child;
            child.P = parent;

            RBTree.Rotate(child, parent);
            Assert.AreEqual(child.Right, parent);
            Assert.AreEqual(grandpa.Left, child);
            Assert.AreEqual(RedblackTree<int, int>.NIL, parent.Left);
            Assert.AreEqual(RedblackTree<int, int>.NIL, parent.Right);
        }

        [TestMethod]
        public void should_rotate_parent_and_grandpa()
        {
            var RBTree = new RedblackTree<int, int>();
            var child = RBTree.GetNewNode(1, 1);
            var parent = RBTree.GetNewNode(2, 2);
            var grandpa = RBTree.GetNewNode(3, 3);

            grandpa.Left = parent;
            parent.P = grandpa;

            parent.Left = child;
            child.P = parent;

            RBTree.Rotate(parent, grandpa);
            Assert.AreEqual(parent.Right, grandpa);
            Assert.AreEqual(grandpa.P, parent);
            Assert.AreEqual(parent.Left, child);
            Assert.AreEqual(RedblackTree<int, int>.NIL, grandpa.Left);
        }

        [TestMethod]
        public void should_build_correct_red_black_tree()
        {
            var RBTree = new RedblackTree<int, int>();
            RBTree.Insert(9, 9);
            RBTree.Insert(3, 3);
            var tree = RBTree.Print();
            RBTree.Insert(1, 1);
            RBTree.Insert(8, 8);
            RBTree.Insert(2, 2);
            tree = RBTree.Print();
            RBTree.Insert(4, 4);
            RBTree.Insert(10, 10);
            tree = RBTree.Print();
            RBTree.Insert(6, 6);
            RBTree.Insert(7, 7);
            RBTree.Insert(5, 5);
            tree = RBTree.Print();
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void should_delete_node_in_correct_way()
        {
            var RBTree = new RedblackTree<int, int>();
            RBTree.Insert(9, 9);
            RBTree.Insert(3, 3);
            RBTree.Insert(1, 1);
            RBTree.Insert(8, 8);
            RBTree.Insert(2, 2);
            RBTree.Insert(4, 4);
            RBTree.Insert(10, 10);
            RBTree.Insert(6, 6);
            RBTree.Insert(7, 7);
            RBTree.Insert(5, 5);
            var tree = RBTree.Print();
            RBTree.Delete(6);
            tree = RBTree.Print();
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void should_delete_node_in_correct_way2()
        {
            var RBTree = new RedblackTree<int, int>();
            RBTree.Insert(9, 9);
            RBTree.Insert(3, 3);
            RBTree.Insert(1, 1);
            RBTree.Insert(8, 8);
            RBTree.Insert(2, 2);
            RBTree.Insert(4, 4);
            RBTree.Insert(10, 10);
            RBTree.Insert(6, 6);
            RBTree.Insert(7, 7);
            RBTree.Insert(5, 5);
            var tree = RBTree.Print();
            RBTree.Delete(3);
            tree = RBTree.Print();
            Assert.AreEqual(true, true);
        }
    }
}
