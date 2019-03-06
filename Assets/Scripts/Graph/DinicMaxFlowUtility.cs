using UnityEngine;
using System.Collections;

namespace NodeVR
{
    public static class DinicMaxFlowUtility
    {
        public const int BASE_FLOW_HIGH_NUMBER = 50000;

        public static int ComputeMaxFlow(Graph graph, int startNodeIndex, int endNodeIndex)
        {
            int flow = 0;
            int[] levelGraph = new int[graph.Nodes.Count];
            // While there exists an augmenting path in levelgraph 
            while (ConstructLevelGraphBFS(graph, startNodeIndex, endNodeIndex, ref levelGraph))
            {
                int[] visitedArcs = new int[graph.Nodes.Count];
                while (true)
                {
                    int blockingFlow = FindBlockingFlowDFS(graph, ref visitedArcs, ref levelGraph, startNodeIndex, endNodeIndex, BASE_FLOW_HIGH_NUMBER);
                    if (blockingFlow == 0)
                        break;
                    flow += blockingFlow;
                }
            }
            return flow;
        }

        /// <summary>
        /// Builds level graph (distance to start node) using breadth first search.
        /// Returns true if successful, false if no augmenting path to endNode was found.
        /// </summary>
        /// <returns>Returns true if successful, false if no augmenting path was found</returns>
        public static bool ConstructLevelGraphBFS(Graph graph, int startNodeIndex, int endNodeIndex, ref int[] levelGraph)
        {
            levelGraph.Fill(-1);
            levelGraph[startNodeIndex] = 0;
            // The queue is the length of total number of nodes,
            // we should visit each node exactly once.
            int[] queue = new int[graph.Nodes.Count];
            int sizeQ = 0;
            // Queue 0 is initialized to startNodeIndex.
            queue[sizeQ++] = startNodeIndex;
            for (int i = 0; i < sizeQ; i++)
            {
                int nextNodeIndex = queue[i];
                foreach (Arc arc in graph.Nodes[nextNodeIndex].Arcs)
                {
                    // levelGraph is < 0 before we have visited it
                    if (levelGraph[arc.toNodeIndex] < 0 && arc.flow < arc.capacity)
                    {
                        levelGraph[arc.toNodeIndex] = levelGraph[nextNodeIndex] + 1;
                        queue[sizeQ++] = arc.toNodeIndex;
                    }
                }
            }
            return levelGraph[endNodeIndex] >= 0;
        }

        /// <summary>
        /// Finds blocking flow using depth first search
        /// Returns flow
        /// </summary>
        public static int FindBlockingFlowDFS(Graph graph, ref int[] visitedArcs, ref int[] levelGraph, int startNodeIndex, int endNodeIndex, int flow)
        {
            if (startNodeIndex == endNodeIndex)
                return flow;
            // For each arc connected to this node
            for (; visitedArcs[startNodeIndex] < graph.Nodes[startNodeIndex].Arcs.Count; ++visitedArcs[startNodeIndex])
            {
                Arc arc = graph.Nodes[startNodeIndex].Arcs[visitedArcs[startNodeIndex]];
                if (levelGraph[arc.toNodeIndex] == levelGraph[startNodeIndex] + 1 && arc.flow < arc.capacity)
                {
                    int df = FindBlockingFlowDFS(graph, ref visitedArcs, ref levelGraph, arc.toNodeIndex, endNodeIndex, Mathf.Min(flow, arc.capacity - arc.flow));
                    if (df > 0)
                    {
                        arc.flow += df;
                        arc.backflow.flow -= df;
                        return df;
                    }
                }
            }
            return 0;
        }
    }

}
