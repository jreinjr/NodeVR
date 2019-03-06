using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;
using TMPro;
namespace NodeVR
{
    public class UIUpgradePanel : MonoBehaviour
    {
        public TextMeshProUGUI headerTextUGUI;
        public Transform optionButtonsContainer;
        public Button optionButtonPrefab;

        private List<Button> optionButtons;

        public string headerText;

        public string upgradeButtonText;
        public UnityAction UpgradeButtonClicked;

        private void Start()
        {
            Initialize();
            NodeMapController.SelectableSelected += OnSelectableSelected;
        }

        private void Initialize()
        {
            headerTextUGUI.text = headerText;

            optionButtons = optionButtonsContainer.GetComponentsInChildren<Button>().ToList();
            foreach (Button b in optionButtons)
            {
                Text buttonText = b.GetComponentInChildren<Text>();
                buttonText.text = upgradeButtonText;

                UpgradeButtonClicked += OnUpgradeButtonClicked;
                b.onClick.AddListener(UpgradeButtonClicked);
            }
        }

        private void OnUpgradeButtonClicked()
        {
            Debug.Log("Clicked");
        }


        protected void OnSelectableSelected(Selectable selectable)
        {
            WipeInterface();
            var actionComponent = selectable.gameObject.GetComponent<IHasUpgrades>();

            if (actionComponent != null)
            {
                UpdateInterface(actionComponent as IHasUpgrades);
            }
        }

        protected void UpdateInterface(IHasUpgrades hasActions)
        {
            var newActions = hasActions.ReportAvailableUpgrades();
            SpawnOptionButtonsFromList(newActions);
        }

        private void WipeInterface()
        {
            for (int i = 0; i < optionButtonsContainer.childCount; i++)
            {
                Destroy(optionButtonsContainer.GetChild(i).gameObject);
            }
        }

        private void SpawnOptionButtonsFromList(List<(string, UnityAction)> actions)
        {
            foreach (var a in actions)
            {
                SpawnOptionButton(a.Item1, a.Item2);
            }
        }

        private void SpawnOptionButton(string label, UnityAction action)
        {
            var newOptionButton = Instantiate(optionButtonPrefab);
            newOptionButton.transform.SetParent(optionButtonsContainer);

            newOptionButton.onClick.AddListener(action);

            var newLabelText = newOptionButton.GetComponentInChildren<Text>();
            newLabelText.text = label;
        }
    }
}