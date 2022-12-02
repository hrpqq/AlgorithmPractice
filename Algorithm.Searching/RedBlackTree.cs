using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Searching
{
    public class RedblackTree<TKey,TValue> where TKey : IComparable
    {
        public Node NIL { get; private set; } = new Node();

        public Node? Root { get; private set; }

        public void Insert(TKey key, TValue value)
        {
            var newNode = GetNewNode(key, value);
            if (Root == null)
            {
                Root = newNode;
                newNode.IsRed = false;
            }
            else
            {
                var curNode = Root;
                while (newNode.P != curNode)
                {
                    var cmpRes = curNode.Key.CompareTo(key);
                    if (cmpRes == 0)
                    {
                        curNode.Value = value;
                        break;
                    }
                    else if (cmpRes > 0)
                    {
                        if (curNode.Left != NIL) curNode = curNode.Left;
                        else
                        {
                            curNode.Left = newNode;
                            newNode.P = curNode;
                        }
                    }
                    else
                    {
                        if (curNode.Right != NIL) curNode = curNode.Right;
                        else
                        {
                            curNode.Right = newNode;
                            newNode.P = curNode;
                        }
                    }
                }
                FixUpBalance(newNode);
            }
        }

        private void FixUpBalance(Node targetNode)
        {
            while (targetNode.P.IsRed)
            {
                var parent = targetNode.P;
                var grandpa = parent.P;
                var isTargetOnLeftSide = parent.Left == targetNode;
                var isParentOnLeftSide = parent.P.Left == parent;
                var parentSibling = isParentOnLeftSide
                    ? parent.P.Right
                    : parent.P.Left;
                if (parent.IsRed && parentSibling.IsRed)
                {
                    parent.P.IsRed = true;
                    parent.IsRed = false;
                    parentSibling.IsRed = false;
                    targetNode = parent.P;
                }
                else
                {
                    if (isTargetOnLeftSide ^ isParentOnLeftSide)
                    {
                        Rotate(targetNode, parent);
                        targetNode = parent;
                    }
                    else
                    {
                        Rotate(parent, grandpa);
                        parent.IsRed = false;
                        grandpa.IsRed = true;
                    }
                } 
            }
        }

        public void Rotate(Node child, Node parent)
        {
            if (child != NIL
                && parent != NIL)
            {
                var grandpa = parent.P;
                bool isParentOnLeftSide = grandpa.Left == parent;
                bool isChildOnLeftSide = parent.Left == child;
                if (isChildOnLeftSide)
                {
                    var cr = child.Right;
                    child.Right = parent;
                    parent.Left = cr;
                    cr.P = parent;
                }
                else
                {
                    var cl = child.Left;
                    child.Left = parent;
                    parent.Right = cl;
                    cl.P = parent;
                }

                if (grandpa != NIL)
                {
                    if (isParentOnLeftSide) grandpa.Left = child;
                    else grandpa.Right = child;
                    child.P = grandpa;
                }
                else
                {
                    Root = child;
                    child.P = NIL;
                }
                parent.P = child;
            }
            else
                throw new InvalidOperationException("NIL node can't be rotated!");
        }

        public Node GetNewNode(TKey key, TValue value)
        {
            return new Node
            {
                Key = key,
                Value = value,
                Left = NIL,
                Right = NIL,
                P = NIL,
                IsRed = true
            };
        }

        public class Node
        {
            public Node P { get; set; }
            public TKey Key { get; set; }
            public TValue Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool IsRed { get; set; } 
        }
    }

    
}
