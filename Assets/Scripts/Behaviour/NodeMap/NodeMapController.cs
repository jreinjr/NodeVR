using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
namespace NodeVR
{
    /// <summary>
    /// Handles interactions and updates for nodes & arcs
    /// </summary>
    public class NodeMapController : MonoBehaviour
    {
        public NodeMapManager nodeConnectionMapManager;
        private Selectable currentSelection;

        public static Action<Selectable> SelectableSelected = delegate { };

        private void Update()
        {
            // Abort if cursor is over a UI object
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Select();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                //Upgrade();
            }
        }

        void Select()
        {
            var clicked = GetValidClick<Selectable>();
            if (clicked != null)
            {
                if (currentSelection != null)
                    currentSelection.Deselect();

                clicked.Select();
                currentSelection = clicked;
                SelectableSelected.Invoke(clicked);
            }
        }

        void Upgrade()
        {
            var clicked = GetValidClick<NodeBehaviour>();
            if (clicked != null)
            {
                //clicked.AddPsi();
            }
        }

        protected T GetValidClick<T>()
        {
            T hitObject = default(T);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                hitObject = hit.collider.GetComponent<T>();
            }

            return hitObject;
        }
    }
}