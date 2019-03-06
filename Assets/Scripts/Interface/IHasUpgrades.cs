using System.Collections.Generic;
using UnityEngine.Events;
namespace NodeVR
{
    public interface IHasUpgrades
    {
        List<(string, UnityAction)> ReportAvailableUpgrades();
    }
}