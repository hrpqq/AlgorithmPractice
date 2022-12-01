using System.Text;

namespace Algorithm.Searching
{
    public class BinarySearchTree<T> where T : IComparable
    {
        public int Size => _root.Left?.Size ?? 0;
        // the true root of the tree is _root.left
        private Node<T> _root = new Node<T>();
        public void Put(T target)
        {
            if (_root.Left == null)
            {
                _root.Left = new Node<T> { Value = target };
            }
            else
                Put(_root.Left, target);
        }

        private void Put(Node<T> curNode, T target)
        {
            var nextNode = GetNextNode(curNode, target, true);
            if (nextNode != null
                && nextNode.Value.CompareTo(target) != 0)
            {
                Put(nextNode, target);
            }
        }

        public T? Get(T key)
        {
            T? result = default(T?);
            if (_root.Left != null)
            {
                result = Get(_root.Left, key);
            }
            return result;
        }

        private T? Get(Node<T> curNode, T key)
        {
            T? returnVal = default(T);
            var nextNode = GetNextNode(curNode, key);
            if (nextNode == curNode)
            {
                returnVal = curNode.Value;
            }
            else if (nextNode != null)
            {
                returnVal = Get(nextNode, key);
            }
            return returnVal;
        }

        public void Delete(T key)
        {
            if (_root.Left != null)
                Delete(_root.Left, _root, key);
        }

        private bool Delete(Node<T> curNode, Node<T> curParent, T key)
        {
            var nextNode = GetNextNode(curNode, key);
            if (nextNode != curNode)
            {
                if (nextNode != null)
                {
                    var isDel = Delete(nextNode, curNode, key);
                    if (isDel) curNode.Size--;
                    return true;
                }
                else
                    return false;    
            }
            else
            {
                if (curNode.Right != null)
                {
                    var (min, minParent) = GetMin(curNode.Right, curNode);
                    if (min != null)
                    {
                        DeleteMin(curNode.Right, curNode);
                        min.Left = curNode.Left;
                        min.Right = curNode.Right;
                        min.Size = curNode.Size - 1;
                        if (curParent.Left == curNode) curParent.Left = min;
                        else curParent.Right = min;
                    }
                }
                else
                {
                    if (curParent.Left == curNode) curParent.Left = curNode.Left;
                    else curParent.Right = curNode.Left;
                }
                return true; 
            }
        }

        public (Node<T> min, Node<T> minParent) GetMin(Node<T> curNode, Node<T> curParent)
        {
            if (curNode.Left == null)
            {
                return(curNode, curParent);
            }
            else
            {
                return GetMin(curNode.Left, curNode);
            }
        }

        public void DeleteMin(Node<T> curNode, Node<T> parent)
        {
            var (min, minParent) = GetMin(curNode, parent);
            if (min != curNode)
            {
                if (min.Right == null)
                {
                    minParent.Left = null;
                }
                else
                {
                    minParent.Left = min.Right;
                }
            }
            else 
            {
                if (minParent.Left == min) minParent.Left = null;
                else minParent.Right = null;
            }
        }

        public string Print()
        {
            var sb = new StringBuilder();
            if (_root.Left != null)
            {
                Print(_root.Left, sb, "|");
            }
            return sb.ToString();
        }

        private void Print(Node<T> curNode, StringBuilder sb, string prefix)
        {
            if (curNode.Left != null) Print(curNode.Left, sb, prefix + "--");
            sb.Append(prefix).AppendLine($"value:{curNode.Value} - size:{curNode.Size}");
            if (curNode.Right != null) Print(curNode.Right, sb, prefix + "--");
        }

        //public int Rank(T key)
        //{
        //    return -1;
        //}

        //public T Select(int index)
        //{
        //    return default(T);
        //}

        //public IEnumerable<T> Keys(int startIndex, int length)
        //{
        //    IList<T> returnVal = new List<T>();

        //    return returnVal;
        //}

        private Node<T>? GetNextNode(Node<T> curNode, T target, bool shouldUpdate = false)
        {
            var cmpRes = curNode.Value.CompareTo(target);
            if (cmpRes == 0)
            {
                if (shouldUpdate)
                    curNode.Value = target;
                return curNode;
            }
            else if (cmpRes < 0)
            {
                if (shouldUpdate)
                {
                    curNode.Size++;
                    curNode.Right = curNode.Right ?? new Node<T> { Value = target };
                }
                return curNode.Right;
            }
            else
            {
                if (shouldUpdate)
                {
                    curNode.Size++;
                    curNode.Left = curNode.Left ?? new Node<T> { Value = target };
                }
                return curNode.Left;
            }
        }
    }

    public class Node<T> where T : IComparable
    {
        public int Size { get; set; } = 1;
        public T Value { get; set; }
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }
        

    }
}