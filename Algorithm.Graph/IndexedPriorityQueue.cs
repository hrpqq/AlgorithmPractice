using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Graph
{
    public class IndexedPriorityQueue
    {
        private int _capacity;
        public int Count => _innerArr.Where(q => q != double.NegativeInfinity).Count();

        public double[] _innerArr;

        public Dictionary<double, Queue<int>> _PriDic;
        public IndexedPriorityQueue(int capacity) 
        {
            _capacity = capacity;
            _innerArr = Enumerable.Repeat(double.NegativeInfinity, capacity).ToArray();
            _PriDic = new Dictionary<double, Queue<int>>();
        }

        public bool Set(int idx, double p)
        {
            if (idx < _capacity)
            {
                _innerArr[idx] = p;
                Queue<int> queue;
                if(_PriDic.ContainsKey(p))
                    queue= _PriDic[p];
                else
                    queue = _PriDic[p] = new Queue<int>();
                queue.Enqueue(idx);
                return true;
            }
            return false;
        }

        public (int idx, double priority) DeleteMin()
        {
            double minPri = double.PositiveInfinity;
            for (int i = 0; i < _capacity; i++)
            {
                if(_innerArr[i] == double.NegativeInfinity) 
                    continue; 
                else if (_innerArr[i] < minPri)
                {
                    minPri = _innerArr[i];
                }
            }
            if (minPri != double.PositiveInfinity)
            {
                var minIdx = _PriDic[minPri].Dequeue();
                if (_PriDic[minPri].Count == 0)
                    _PriDic.Remove(minPri);

                _innerArr[minIdx] = double.NegativeInfinity;
                return (minIdx, minPri);
            }  
            return default((int, double));
        }
    }
}
