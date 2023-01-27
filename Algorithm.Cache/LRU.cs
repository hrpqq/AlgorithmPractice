using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Cache
{
    public class LRU<TKey, TVal>
    {
        private Dictionary<TKey, Node<KeyValuePair<TKey, TVal>>> KVMap { get; } 
            = new Dictionary<TKey, Node<KeyValuePair<TKey, TVal>>> ();

        public DoubleLinkedList<KeyValuePair<TKey, TVal>> List { get; }

        public int Capacity { get; }

        public int Count => List.Count;

        public LRU(int capacity)
        {
            Capacity = capacity;
            List = new DoubleLinkedList<KeyValuePair<TKey, TVal>>();
        }

        public void Put(TKey key, TVal val)
        {
            var newPayload = new KeyValuePair<TKey, TVal>(key, val);
            if (KVMap.ContainsKey(key))
            {
                var node = KVMap[key];
                node.Payload = newPayload;
                List.PickNode(node);
                List.Add2Head(node);
            }
            else
            {
                var newNode = new Node<KeyValuePair<TKey, TVal>>(newPayload);
                if (List.Count == Capacity)
                {
                   List.RemoveLast();
                }
                KVMap[key] = newNode;
                List.Add2Head(newNode);
            }

        }

        public bool TryGetVal(TKey key, out TVal val)
        {
            val = default(TVal);
            if (KVMap.ContainsKey(key))
            {
                var node = KVMap[key];
                val = node.Payload.Value;
                List.PickNode(node);
                List.Add2Head(node);
                return true;
            }
            return false;
        }
    }
}
