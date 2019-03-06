// Gracias a github.com/jdamador

using System;
using System.Collections.Generic;
using System.Linq;

namespace NodeVR
{
    public class Graph
    {
        public int NextNodeIndex { get; protected set; }
        public List<Node> Nodes { get; protected set; }

        public Graph()
        {
            this.Nodes = new List<Node>();
            this.NextNodeIndex = 0;
        }

        public Node AddNode()
        {
            var newNode = new Node(NextNodeIndex++);
            Nodes.Add(newNode);
            return newNode;
        }

        public void AddArc(Node start, Node end, int capacity)
        {
            Arc forward = new Arc(end, capacity);
            Arc backflow = new Arc(start, 0);

            forward.backflow = backflow;
            backflow.backflow = forward;
            
            start.Arcs.Add(forward);
            end.Arcs.Add(backflow);
        }
    }
}