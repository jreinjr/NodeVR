using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NodeVR
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Awake()
        {
            UpdateTransform();
        }

        void Update()
        {
            UpdateTransform();
        }

        void UpdateTransform()
        {
            transform.LookAt(Camera.main.transform.position, -Vector3.up);
        }
    }
}