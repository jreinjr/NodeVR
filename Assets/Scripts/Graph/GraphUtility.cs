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
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int maxFlow(Graph graph, int start, int end)
        {
            int flow = 0;
            int[] dist = new int[graph.Nodes.Count];
            while (dinicBFS(graph, start, end, dist))
            {
                int[] ptr = new int[graph.Nodes.Count];
                while (true)
                {
                    int df = dinicDfs(graph, ptr, dist, end, start, 50000);
                    if (df == 0)
                        break;
                    flow += df;
                }
            }
            return flow;
        }

        /// <summary>
        /// Breadth first search
        /// </summary>
        /// <param name="graph">Arreglo con le grafo</param>
        /// <param name="start">Vertice de origen</param>
        /// <param name="end">Vertice de destino</param>
        /// <param name="dist">distancia</param>
        /// <returns></returns>
        static bool dinicBFS(Graph graph, int start, int end, int[] dist)
        {
            dist.Fill(-1);
            dist[start] = 0;
            int[] queue = new int[graph.Nodes.Count()]; //a queue is created to add the remaining values
            int sizeQ = 0;
            queue[sizeQ++] = start;
            for (int i = 0; i < sizeQ; i++)
            {
                int u = queue[i];
                foreach (Arc aux in graph.Nodes[u].arcs)
                {
                    if (dist[aux.index] < 0 && aux.flow < aux.capacity)
                    {
                        dist[aux.index] = dist[u] + 1;
                        queue[sizeQ++] = aux.index;
                    }
                }
            }
            return dist[end] >= 0;
        }

        /// <summary>
        /// Depth first search
        /// </summary>
        static int dinicDfs(Graph graph, int[] ptr, int[] dist, int dest, int u, int flow)
        {
            if (u == dest)
                return flow;
            for (; ptr[u] < graph.Nodes[u].arcs.Count(); ++ptr[u])
            {
                Arc e = graph.Nodes[u].arcs[ptr[u]];
                if (dist[e.index] == dist[u] + 1 && e.flow < e.capacity)
                {
                    int df = dinicDfs(graph, ptr, dist, dest, e.index, Math.Min(flow, e.capacity - e.flow));
                    if (df > 0)
                    {
                        e.flow += df;
                        graph.Nodes[e.index].arcs[e.toNode].flow -= df;
                        return df;
                    }
                }
            }
            return 0;
        }
    }
}