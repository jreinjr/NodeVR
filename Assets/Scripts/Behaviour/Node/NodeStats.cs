using System;
using UnityEngine;
using System.Collections.Generic;

namespace NodeVR
{
    public class NodeStats : MonoBehaviour, IHasStats
    {
        public float currentPsi;
        public int MaxPsi { get; protected set; }
        public int PsiPerSec { get; protected set; }
        public int MaxActiveConnections { get; protected set; }

        protected void Update()
        {
            TickPsiPerSec();
        }

        public void UpgradeMaxPsi()
        {
            MaxPsi += 100;
        }

        public void UpgradePsiPerSec()
        {
            PsiPerSec += 1;
        }

        public void UpgradeMaxActiveConnections()
        {
            MaxActiveConnections++;
        }

        protected void TickPsiPerSec()
        {
            currentPsi += PsiPerSec * Time.deltaTime;
            currentPsi = Mathf.Clamp(currentPsi, 0, MaxPsi);
            currentPsi = (float)Math.Round(currentPsi, 2);
        }

        public List<(string, string)> ReportStats()
        {
            var report = new List<(string, string)>();

            report.Add(("Current Psi", currentPsi.ToString()));
            report.Add(("Max Psi", MaxPsi.ToString()));
            report.Add(("Psi Per Sec", PsiPerSec.ToString()));
            report.Add(("Max Active Connections", MaxActiveConnections.ToString()));

            return report;
        }
    }
}