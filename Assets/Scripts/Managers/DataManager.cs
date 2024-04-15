using System;
using System.Collections.Generic;
using Data;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Signals;


namespace Managers
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        public MainData mainData;
        
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        

        private void Start()
        {
            TMSignals.Instance.OnTMPreferencesDetermined += OnTMPreferencesDetermined;
        }

        private void OnDisable()
        {
            TMSignals.Instance.OnTMPreferencesDetermined -= OnTMPreferencesDetermined;
        }

        private void OnTMPreferencesDetermined(int stateCount, HashSet<char> inputSymbols, HashSet<char> tapeSymbols)
        {
            mainData.StateCount = stateCount;
            mainData.InputSymbols = inputSymbols;
            mainData.TapeSymbols = tapeSymbols;
            
            //SavePrefs();
            
            
        }

        private void SavePrefs()
        {
            // Saving a value
            PlayerPrefs.SetInt("StateCount", mainData.StateCount);
            PlayerPrefs.SetString("InputSymbols", string.Join(",", mainData.InputSymbols));
            PlayerPrefs.SetString("TapeSymbols", string.Join(",", mainData.TapeSymbols)); 
        }

        private void LoadPrefs()
        {
            /*
            // Loading a value
            stateCount = PlayerPrefs.GetInt("StateCount", defaultValue);
            inputSymbols = new HashSet<char>(PlayerPrefs.GetString("InputSymbols", "").ToCharArray());
            */
            
        }
        
        
       
    }
}