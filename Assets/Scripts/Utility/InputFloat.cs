using UnityEngine;

namespace InsurrectionVR
{
    [CreateAssetMenu(menuName ="Input/Input Float")]
    [System.Serializable]
    public class InputFloat : ScriptableObject
    {
        // Input
        public bool isAxis;
        public string AxisString;
        public KeyCode KeyCode;

        // Output
        public float Value { get { return GetInput(); } }

        protected float GetInput()
        {
            return  isAxis ?
                    Input.GetAxis(AxisString) :
                    Input.GetKeyDown(KeyCode) ? 1 : 0;
        }
    }
}
