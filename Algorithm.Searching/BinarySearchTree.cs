namespace Algorithm.Searching
{
    public class BinarySearchTree<T> where T : IComparable
    {
        private Node<T>? _root = null;
        public void Put(T target)
        {
            if (_root == null)
            {
                _root = new Node<T>() { Object = target };
            }
            Put(_root, target);
        }

        private void Put(Node<T> curNode, T target)
        {
            var nextNode = GetNextNode(curNode, target, true);
            if (nextNode != null
                && nextNode.Object.CompareTo(target) != 0)
            {
                Put(nextNode, target);
            }
        }

        public T? Get(T key)
        {
            T? result = default(T?);
            if (_root != null)
            {
                result = Get(_root, key);
            }
            return result;
        }

        private T? Get(Node<T> curNode, T key)
        {
            T? returnVal = default(T);
            var nextNode = GetNextNode(curNode, key);
            if (nextNode == curNode)
            {
                returnVal = curNode.Object;
            }
            else if (nextNode != null)
            {
                returnVal = Get(nextNode, key);
            }
            return returnVal;
        }

        public void Delete(T key)
        {
            if (_root != null)
                Delete(ref _root, key);
        }

        private void Delete(ref Node<T> curNode, T key)
        {
            var nextNode = GetNextNode(curNode, key);
            if (nextNode != curNode)
            {
                if (nextNode != null)
                    Delete(ref nextNode, key);
            }
            else
            {
                if (curNode.Right != null)
                {
                    var min = GetMin(curNode.Right, out _);
                    if (min != null)
                    {
                        DeleteMin(curNode);
                        min.Left = curNode.Left;
                        min.Right = curNode.Right;
                        curNode = min;
                    }
                }
                else
                    curNode = curNode.Left;
            }
        }

        public Node<T> GetMin(Node<T> curNode, out Node<T> parentOfMin)
        {
            Node<T> result = null;
            parentOfMin = null;
            if (curNode.Left == null)
            {
                result = curNode;
            }
            else
            {
                result = GetMin(curNode.Left, out parentOfMin);
                if(parentOfMin == null)
                    parentOfMin = curNode;
            }
            return result;
        }

        public void DeleteMin(Node<T> curNode)
        {
            var minNode = GetMin(curNode, out var parentOfMin);
            if (minNode != curNode)
            {
                if (minNode.Right == null)
                {
                    parentOfMin.Left = null;
                }
                else
                {
                    parentOfMin.Left = minNode.Right;
                }
            }
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
            var cmpRes = curNode.Object.CompareTo(target);
            if (cmpRes == 0)
            {
                if (shouldUpdate)
                    curNode.Object = target;
                return curNode;
            }
            else if (cmpRes > 0)
            {
                if (shouldUpdate)
                {
                    curNode.Size++;
                    curNode.Right = curNode.Right ?? new Node<T> { Object = target };
                }
                return curNode.Right;
            }
            else
            {
                if (shouldUpdate)
                {
                    curNode.Size++;
                    curNode.Left = curNode.Left ?? new Node<T> { Object = target };
                }
                return curNode.Left;
            }
        }
    }

    public class Node<T> where T : IComparable
    {
        public int Size { get; set; } = 1;
        public Node<T>? Left { get; set; }
        public Node<T>? Right { get; set; }
        public T Object { get; set; }

    }
}