using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NodeVR;

namespace Tests
{
    public class GraphTests
    {
        public class ConstructorTests
        {
            [Test]
            public void New_Graph_Has_No_Nodes()
            {
                Graph g = new Graph();
                Assert.AreEqual(g.Nodes.Count, 0);
            }

            [Test]
            public void New_Graph_Next_Node_Index_Is_0()
            {
                Graph g = new Graph();
                Assert.AreEqual(g.NextNodeIndex, 0);
            }
        }

        public class AddNodeMethod
        {
            [Test]
            public void Add_Node_Adds_One_Node_To_New_Graph()
            {
                Graph g = new Graph();
                g.AddNode();
                Assert.AreEqual(g.Nodes.Count, 1);
            }

            [Test]
            public void Add_Node_Increments_Graph_Node_Index()
            {
                Graph g = new Graph();
                g.AddNode();
                Assert.AreEqual(g.NextNodeIndex, 1);
            }
        }
           
    }
}
