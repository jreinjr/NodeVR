// Gracias a github.com/jdamador
using UnityEngine;
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
        public static int maxFlow(Graph graph, Node start, Node end)
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
        static bool dinicBFS(Graph graph, Node start, Node end, int[] dist)
        {
            dist.Fill(-1);
            dist[start.index] = 0;
            int[] queue = new int[graph.Nodes.Count()]; //a queue is created to add the remaining values
            int sizeQ = 0;
            queue[sizeQ++] = start.index;
            for (int i = 0; i < sizeQ; i++)
            {
                int u = queue[i];
                foreach (Arc arc in graph.Nodes[u].Arcs)
                {
                    if (dist[arc.fromNode.index] < 0 && arc.currentFlow < arc.capacity)
                    {
                        dist[arc.fromNode.index] = dist[u] + 1;
                        queue[sizeQ++] = arc.fromNode.index;
                    }
                }
            }
            return dist[end.index] >= 0;
        }

        /// <summary>
        /// Depth first search
        /// </summary>
        static int dinicDfs(Graph graph, int[] ptr, int[] dist, Node end, Node start, int flow)
        {
            if (start == end)
                return flow;
            for (; ptr[start.index] < start.Arcs.Count(); ++ptr[start.index])
            {
                Arc arc = start.Arcs[ptr[start.index]];
                if (dist[arc.fromNode.index] == dist[start.index] + 1 && arc.currentFlow < arc.capacity)
                {
                    int df = dinicDfs(graph, ptr, dist, end, arc.fromNode, Math.Min(flow, arc.capacity - arc.currentFlow));
                    if (df > 0)
                    {
                        arc.currentFlow += df;
                        arc.fromNode.Arcs.Where(p => p.toNode == arc.toNode).First().currentFlow -= df;
                        //graph.Nodes[arc.fromNode.index].Arcs[arc.toNode.index].currentFlow -= df;
                        return df;
                    }
                }
            }
            return 0;
        }
    }
}