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
                var curNode = root;
                bool find = false;
                int targetIndex = -1;
                char[] targetWord = new char[0];
                
                for (int j = 0; true;)
                {
                    if (curNode != root
                        && curNode.Charactor == source[i + j])
                    {
                        j++;
                        if (curNode.IsEnding)
                        {
                            find = true;
                            targetIndex = i + (j - curNode.Length);
                            targetWord = source.Take(new Range(i + j - curNode.Length, i + j)).ToArray();
                            i = i + j;
                            break;
                        }
                        if (curNode.Children.ContainsKey(source[i + j]))
                        {
                            curNode = curNode.Children[source[i + j]];
                            continue;
                        }
                        // try to fall back
                        if (curNode != root)
                        {
                            curNode = curNode.Fallback;
                            j--;
                        }
                    }
                    else if (curNode.Children.ContainsKey(source[i + j]))
                    {
                        curNode = curNode.Children[source[i + j]];
                    }
                    else
                    {
                        i = i + Math.Max(1, j);
                        break;
                    }
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
                        node.Fallback = backup ?? root;
                        queue.Enqueue(node);
                    }
                }
            }
            return root;
        }

        public class ACNode
        {
            public char Charactor { get; set; }
            public IDictionary<char,ACNode> Children { get; set; }
            public ACNode? Fallback { get; set; }
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
