using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace NodeVR
{
    public class UIStatsPanel : MonoBehaviour
    {
        public TextMeshProUGUI headerTextUGUI;


        public Transform statFieldsContainer;
        public UIStatsField uiStatsFieldPrefab;

        private List<UIStatsField> statFields;
        private IHasStats currentStatsObject;
        private List<(string, string)> currentStat;
        public string headerText;

        private void Start()
        {
            Initialize();
        }

        private void Update()
        {
            if (currentStatsObject != null)
                RebuildInterface();
        }

        private void Initialize()
        {
            statFields = new List<UIStatsField>();
            headerTextUGUI.text = headerText;
            NodeMapController.SelectableSelected += OnSelectableSelected;
        }

        protected void OnSelectableSelected(Selectable selectable)
        {
            var statComponent = selectable.gameObject.GetComponent<IHasStats>();
            if (statComponent != null)
            {
                currentStatsObject = statComponent as IHasStats;
                RebuildInterface();
            }
        }

        protected void UpdateCurrentStat()
        {
            currentStat = currentStatsObject.ReportStats();
        }

        protected void UpdateInterface()
        {
            UpdateCurrentStat();

            foreach (var i in currentStat)
            {
                // TODO: Implement update rather than rebuild
            }
        }

        protected void RebuildInterface()
        {
            WipeInterface();
            UpdateCurrentStat();
            SpawnStatFieldsFromList();
        }

        private void WipeInterface()
        {
            for (int i = statFields.Count - 1; i >= 0; i--)
            {
                Destroy(statFields[i].gameObject);
                statFields.Remove(statFields[i]);
            }
        }

        private void SpawnStatFieldsFromList()
        {
            foreach (var i in currentStat)
            {
                SpawnStatField(i.Item1, i.Item2);
            }
        }

        private void SpawnStatField(string property, string value)
        {
            var newInfoField = Instantiate(uiStatsFieldPrefab);
            newInfoField.transform.SetParent(statFieldsContainer);
            newInfoField.Initialize(property, value);
            statFields.Add(newInfoField);
        }
    }
}