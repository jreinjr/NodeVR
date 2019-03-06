// Gracias a github.com/jdamador

namespace NodeVR
{
    public class Arc
    {
        public Arc backflow;
        //Global Variables
        public int toNodeIndex, capacity, flow;
        //Arc class, used to instantiate each edge in the graph
        public Arc(int toNodeIndex, int capacity)
        {
            this.toNodeIndex = toNodeIndex;
            this.capacity = capacity;
            this.flow = 0;
        }
    }
}
