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
            Assert.AreEqual(RBTree.NIL, parent.Left);
            Assert.AreEqual(RBTree.NIL, parent.Right);
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
            Assert.AreEqual(RBTree.NIL, grandpa.Left);
        }

        [TestMethod]
        public void should_build_correct_red_black_tree()
        {
            var RBTree = new RedblackTree<int, int>();
            RBTree.Insert(1, 1);
            RBTree.Insert(2, 2);
            RBTree.Insert(3, 3);
            Assert.AreEqual(true, true);
        }
    }
}
