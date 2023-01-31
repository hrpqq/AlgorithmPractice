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
            var returnVal = new WeightedDigraph(EdgeCount);
            foreach(var edge in VEListArr.SelectMany(l => l))
            {
                returnVal.AddEdge(edge);
            }
            return returnVal;
        }

        public bool HasLoop()
        {
            var callStackVariables = new Stack<(IEnumerator<DirectedEdge> vectorAdjEdge, int vectorIdx)>();
            var VectorsInPath= new HashSet<int>();
            var hasVisited = new bool[_vectorCount];
            for (int i = 0; i < _vectorCount; i++)
            {
                if (hasVisited[i]) continue;
                IEnumerator<DirectedEdge> getEnumerator(int idx) => VEListArr[idx]
                                                            ?.GetEnumerator()
                                                            ?? Enumerable.Empty<DirectedEdge>().GetEnumerator();
                int vectorTo = i;
                bool goDeeper = true;
                IEnumerator<DirectedEdge> curEnumerator = getEnumerator(vectorTo);
                while (true)
                {
                    if (goDeeper)
                    {
                        hasVisited[vectorTo] = true;
                        if (VectorsInPath.Contains(vectorTo))
                            return true;
                        else
                            VectorsInPath.Add(vectorTo);
                        curEnumerator = getEnumerator(vectorTo);
                        callStackVariables.Push((curEnumerator, vectorTo));
                    }

                    if (curEnumerator.MoveNext())
                    {
                        var edge = curEnumerator.Current;
                        //hasVisited[edge.From] = true;
                        if (!hasVisited[edge.To])
                        {
                            vectorTo = edge.To;
                            goDeeper = true;
                        }
                    }
                    else if (callStackVariables.Count > 0)
                    {
                        goDeeper = false;
                        (curEnumerator, vectorTo) = callStackVariables.Pop();
                        VectorsInPath.Remove(vectorTo);
                    }
                    else
                        break; 
                }
            }
            return false;
        }

        

    }
}