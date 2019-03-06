using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NodeVR
{
    public class Test : MonoBehaviour
    {
        public FlowGraphGenerator fGG;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fGG.UpdateFlowGraph();
                var fG = fGG.FlowGraph;
                ReportMaxFlow(fG, fGG.superSource, fGG.superSink);
            }
                

        }

        public void ReportMaxFlow(Graph flowGraph, int fromNodeIndex, int toNodeIndex)
        {
            int maxFlow = DinicMaxFlowUtility.ComputeMaxFlow(flowGraph, fromNodeIndex, toNodeIndex);

            Debug.Log(maxFlow);

            foreach (var node in flowGraph.Nodes)
            {
                foreach (var arc in node.Arcs.Where(p => Math.Abs(p.flow) > 0))
                {
                    Debug.Log("From node index: " + node.Index + "  Arc flow: " + arc.flow);
                }
            }
        }
    }
}