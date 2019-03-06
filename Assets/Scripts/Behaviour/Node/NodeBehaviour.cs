using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;
namespace NodeVR
{
    [RequireComponent(typeof(Selectable), typeof(NodeUpgrades), typeof(NodeStats))]
    public class NodeBehaviour : MonoBehaviour
    {
        public Node graphNode;
        public Node incomeNode;
        public Node capacitorNode;

        public Renderer rend;
        public Color nodeColor;
        public List<NodeBehaviour> connectedNodes;

        public Selectable Selectable { get; protected set; }
        public NodeUpgrades Upgrades { get; protected set; }
        public NodeStats Stats { get; protected set; }

        public bool IsProducing { get { return Stats.PsiPerSec > Mathf.Epsilon; } }
        public bool HasPsiCapacity { get { return Stats.currentPsi < Stats.MaxPsi; } }
        
        private void Awake()
        {
            Selectable = GetComponent<Selectable>();
            Upgrades = GetComponent<NodeUpgrades>();
            Stats = GetComponent<NodeStats>();

            Upgrades.MaxPsiUpgraded += Stats.UpgradeMaxPsi;
            Upgrades.MaxActiveConnectionsUpgraded += Stats.UpgradeMaxActiveConnections;
            Upgrades.PsiPerSecUpgraded += Stats.UpgradePsiPerSec;

            connectedNodes = new List<NodeBehaviour>();
            rend = GetComponent<Renderer>();
        }

        public void Tick()
        {
            UpdateMaterialColor();
        }

        private void UpdateMaterialColor()
        {
            float a = Stats.currentPsi / Stats.MaxPsi;
            rend.material.SetFloat("_Intensity", a);
        }
    }
}