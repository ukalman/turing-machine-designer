using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Signals;
using UnityEngine.UI;

namespace UI.TMPreferencesPanel
{
    public class TMPreferencesPanelController : MonoBehaviour
    {

        [SerializeField] protected StateCounter stateCounter;
        [SerializeField] protected AlphabetManager alphabetManager;
        [SerializeField] protected Button readyButton;
        
        private int _stateCount;
        private HashSet<char> _inputSymbols;
        private HashSet<char> _tapeSymbols;
        
        
        
        
        // Input fieldlardan aldığın değeri DataManager'ın MainDatasındaki değerlere aktar.
       
        private void Start()
        {
            
        }

        private void Update()
        {
            UpdateButtonInteractivity();
        }

        private void OnEnable()
        {
           
        }
        

        private void OnDisable()
        {
           
        }

        private void UpdateButtonInteractivity()
        {
            readyButton.interactable = ValidateInputs();
        }
        

        // OnTMPreferencesCompleted signal'ini invoke et
        // Bu sinyalle input değişkenlerini yolla
        public void OnReadyPressed()
        {
            if (ValidateInputs())
            {
                _stateCount = stateCounter.StateCount;
                _inputSymbols = alphabetManager.InputSymbols;
                _tapeSymbols = alphabetManager.TapeSymbols;
                Debug.Log("Inputs are Valid.");
                Debug.Log("state count: " + _stateCount);
                Debug.Log("input symbols: " + _inputSymbols);
                Debug.Log("tape symbols: " + _tapeSymbols);
                TMSignals.Instance.OnTMPreferencesDetermined?.Invoke(_stateCount, _inputSymbols, _tapeSymbols);
                GameManager.Instance.StartTMStateRules();
            }
            else
            {
                Debug.Log("Inputs are not valid!");
            }
                
        }

        private bool ValidateInputs()
        {
            return alphabetManager.AreAlphabetsValid() && stateCounter.IsStateCountValid();
        }
        
        
    }
   
}

