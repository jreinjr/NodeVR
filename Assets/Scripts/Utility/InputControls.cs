using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InsurrectionVR
{
    [CreateAssetMenu(menuName = "Input/Input Controls")]
    public class InputControls : ScriptableObject
    {
        [Expandable]
        public List<InputFloat> hotkeys;
    }
}
