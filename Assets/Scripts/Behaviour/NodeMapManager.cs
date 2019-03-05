using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NodeVR
{
    /// <summary>
    /// Record graph relationships
    /// </summary>
    public class NodeMapManager : MonoBehaviour
    {
        [HideInInspector] public static List<NodeBehaviour> AllNodes;
        [HideInInspector] public static List<ArcBehaviour> AllArcs;
        public static Dictionary<NodeBehaviour, List<ArcBehaviour>> NodeArcsMap;

        private void Awake()
        {
            Init();
            FindAndAddExistingNodes();
            FindAndAddExistingArcs();
        }

        private void FindAndAddExistingNodes()
        {
            var existingNodes = FindObjectsOfType<NodeBehaviour>().ToList();
            foreach (var node in existingNodes)
            {
                OnNodeBehaviourSpawned(node);
            }
        }

        private void FindAndAddExistingArcs()
        {
            var existingArcs = FindObjectsOfType<ArcBehaviour>().ToList();
            foreach (var arc in existingArcs)
            {
                OnArcBehaviourSpawned(arc);
            }
        }


        private void Init()
        {
            NodeArcsMap = new Dictionary<NodeBehaviour, List<ArcBehaviour>>();
            AllNodes = new List<NodeBehaviour>();
            AllArcs = new List<ArcBehaviour>();

            NodeMapGenerator.NodeBehaviourSpawned += OnNodeBehaviourSpawned;
            NodeMapGenerator.ArcBehaviourSpawned += OnArcBehaviourSpawned;
        }

        private void Update()
        {

            foreach (NodeBehaviour n in AllNodes.Where(p => p.IsProducing))
            {
                n.Tick();
            }
            foreach (ArcBehaviour c in AllArcs)
            {
                c.Tick();
            }

        }

        private void OnNodeBehaviourSpawned(NodeBehaviour node)
        {
            AllNodes.Add(node);
            NodeArcsMap.Add(node, new List<ArcBehaviour>());
        }

        private void OnArcBehaviourSpawned(ArcBehaviour arc)
        {
            AllArcs.Add(arc);
            NodeArcsMap[arc.fromNode].Add(arc);
            NodeArcsMap[arc.toNode].Add(arc);
        }

    }
}