// Gracias a github.com/jdamador

namespace NodeVR
{
    public class Arc
    {
        //Global Variables
        public Node fromNode, toNode;
        public int capacity, currentFlow;
        //Arc class, used to instantiate each edge in the graph
        public Arc(Node fromNode, Node toNode, int capacity)
        {
            this.fromNode = fromNode;
            this.toNode = toNode;
            this.capacity = capacity;
            this.currentFlow = 0;
        }
    }
}
