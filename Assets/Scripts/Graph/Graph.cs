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
                AddNode();
            }
        }

        public void AddNode()
        {
            Nodes.Add(new Node());
        }

        public void AddArc(Node start, Node end, int capacity)
        {
            start.arcs.Add(new Arc(end.index, Nodes[end.index].arcs.Count(), capacity));
            end.arcs.Add(new Arc(end.index, Nodes[end.index].arcs.Count(), 0));
        }

        public void AddArc(int start, int end, int capacity)
        {
            Nodes[start].arcs.Add(new Arc(end, Nodes[end].arcs.Count(), capacity));
            Nodes[end].arcs.Add(new Arc(start, Nodes[start].arcs.Count() - 1, 0));
        }
    }
}