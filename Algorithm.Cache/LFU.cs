using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Cache
{
    public class LFU<TKey, TVal>
    {
        public int Capacity { get; }

        public int Count { get; private set; }

        private readonly Dictionary<TKey, Node<KeyValuePair<TKey, TVal>>> KVMap
            = new Dictionary<TKey, Node<KeyValuePair<TKey, TVal>>>();

        private readonly Dictionary<TKey, int> KFMap = new Dictionary<TKey, int>(); 

        private readonly Dictionary<int, FrequencyGroup> FGMap 
            = new Dictionary<int, FrequencyGroup>();

        private int MinFrequency { get; set; } = 1;

        public LFU(int capacity) => Capacity = capacity;

        public void Put(TKey key, TVal val)
        {
            var newPayload = new KeyValuePair<TKey, TVal>(key, val);
            if (KVMap.ContainsKey(key))
            {
                var node = KVMap[key];
                node.Payload = newPayload;
                var freq = KFMap[key];
                FGMap[freq].List.PickNode(node);
                RemoveGroupIfEmpty(freq);
                if (FGMap.TryGetValue(freq + 1, out var group))
                {
                    group.List.Add2Head(node);
                }
                else
                {
                    var newG = new FrequencyGroup();
                    newG.List.Add2Head(node);
                    FGMap[freq + 1] = newG;
                }
                KFMap[key] = freq + 1;
                if (freq == MinFrequency) MinFrequency++;
            }
            else
            {
                var newNode = new Node<KeyValuePair<TKey, TVal>>(newPayload);
                if (Count == Capacity)
                {
                    RemoveLast();
                }
                if (FGMap.TryGetValue(1, out var group))
                {
                    group.List.Add2Head(newNode);
                }
                else
                {
                    FGMap[1] = new FrequencyGroup();
                    FGMap[1].List.Add2Head(newNode);
                }
                MinFrequency = 1;
                KVMap[key] = newNode;
                KFMap[key] = 1;
                Count++;
            }
        }

        public bool TryGetValueByKey(TKey key, out TVal val)
        {
            val = default(TVal);
            if (KVMap.TryGetValue(key, out var node))
            {
                val = node.Payload.Value;
                var freq = KFMap[key];
                var oldGroup = FGMap[freq];
                oldGroup.List.PickNode(node);
                RemoveGroupIfEmpty(freq);
                KFMap[key] = freq + 1;
                if (FGMap.TryGetValue(freq + 1, out var group))
                {
                    group.List.Add2Head(node);
                }
                else
                {
                    FGMap.Add(freq + 1, new FrequencyGroup());
                    FGMap[freq + 1].List.Add2Head(node);
                }
                return true;
            }
            return false;
        }

        private void RemoveLast()
        {
            var minG = FGMap[MinFrequency];
            Count--;
            minG.List.RemoveLast();
            if (minG.List.Count == 0)
            {
                FGMap.Remove(MinFrequency);
            }
        }

        private void RemoveGroupIfEmpty(int freq)
        {
            if (FGMap.TryGetValue(freq, out var g)
                && g.List.Count == 0)
            {
                FGMap.Remove(freq);
            }
        }

        private class FrequencyGroup
        { 
            public DoubleLinkedList<KeyValuePair<TKey, TVal>> List { get; } 
                = new DoubleLinkedList<KeyValuePair<TKey, TVal>>();

        }
    }


}
