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
            graph.AddArc(0, 1, 10);
            graph.AddArc(0, 2, 10);

            graph.AddArc(1, 2, 2);
            graph.AddArc(1, 4, 8);
            graph.AddArc(1, 3, 4);

            graph.AddArc(2, 4, 9);

            graph.AddArc(3, 5, 10);

            graph.AddArc(4, 3, 6);
            graph.AddArc(4, 5, 10);

            Debug.Log(GraphUtility.maxFlow(graph, 0, 5));
            Debug.Log(graph.Nodes[0].arcs[0].flow);
        }
    }
}