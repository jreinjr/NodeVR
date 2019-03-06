using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NodeVR;

namespace Tests
{
    public class DinicMaxFlowUtilityTests
    {
        public class ComputeMaxFlowMethod
        {
            Graph flowGraph;
            Node superSource;
            Node superSink;

            [SetUp]
            public void SetUp_Sample_Data()
            {
                flowGraph = new Graph();

                superSource = flowGraph.AddNode();
                superSink = flowGraph.AddNode();

                Node n0 = flowGraph.AddNode();
                Node n1 = flowGraph.AddNode();
                Node n2 = flowGraph.AddNode();
                Node n3 = flowGraph.AddNode();
                Node n4 = flowGraph.AddNode();
                Node n5 = flowGraph.AddNode();

                flowGraph.AddArc(superSource, n0, FlowGraphGenerator.SUPER_FLOW_BIG_NUMBER);

                flowGraph.AddArc(n0, n1, 10);
                flowGraph.AddArc(n0, n2, 10);

                flowGraph.AddArc(n1, n2, 2);
                flowGraph.AddArc(n1, n4, 8);
                flowGraph.AddArc(n1, n3, 4);

                flowGraph.AddArc(n2, n4, 9);

                flowGraph.AddArc(n3, n5, 10);
                flowGraph.AddArc(n3, n0, 10);

                flowGraph.AddArc(n4, n3, 6);
                flowGraph.AddArc(n4, n5, 10);

                flowGraph.AddArc(n5, superSink, FlowGraphGenerator.SUPER_FLOW_BIG_NUMBER);
            }

            [Test]
            public void Sample_Data_Yields_Expected_Result()
            {

                Assert.AreEqual(19, DinicMaxFlowUtility.ComputeMaxFlow(flowGraph, superSource, superSink));
            }
        }
    }
}
