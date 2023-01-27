using System.Collections;

namespace Algorithm.Cache
{
    public class DoubleLinkedList<T>: IEnumerable<T>
    {
        private Node<T> Head { get; }

        private Node<T> Tail { get; }

        public int Count { get; private set; } = 0;

        public DoubleLinkedList()
        {
            Head = new Node<T>();
            Tail = new Node<T>();
            Head.Next = Tail;
            Tail.Previous = Head;
        }

        public Node<T> AddValue(T val)
        {
            var newNode = new Node<T>(val);
            Add2Head(newNode);
            return newNode;
        }

        public void Add2Head(Node<T> newNode)
        {
            var handNext = Head.Next;
            Head.Next = newNode;
            newNode.Previous = Head;
            handNext.Previous = newNode;
            Count++;
        }

        public bool RemoveLast()
        {
            if (Count > 0)
            {
                return PickNode(Tail.Previous);
            }
            return false;
        }

        public bool PickNode(Node<T> node)
        {
            if (Count > 0 && node != Head && node != Tail)
            { 
                var targetPre = node.Previous;
                var targetNext = node.Next;
                targetPre.Next = targetNext;
                targetNext.Previous = targetPre;
                node.Next = null;
                node.Previous = null;
                Count--;
                return true;
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (Count > 0)
            {
                var nextNode = Head.Next;
                while (nextNode != Tail)
                    yield return nextNode.Payload;
                
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
        
    }


}