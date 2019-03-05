// Gracias a github.com/jdamador

using System.Collections.Generic;
using System.Linq;

namespace NodeVR
{
    public class Graph
    {
        public List<Node> Nodes {get; protected set;}

        public Graph(int numNodes)
        {
            Nodes = new List<Node>();
            for (int i = 0; i < numNodes; i++)
            {
                AddNode(i);
            }
        }

        public void AddNode(int index)
        {
            Nodes.Add(new Node(index));
        }

        public void AddArc(Node start, Node end, int capacity)
        {
            start.Arcs.Add(new Arc(end, start, capacity));
            end.Arcs.Add(new Arc(start, end, 0));
        }
    }
}