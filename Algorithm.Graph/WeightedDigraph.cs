using System.Collections;
using System.Linq;

namespace Algorithm.Graph
{
    public class WeightedDigraph
    {
        private readonly int _vectorCount;
        public int VectorCount => _vectorCount;

        public int EdgeCount { get; private set; } = 0;

        public List<HashSet<DirectedEdge>> VEListArr { get; private set; }

        public WeightedDigraph(int VCount)
        {
            _vectorCount= VCount;
            VEListArr = Enumerable.Repeat<HashSet<DirectedEdge>>(null, _vectorCount).ToList();
        }

        public bool AddEdge(DirectedEdge e)
        {
            if (e.From < _vectorCount && e.To < _vectorCount)
            {
                VEListArr[e.From] = VEListArr[e.From] ?? new HashSet<DirectedEdge>();
                VEListArr[e.From].Add(e);
                EdgeCount++;
                return true;
            }
            return false;
        }

        public IEnumerable<DirectedEdge> AdjcentEdges(int v)
        {
            return VEListArr.ElementAtOrDefault(v) ?? new HashSet<DirectedEdge>();
        }

        public WeightedDigraph Reverse()
        {
            var returnVal = new WeightedDigraph(VectorCount);
            foreach(var edge in VEListArr.SelectMany(l => l ?? Enumerable.Empty<DirectedEdge>()))
            {
                returnVal.AddEdge(new DirectedEdge(edge.To, edge.From, edge.Weight));
            }
            return returnVal;
        }

        public bool HasLoop()
        {
            var callStackVariables = new Stack<(IEnumerator<DirectedEdge> vectorAdjEdges, int vectorIdx)>();
            var vectorsInPath= new HashSet<int>();
            var hasVisited = new bool[_vectorCount];
            for (int i = 0; i < _vectorCount; i++)
            {
                if (hasVisited[i]) continue;
                int vIdx = i;
                bool goDeeper = true;
                IEnumerator<DirectedEdge> curEnumerator = getEnumerator(vIdx);
                while (true)
                {
                    if (goDeeper)
                    {
                        hasVisited[vIdx] = true;
                        curEnumerator = getEnumerator(vIdx);
                        callStackVariables.Push((curEnumerator, vIdx));
                    }
                    if (curEnumerator.MoveNext())
                    {
                        var edge = curEnumerator.Current;
                        if (!vectorsInPath.Contains(edge.To))
                        {
                            vIdx = edge.To;
                            vectorsInPath.Add(vIdx);
                            goDeeper = true;
                        }
                        else
                            return true;
                    }
                    else if (callStackVariables.Count > 0)
                    {
                        goDeeper = false;
                        (curEnumerator, vIdx) = callStackVariables.Pop();
                        vectorsInPath.Remove(vIdx);
                    }
                    else
                        break; 
                }
            }
            return false;
        }

        private IEnumerator<DirectedEdge> getEnumerator(int vIdx) 
            => VEListArr[vIdx]
                ?.GetEnumerator()
                ?? Enumerable.Empty<DirectedEdge>().GetEnumerator();

    }
}