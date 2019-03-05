using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeVR
{
    public class FlowGraphGenerator : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            ComputeMaxFlow();
        }

        /// <summary>
        /// Main, example of calculating maximum flow
        /// </summary>
        /// <param name="args"></param>
        public void ComputeMaxFlow()
        {
            Graph graph = new Graph(6);
            graph.AddArc(graph.Nodes[0], graph.Nodes[1], 10);
            graph.AddArc(graph.Nodes[0], graph.Nodes[2], 10);

            graph.AddArc(graph.Nodes[1], graph.Nodes[2], 2);
            graph.AddArc(graph.Nodes[1], graph.Nodes[4], 8);
            graph.AddArc(graph.Nodes[1], graph.Nodes[3], 4);

            graph.AddArc(graph.Nodes[2], graph.Nodes[4], 9);

            graph.AddArc(graph.Nodes[3], graph.Nodes[5], 10);

            graph.AddArc(graph.Nodes[4], graph.Nodes[3], 6);
            graph.AddArc(graph.Nodes[4], graph.Nodes[5], 10);

            Debug.Log(GraphUtility.maxFlow(graph, graph.Nodes[0], graph.Nodes[5]));
            Debug.Log(graph.Nodes[0].Arcs[0].currentFlow);
        }
    }
}