using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace NodeVR
{
    public class UIInfoField : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI propertyTextUGUI;
        public TextMeshProUGUI PropertyTextUGUI
        {
            get { return propertyTextUGUI; }
            protected set { propertyTextUGUI = value; }
        }

        [SerializeField] private TextMeshProUGUI valueTextUGUI;
        public TextMeshProUGUI ValueTextUGUI
        {
            get { return valueTextUGUI; }
            protected set { valueTextUGUI = value; }
        }

        private void Awake()
        {
            if (propertyTextUGUI == null)
                propertyTextUGUI = transform.Find("PropertyText").GetComponent<TextMeshProUGUI>();

            if (valueTextUGUI == null)
                valueTextUGUI = transform.Find("ValueText").GetComponent<TextMeshProUGUI>();

            if (propertyTextUGUI == null || valueTextUGUI == null)
                throw new System.Exception("UIInfoField is not property set up (prefab requires PropertyText and ValueText children)");
        }

        public void Initialize(string propertyText, string valueText)
        {
            propertyTextUGUI.text = propertyText;
            ValueTextUGUI.text = valueText;
        }
    }
}