using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeVR
{
    /// <summary>
    /// Create the initial map
    /// </summary>
    public class NodeMapGenerator : MonoBehaviour
    {
        const float NODE_RADIUS = 0.5f;
        const int MAX_VALIDATOR_LOOPS = 50;

        public NodeBehaviour nodePrefab;
        public int numNodes;
        public float nodeMapRadius;

        public ArcBehaviour arcPrefab;
        public float maxArcDistance;
        public int maxArcs;

        private List<Vector3> spawnPoints;

        public static Action<NodeBehaviour> NodeBehaviourSpawned = delegate { };
        public static Action<ArcBehaviour> ArcBehaviourSpawned = delegate { };

        private void Start()
        {
            GenerateNodeMap();
        }

        private void GenerateNodeMap()
        {
            spawnPoints = GenerateSpawnPoints();
            spawnPoints = ValidateSpawnPoints();
            SpawnNodesAtPoints(spawnPoints);
            CalculateConnectedNodes();
            SpawnArcsBetweenNodes();
        }
        
        private List<Vector3> GenerateSpawnPoints()
        {
            var result = new List<Vector3>();
            for (int i = 0; i < numNodes; i++)
            {
                result.Add(GenerateRandomSpawnPoint());
            }
            return result;
        }

        private Vector3 GenerateRandomSpawnPoint()
        {
            return UnityEngine.Random.insideUnitSphere * nodeMapRadius;
        }

        /// <summary>
        /// Loops through the given points and verifies that none
        /// are closer together than NODE_RADIUS. May cause an infinite loop
        /// if it somehow continually generates new invalid guess points.
        /// </summary>
        /// <param name="spawnPoints"></param>
        /// <returns></returns>
        private List<Vector3> ValidateSpawnPoints()
        {
            var result = new List<Vector3>();
            var unvalidatedPts = spawnPoints.ToList();
            int loopCount = 0;

            while (unvalidatedPts.Count > 0)
            {
                loopCount++;
                if (loopCount > MAX_VALIDATOR_LOOPS)
                {
                    Debug.LogError("Max validator loops exceeded");
                    break;
                }
                foreach (Vector3 pt in spawnPoints)
                {
                    // Remove this point from unvalidated list 
                    // (a new random point will be added later 
                    // if this turns out to be invalid)
                    unvalidatedPts.Remove(pt);

                    // take the minimum of the distance to each other point
                    float minDist = spawnPoints.Where(p => p != pt)
                                               .Select(p => Vector3.Distance(p, pt))
                                               .OrderBy(q => q).First();
                    // Guess a new point if we're too close, add to result otherwise
                    if (minDist < NODE_RADIUS)
                    {
                        unvalidatedPts.Add(GenerateRandomSpawnPoint());
                    }
                    else if (minDist >= NODE_RADIUS)
                    {
                        result.Add(pt);
                    }
                }
            }
            return result;
        }

        private void SpawnNodesAtPoints(IEnumerable<Vector3> nodePoints)
        {
            foreach (Vector3 p in nodePoints)
            {
                SpawnNode(p);
            }
        }

        private void SpawnNode(Vector3 point)
        {
            var newNode = Instantiate(nodePrefab);
            newNode.transform.position = point;
            NodeBehaviourSpawned.Invoke(newNode);
        }

        private void CalculateConnectedNodes()
        {
            List<NodeBehaviour> tempOrderedNodes;
            foreach (NodeBehaviour n in NodeMapManager.AllNodes)
            {
                tempOrderedNodes = (from o in NodeMapManager.AllNodes
                                    let dist = Vector3.Distance(n.transform.position, o.transform.position)
                                    where dist < maxArcDistance
                                    where o != n
                                    orderby dist
                                    select o).Take(maxArcs).ToList();

                n.connectedNodes.AddRange(tempOrderedNodes);
            }
        }

        private void SpawnArcsBetweenNodes()
        {
            foreach (NodeBehaviour n in NodeMapManager.AllNodes)
            {
                var tempArcs = new List<ArcBehaviour>();
                foreach (NodeBehaviour o in n.connectedNodes)
                {
                    if (NodeMapManager.NodeArcsMap.ContainsKey(o))
                        continue;
                    else
                        tempArcs.Add(SpawnArc(fromNode: n, toNode: o));
                }

                // TODO: Loosely couple
                NodeMapManager.NodeArcsMap.Add(n, tempArcs);
            }
        }

        public ArcBehaviour SpawnArc(NodeBehaviour fromNode, NodeBehaviour toNode)
        {
            // Set new connection position to average of node positions
            Vector3 newPos = 0.5f * (fromNode.transform.position + toNode.transform.position);

            ArcBehaviour newArc = Instantiate(arcPrefab);
            newArc.transform.position = newPos;
            newArc.Initialize(fromNode, toNode);
            ArcBehaviourSpawned.Invoke(newArc);

            return newArc;
        }
    }
}