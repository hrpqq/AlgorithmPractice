using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Searching
{
    public class RedblackTree<TKey,TValue> where TKey : IComparable
    {
        public static Node NIL { get; } = new Node();

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
                FixUpInsertBalance(newNode);
            }
        }

        private void FixUpInsertBalance(Node targetNode)
        {
            while (targetNode.P.IsRed)
            {
                var parent = targetNode.P;
                var grandpa = parent.P;
                var isTargetOnLeftSide = parent.Left == targetNode;
                var isParentOnLeftSide = grandpa.Left == parent;
                var parentSibling = isParentOnLeftSide
                    ? grandpa.Right
                    : grandpa.Left;
                if (parent.IsRed && parentSibling.IsRed)
                {
                    grandpa.IsRed = true;
                    parent.IsRed = false;
                    parentSibling.IsRed = false;
                    targetNode = grandpa;
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
                Root.IsRed = false;
            }
            
        }

        public void Delete(TKey key)
        {
            var targetNode = GetNodeByKey(key);
            Node balanceTarget;
            if (targetNode != null)
            {
                var isTargetNodeRed = targetNode.IsRed;
                if (targetNode.Left == NIL)
                {
                    balanceTarget = targetNode.Right;
                    Transplant(targetNode, targetNode.Right);
                }
                else if (targetNode.Right == NIL)
                {
                    balanceTarget = targetNode.Left;
                    Transplant(targetNode, targetNode.Left);
                }
                else 
                {
                    var alternate = GetMinNode(targetNode.Right);
                    isTargetNodeRed = alternate.IsRed;
                    balanceTarget = alternate.Right;

                    if (targetNode.Right != alternate)
                    {
                        Transplant(alternate, alternate.Right);
                        alternate.Right = targetNode.Right;
                        alternate.Right.P = alternate;
                    }
                    
                    Transplant(targetNode, alternate);
                    alternate.Left = targetNode.Left;
                    alternate.Left.P = alternate;
                    alternate.IsRed = targetNode.IsRed;
                }
                var tree = Print();
                if (!isTargetNodeRed)
                {
                    FixUpDeleteBalance(balanceTarget);
                }
            }
        }

        //|--------value:1 color:black
        //|------------value:2 color:red
        //|----value:3 color:red
        //|--------value:4 color:black
        //|------------value:5 color:red
        //|value:6 color:black
        //|--------value:7 color:black
        //|----value:8 color:red
        //|--------value:9 color:black
        //|------------value:10 color:red
        private void FixUpDeleteBalance(Node targetNode)
        {
            var curNode = targetNode;
            while (curNode != Root
                && !curNode.IsRed)
            {
                var parent = curNode.P;
                Node curSibling;
                if (parent.Left == curNode) curSibling = parent.Right;
                else curSibling = parent.Left;

                var isCurSilbingOnLeft = curSibling.P.Left == curSibling;
                var isCurSiblingLeftRed = curSibling.Left.IsRed;
                // case 1
                if (curSibling.IsRed) 
                {
                    curSibling.IsRed = false;
                    parent.IsRed = true;
                    Rotate(curSibling, parent);
                }
                else
                {
                    // case 2
                    if (!curSibling.IsRed
                        && !curSibling.Left.IsRed
                        && !curSibling.Right.IsRed)
                    {
                        curSibling.IsRed = true;
                        curNode = parent;
                    }
                    //case 3
                    else if (isCurSilbingOnLeft ^ isCurSiblingLeftRed) 
                    {
                        curSibling.IsRed = true;
                        Node sibingChild;
                        if (isCurSilbingOnLeft)
                        {
                            curSibling.Right.IsRed = false;
                            sibingChild = curSibling.Right;
                        } 
                        else 
                        {
                            curSibling.Left.IsRed = false;
                            sibingChild = curSibling.Left;
                        }
                        Rotate(sibingChild, curSibling);
                        curSibling = sibingChild;
                    }
                    // case 4
                    curSibling.IsRed = parent.IsRed;
                    parent.IsRed = false;
                    if (isCurSilbingOnLeft) curSibling.Left.IsRed = false;
                    else curSibling.Right.IsRed = false;

                    Rotate(curSibling, parent);
                    curNode = Root;
                }
            }
            curNode.IsRed = false;
        }

        private void Transplant(Node ancestor, Node descendant)
        {
            if (ancestor == Root)
            {
                Root = descendant;
            }
            else if (ancestor.P.Left == ancestor)
            {
                ancestor.P.Left = descendant;
            }
            else
                ancestor.P.Right = descendant;
            descendant.P = ancestor.P;
        }

        public bool TryGetValueByKey(TKey key, out TValue value)
        {
            value = default(TValue);
            var resNode = GetNodeByKey(key);
            if (resNode != null)
            {
                value = resNode.Value;
                return true;
            }
            return false;
        }

        private Node? GetNodeByKey(TKey key)
        {
            var curNode = Root;
            while (curNode != null)
            {
                var cmpRes = curNode.Key.CompareTo(key);
                if (cmpRes == 0)
                {
                    return curNode;
                }
                else if (cmpRes > 0)
                {
                    curNode = curNode.Left;
                }
                else 
                    curNode = curNode.Right;
            }
            return null;
        }

        private Node GetMinNode(Node curNode)
        {
            if (curNode.Left != NIL)
            {
                return GetMinNode(curNode.Left);
            }
            else
                return curNode;
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

        public string Print()
        {
            var sb = new StringBuilder();
            if (Root != null)
            {
                Print(Root, sb, "|");
            }
            return sb.ToString();
        }

        private void Print(Node curNode, StringBuilder sb, string prefix)
        {
            if (curNode.Left != NIL) Print(curNode.Left, sb, prefix + "----");
            var color = curNode.IsRed ? "red" : "black";
            sb.Append(prefix).AppendLine($"value:{curNode.Value} color:{color}");
            if (curNode.Right != NIL) Print(curNode.Right, sb, prefix + "----");
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
