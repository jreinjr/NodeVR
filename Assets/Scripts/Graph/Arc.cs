// Gracias a github.com/jdamador

namespace NodeVR
{
    public class Arc
    {
        //Global Variables
        public int fromNodeIndex, toNodeIndex, capacity, flow;
        //Arc class, used to instantiate each edge in the graph
        public Arc(int fromNodeIndex, int toNodeIndex, int capacity)
        {
            this.fromNodeIndex = fromNodeIndex;
            this.toNodeIndex = toNodeIndex;
            this.capacity = capacity;
            this.flow = 0;
        }
    }
}
