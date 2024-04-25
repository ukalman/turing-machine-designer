using System;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.TMExecutionPanel
{
    public class TMExecutionPanelController : MonoBehaviour
    {
        [SerializeField] protected TMP_InputField inputField;

        [SerializeField] protected Button executeButton;

        [SerializeField] protected Button scanButton;

        [SerializeField] protected TMP_Text acceptRejectText;

        private void Start()
        {
            acceptRejectText.text = "";
            TMSignals.Instance.OnTMHalted += OnTMHalted;
        }

        private void OnDestroy()
        {
            TMSignals.Instance.OnTMHalted -= OnTMHalted;
        }


        public void TMSetInputString()
        {
            acceptRejectText.text = "";
            TMSignals.Instance.OnTMInputStringSet?.Invoke(inputField.text);
        }

        private void OnTMHalted(bool returnValue)
        {
            if (returnValue)
            {
                acceptRejectText.text = "Input Accepted!";
            }
            else
            {
                acceptRejectText.text = "Input Rejected!";
            }
            
            
        }
        
        
    }
}