using System.Collections.Generic;
using UnityEngine.Events;
namespace NodeVR
{
    public interface IHasActions
    {
        List<(string, UnityAction)> ReportActions();
    }
}