// Gracias a github.com/jdamador

using System.Collections.Generic;

namespace NodeVR
{
    public class Node
    {
        public int index;
        public List<Arc> arcs;

        public Node()
        {
            arcs = new List<Arc>();
        }
    }
}