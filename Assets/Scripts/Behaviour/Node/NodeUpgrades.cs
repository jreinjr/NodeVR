using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

namespace NodeVR
{
    public class NodeUpgrades : MonoBehaviour, IHasUpgrades
    {
        public UnityAction MaxPsiUpgraded = delegate { };
        public UnityAction PsiPerSecUpgraded = delegate { };
        public UnityAction MaxActiveConnectionsUpgraded = delegate { };


        public List<(string, UnityAction)> ReportAvailableUpgrades()
        {
            var report = new List<(string, UnityAction)>();

            report.Add(("UpgradeMaxPsi", MaxPsiUpgraded));
            report.Add(("UpgradePsiPerSec", PsiPerSecUpgraded));
            report.Add(("UpgradeMaxActiveConnections", MaxActiveConnectionsUpgraded));

            return report;
        }
    }
}
