using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Sort
{
    public class Heap<T> where T : IComparable
    {
        public static IEnumerable<T> Sort(IEnumerable<T> source)
        {
            IList<T> res = new List<T>();
            var heap = new PriorityQueue();
            foreach (var item in source)
            {
                heap.Add(item);
            }
            while (heap.Count > 0)
            {
                res.Add(heap.PopMin());
            }
            res = res.Reverse().ToArray();
            return res;
        }

        public class PriorityQueue
        {
            private int _endIndex = 0;
            private T[] _array;
            public int Count { get { return _endIndex; } }

            public PriorityQueue()
            {
                _array = new T[16];
            }

            public T? GetMin()
            {
                return Count > 0
                        ? _array[1]
                        : default(T);
            }

            public T? PopMin()
            {
                var res = GetMin();
                DeleteMin();
                return res;
            }

            public void DeleteMin()
            {
                if (Count == 1)
                    _endIndex = 0;
                else
                {
                    _array[1] = _array[_endIndex];
                    Sink(1);
                    _endIndex--;
                    TryResize();
                }
            }

            public void Add(T item)
            {
                _array[++_endIndex] = item;
                Swim(_endIndex);
                TryResize();
            }

            private void Swim(int index)
            {
                if (Count > 1)
                {
                    while (index != 1)
                    {
                        var pIndex = index / 2;
                        var cmpRes = _array[index].CompareTo(_array[pIndex]);
                        if (cmpRes > 0)
                        {
                            Swap(index, pIndex);
                            index = pIndex;
                        }
                        else
                            break;
                    }
                }
            }

            private void Sink(int index)
            {
                while (index * 2 <= _endIndex)
                {
                    var li = index * 2;
                    var ri = li + 1;
                    var subBigger = li;
                    if (_endIndex >= ri)
                    {
                        var cmpRes1 = _array[li].CompareTo(_array[ri]);
                        if (cmpRes1 < 0)
                        {
                            subBigger = ri;
                        }
                    }
                    var cmpRes2 = _array[index].CompareTo(_array[subBigger]);
                    if (cmpRes2 < 0)
                    {
                        Swap(index, subBigger);
                        index = subBigger;
                    }
                    else
                        break;
                }
            }

            private void Swap(int left, int right)
            {
                var temp = _array[left];
                _array[left] = _array[right];
                _array[right] = temp;
            }

            private void TryResize()
            {
                if (_endIndex + 1 > _array.Length / 2)
                {
                    var temp = new T[2 * _array.Length];
                    _array.CopyTo(temp, 0);
                    _array = temp;
                }
                else if (_array.Length >= 32
                        && _endIndex + 1 <= _array.Length / 4)
                {
                    var temp = new T[_array.Length / 2];
                    Array.Copy(_array, 0, temp, 0, temp.Length);
                    _array = temp;
                }
            }


        }
    }
}



