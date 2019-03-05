using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace NodeVR
{
    public class UIInfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI headerTextUGUI;


        public Transform infoFieldsContainer;
        public UIInfoField uiInfoFieldPrefab;

        private List<UIInfoField> infoFields;
        private IHasInfo currentInfoObject;
        private List<(string, string)> currentInfo;
        public string headerText;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (currentInfoObject != null)
                RebuildInterface();
        }

        private void Initialize()
        {
            infoFields = new List<UIInfoField>();
            headerTextUGUI.text = headerText;
            NodeMapController.SelectableSelected += OnSelectableSelected;
        }

        protected void OnSelectableSelected(ISelectable selectable)
        {
            if (selectable.GetType().GetInterfaces().Contains(typeof(IHasInfo)))
            {
                currentInfoObject = selectable as IHasInfo;
                RebuildInterface();
            }
        }

        protected void UpdateCurrentInfo()
        {
            currentInfo = currentInfoObject.ReportInfo();
        }

        protected void UpdateInterface()
        {
            UpdateCurrentInfo();

            foreach (var i in currentInfo)
            {
                // TODO: Implement update rather than rebuild
            }
        }

        protected void RebuildInterface()
        {
            WipeInterface();
            UpdateCurrentInfo();
            SpawnInfoFieldsFromList();
        }

        private void WipeInterface()
        {
            for (int i = infoFields.Count - 1; i >= 0; i--)
            {
                Destroy(infoFields[i].gameObject);
                infoFields.Remove(infoFields[i]);
            }
        }

        private void SpawnInfoFieldsFromList()
        {
            foreach (var i in currentInfo)
            {
                SpawnInfoField(i.Item1, i.Item2);
            }
        }

        private void SpawnInfoField(string property, string value)
        {
            var newInfoField = Instantiate(uiInfoFieldPrefab);
            newInfoField.transform.SetParent(infoFieldsContainer);
            newInfoField.Initialize(property, value);
            infoFields.Add(newInfoField);
        }
    }
}