using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
using System;
namespace NodeVR
{
    public class NodeBehaviour : MonoBehaviour, ISelectable, IHasInfo, IHasActions
    {
        private static int nextIndex = 0;
        public int GraphNodeIndex;

        public Renderer rend;
        public Color nodeColor;
        public List<NodeBehaviour> connectedNodes;
        public List<ArcBehaviour> connections;
        public GameObject selectionGO;

        public bool IsSelected { get; protected set; }
        public bool IsProducing { get; protected set; }

        [SerializeField] private float currentPsi = 0f;
        public int maxPsi = 100;
        public int psiPerSec = 1;
        public int maxActiveConnections = 1;

        private void Start()
        {
            GraphNodeIndex = nextIndex++;

            connectedNodes = new List<NodeBehaviour>();
            connections = new List<ArcBehaviour>();
            rend = GetComponent<Renderer>();

            if (psiPerSec > Mathf.Epsilon)
                IsProducing = true;
            //rend.material = new Material(rend.sharedMaterial);
        }
        /*
        private void Update()
        {
            Tick();
        }
        */
        public void Tick()
        {
            TickPsiPerSec();
            PsiFlow();
            UpdateMaterialColor();
        }

        protected void TickPsiPerSec()
        {
            currentPsi += psiPerSec * Time.deltaTime;
            currentPsi = Mathf.Clamp(currentPsi, 0, maxPsi);
            currentPsi = (float)Math.Round(currentPsi, 2);
        }

        protected void PsiFlow()
        {
            var flowIn = connections.Where(p => p.IsFlowing && p.toNode == this)
                                    .Select(q => q.maxPsiThruPerSec).ToList();

            var flowOut = connections.Where(p => p.IsFlowing && p.fromNode == this)
                                     .Select(q => q.maxPsiThruPerSec).ToList();

            //foreach (NodeBehaviour recipient in flowOut.Select(p=>p.))
        }

        public bool IsConnectedTo(NodeBehaviour node)
        {
            return connectedNodes.Contains(node);
        }

        public void UpgradeMaxPsi()
        {
            maxPsi += 100;
        }

        public void UpgradePsiPerSec()
        {
            IsProducing = true;
            psiPerSec += 1;
        }

        public void UpgradeMaxActiveConnections()
        {
            maxActiveConnections++;
        }

        public List<(string, string)> ReportInfo()
        {
            var report = new List<(string, string)>();

            report.Add(("Current Psi", currentPsi.ToString()));
            report.Add(("Max Psi", maxPsi.ToString()));
            report.Add(("Psi Per Sec", psiPerSec.ToString()));
            report.Add(("Max Active Connections", maxActiveConnections.ToString()));

            return report;
        }

        private void UpdateMaterialColor()
        {
            float a = currentPsi / maxPsi;
            rend.material.SetFloat("_Intensity", a);
        }

        public void Select()
        {
            IsSelected = true;
            selectionGO.SetActive(true);
        }

        public void Deselect()
        {
            IsSelected = false;
            selectionGO.SetActive(false);
        }

        public List<(string, UnityAction)> ReportActions()
        {
            var report = new List<(string, UnityAction)>();

            report.Add(("UpgradeMaxPsi", new UnityAction(UpgradeMaxPsi)));
            report.Add(("UpgradePsiPerSec", new UnityAction(UpgradePsiPerSec)));
            report.Add(("UpgradeMaxActiveConnections", new UnityAction(UpgradeMaxActiveConnections)));

            return report;
        }
    }
}