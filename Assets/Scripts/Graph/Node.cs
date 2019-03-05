// Gracias a github.com/jdamador

using System.Collections.Generic;

namespace NodeVR
{
    public class Node
    {
        public int index;
        public List<Arc> Arcs;

        public Node(int index)
        {
            this.index = index;
            this.Arcs = new List<Arc>();
        }
    }
}