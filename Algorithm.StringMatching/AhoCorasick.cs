using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.StringMatching
{
    public class AhoCorasick
    {
        public static List<(char[] target, int index)> MatchTargets(char[] source, IList<char[]> targets)
        {
            var root = BuildTire(targets);
            ProcessMatching(source, root, out var result);
            return result;
        }

        public static void ProcessMatching(char[] source, ACNode root, out List<(char[] target, int index)> res)
        {
            res = new List<(char[] target, int index)>();
            for (int i = 0; i < source.Length; )
            {
                var curChar = source[i];
                var curNode = root;
                bool find = false;
                int targetIndex = -1;
                char[] targetWord = new char[0];
                while (true)
                {
                    if (curNode.Children.ContainsKey(curChar))
                    {
                        curNode = curNode.Children[curChar];
                        curChar = source[i++];
                        if (curNode.IsEnding)
                        {
                            find = true;
                            targetIndex = i - curNode.Length;
                            targetWord = GetCharsFromEnding(curNode);
                        }
                    }
                    else if (curNode.Fallback != null)
                    {
                        curNode = curNode.Fallback;
                    }
                    else
                        break;
                }
                if (find)
                {
                    res.Add((targetWord, targetIndex));
                }
            }
        }

        public static ACNode BuildTire(IList<char[]> targets)
        {
            var root = new ACNode(default(char))
            { 
                Fallback = null,
                Parent = null,
                Length = 0
            };
            // init tire tree
            foreach (var target in targets)
            {
                var curNode = root;
                foreach (var c in target)
                {
                    if (curNode.Children.ContainsKey(c))
                    {
                        curNode = curNode.Children[c];
                    }
                    else
                    {
                        var newNode = new ACNode(c) 
                        {
                            Parent = curNode,
                            Length = curNode.Length + 1 
                        };
                        curNode.Children[c] = newNode;
                        curNode = newNode;
                    }
                }
            }
            // setup fallback
            // base on breath first search on Tire
            var queue = new Queue<ACNode>();
            queue.Enqueue(root);
            while (queue.TryDequeue(out var parent))
            {
                if (parent == root)
                    parent.Children.Values.ToList().ForEach(q => 
                    {
                        q.Fallback = root;
                        queue.Enqueue(q);
                    });
                else
                {
                    foreach (var node in parent.Children.Values)
                    {
                        var backup = parent.Fallback;
                        while (backup != null)
                        {
                            if (backup.Children.ContainsKey(node.Charactor))
                            {
                                backup = backup.Children[node.Charactor];
                                break;
                            }
                            backup = backup.Fallback;
                        }
                        node.Fallback = backup;
                        queue.Enqueue(node);
                    }
                }
            }
            return root;
        }

        private static char[] GetCharsFromEnding(ACNode ending)
        {
            List<char> resList = new List<char>();
            while (ending.Parent != null)
            {
                resList.Add(ending.Charactor);
                ending = ending.Parent;
            }
            resList.Reverse();
            return resList.ToArray();
        }

        public class ACNode
        {
            public ACNode Parent { get; set; }
            public char Charactor { get; set; }
            public IDictionary<char,ACNode> Children { get; set; }
            public ACNode Fallback { get; set; }
            public bool IsEnding => Children?.Count <= 0;
            public int Length { get; set; }
            public ACNode(char charactor)
            {
                Charactor = charactor;
                Children = new Dictionary<char,ACNode>();
                Fallback = null;
            }
        }
    }
}
