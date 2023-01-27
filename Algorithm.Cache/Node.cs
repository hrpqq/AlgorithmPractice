using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm.Cache
{
    public class Node<T>
    {
        public Node<T> Previous { get; set; }

        public Node<T> Next { get; set; }

        public T Payload { get; set; }

        public Node(T payload = default(T))
        {
            Payload = payload;
        }
    }
}
