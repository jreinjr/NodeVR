// Gracias a github.com/jdamador
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace NodeVR
{
    public class GraphUtility
    {
        /// <summary>
        /// Main function receives the two nodes from which you want to calculate the flow
        /// </summary>
        /// <param name="graph"></param>
        /// <param name="startNodeIndex"></param>
        /// <param name="endNodeIndex"></param>
        /// <returns></returns>
        public static int maxFlow(Graph graph, int startNodeIndex, int endNodeIndex)
        {
            int flow = 0;
            int[] levelGraph = new int[graph.Nodes.Count];
            // While there exists an augmenting path in levelgraph 
            while (ConstructLevelGraph(graph, startNodeIndex, endNodeIndex, levelGraph))
            {
                int[] ptr = new int[graph.Nodes.Count];
                while (true)
                {
                    int blockingFlow = FindBlockingFlowDFS(graph, ptr, levelGraph, startNodeIndex, endNodeIndex, 50000);
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
        /// <param name="graph">Arreglo con le grafo</param>
        /// <param name="startNodeIndex">Vertice de origen</param>
        /// <param name="endNodeIndex">Vertice de destino</param>
        /// <param name="levelGraph">Array of distances from start node</param>
        /// <returns>Returns true if successful, false if no augmenting path was found</returns>
        static bool ConstructLevelGraph(Graph graph, int startNodeIndex, int endNodeIndex, int[] levelGraph)
        {
            levelGraph.Fill(-1);
            levelGraph[startNodeIndex] = 0;
            // The queue is the length of total number of nodes,
            // we should visit each node exactly once.
            int[] queue = new int[graph.Nodes.Count()];
            int sizeQ = 0;
            // Queue 0 is initialized to startNodeIndex.
            queue[sizeQ++] = startNodeIndex;
            for (int i = 0; i < sizeQ; i++)
            {
                int nextNodeIndex = queue[i];
                foreach (Arc arc in graph.Nodes[nextNodeIndex].arcs)
                {
                    // levelGraph is < 0 before we have visited it
                    if (levelGraph[arc.fromNodeIndex] < 0 && arc.flow < arc.capacity)
                    {
                        levelGraph[arc.fromNodeIndex] = levelGraph[nextNodeIndex] + 1;
                        queue[sizeQ++] = arc.fromNodeIndex;
                    }
                }
            }
            return levelGraph[endNodeIndex] >= 0;
        }

        /// <summary>
        /// Depth first search
        /// </summary>
        static int FindBlockingFlowDFS(Graph graph, int[] ptr, int[] levelGraph,  int startNodeIndex, int endNodeIndex, int flow)
        {
            if (startNodeIndex == endNodeIndex)
                return flow;
            // For each arc connected to this node
            for (; ptr[startNodeIndex] < graph.Nodes[startNodeIndex].arcs.Count(); ++ptr[startNodeIndex])
            {
                Arc arc = graph.Nodes[startNodeIndex].arcs[ptr[startNodeIndex]];
                if (levelGraph[arc.fromNodeIndex] == levelGraph[startNodeIndex] + 1 && arc.flow < arc.capacity)
                {
                    int df = FindBlockingFlowDFS(graph, ptr, levelGraph, arc.fromNodeIndex, endNodeIndex, Math.Min(flow, arc.capacity - arc.flow));
                    if (df > 0)
                    {
                        arc.flow += df;
                        graph.Nodes[arc.fromNodeIndex].arcs[arc.toNodeIndex].flow -= df;
                        return df;
                    }
                }
            }
            return 0;
        }
    }
}