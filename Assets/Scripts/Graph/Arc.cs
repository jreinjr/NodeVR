// Gracias a github.com/jdamador

namespace NodeVR
{
    public class Arc
    {
        //Global Variables
        public int index, toNode, capacity, flow;
        //Arc class, used to instantiate each edge in the graph
        public Arc(int fromNode, int toNode, int capacity)
        {
            this.index = fromNode;
            this.toNode = toNode;
            this.capacity = capacity;
        }
    }
}
