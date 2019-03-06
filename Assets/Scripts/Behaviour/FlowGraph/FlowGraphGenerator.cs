using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeVR
{
    public class FlowGraphGenerator : MonoBehaviour
    {
        public Graph FlowGraph { get; protected set; }
        public Node superSource;
        public Node superSink;

        public const int SUPER_FLOW_BIG_NUMBER = 500000;
        public const int CAPACITY_DRAIN_PER_SEC = 1;

        public void UpdateFlowGraph()
        {
            FlowGraph = ConvertNodeMapToFlowGraph(NodeMapManager.AllNodes, NodeMapManager.AllArcs);
        }

        private Graph ConvertNodeMapToFlowGraph(List<NodeBehaviour> nodeBehaviours, List<ArcBehaviour> arcBehaviours)
        {
            // Create a new empty graph
            Graph newGraph = new Graph();
            superSource = newGraph.AddNode();
            superSink = newGraph.AddNode();

            foreach (NodeBehaviour nodeBehaviour in nodeBehaviours)
            {
                // Create a new graph node and link it with this nodeBehaviour
                Node newGraphNode = newGraph.AddNode();
                nodeBehaviour.graphNode = newGraphNode;

                // Add another graphNode if this nodeBehaviour is generating income
                // Set its flow equal to income per second
                if (nodeBehaviour.IsProducing)
                {
                    Node newIncomeNode = newGraph.AddNode();
                    nodeBehaviour.incomeNode = newIncomeNode;
                    // Flow from income to graphNode
                    newGraph.AddArc(newIncomeNode, newGraphNode, nodeBehaviour.Stats.PsiPerSec);
                    // Connect income node to super source
                    newGraph.AddArc(superSource, newIncomeNode, SUPER_FLOW_BIG_NUMBER);
                }
                // Add another graphNode if this nodeBehaviour has available capacity
                // This is a sink node that drains to superSink while capacity remains
                // IDEA: Increase capacity drain per sec while constructing?
                if (nodeBehaviour.HasPsiCapacity)
                {
                    Node newCapacitorNode = newGraph.AddNode();
                    nodeBehaviour.capacitorNode = newCapacitorNode;
                    newGraph.AddArc(newGraphNode, newCapacitorNode, CAPACITY_DRAIN_PER_SEC);
                    // Connect capacitor node to super sink
                    newGraph.AddArc(newCapacitorNode, superSink, SUPER_FLOW_BIG_NUMBER);
                }
            }
            foreach(ArcBehaviour arcBehaviour in arcBehaviours)
            {
                var fromGraphNode = arcBehaviour.fromNode.graphNode;
                var toGraphNode = arcBehaviour.toNode.graphNode;
                newGraph.AddArc(fromGraphNode, toGraphNode, arcBehaviour.maxPsiThruPerSec);
            }

            return newGraph;
        }
    }
}