using UnityEngine;
using System.Collections;

namespace NodeVR
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private GameObject _selectionGO;
        public bool IsSelected { get; protected set; }

        public void Select()
        {
            IsSelected = true;
            _selectionGO.SetActive(true);
        }

        public void Deselect()
        {
            IsSelected = false;
            _selectionGO.SetActive(false);
        }
    }

}