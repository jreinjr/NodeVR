// Gracias a github.com/jdamador

using System.Collections.Generic;

namespace NodeVR
{
    public class Node
    {
        public int Index { get; }
        public List<Arc> Arcs;

        /// <summary>
        /// The class that instantiates this should ensure Index is unique per graph!
        /// </summary>
        /// <param name="index">Index should be unique per graph!</param>
        public Node(int index)
        {
            this.Index = index;
            this.Arcs = new List<Arc>();
        }

        public static implicit operator int(Node n)
        {
            return n.Index;
        }
    }
}