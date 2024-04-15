using System.Collections;
using System.Collections.Generic;
using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Signals;

namespace UI.TMPreferencesPanel
{
    public class TMPreferencesPanelController : MonoBehaviour
    {

        [SerializeField] protected StateCounter stateCounter;
        [SerializeField] protected AlphabetManager alphabetManager;

        private int _stateCount;
        private HashSet<char> _inputSymbols;
        private HashSet<char> _tapeSymbols;
        
        
        // Input fieldlardan aldığın değeri DataManager'ın MainDatasındaki değerlere aktar.
       
        private void Start()
        {
            
        }

        private void OnEnable()
        {
           
        }
        

        private void OnDisable()
        {
           
        }

        // OnTMPreferencesCompleted signal'ini invoke et
        // Bu sinyalle input değişkenlerini yolla
        public void OnReadyPressed()
        {
            if (ValidateInputs())
            {
                Debug.Log("Inputs are Valid.");
                TMSignals.Instance.OnTMPreferencesDetermined?.Invoke(_stateCount, _inputSymbols, _tapeSymbols);
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

