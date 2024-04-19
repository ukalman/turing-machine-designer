using System;
using System.Collections.Generic;
using Data;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Signals;
using Unity.VisualScripting;
using TM;
using State = TM.State;
using Enums;


namespace Managers
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }
        public MainData mainData;
        public List<State> States; // Every state has its transition functions
        
        
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

        private void SetupStates()
        {
            States = new List<State>();
            // Create normal states based on the state count
            for (int i = 0; i < mainData.StateCount; i++)
            {
                State newState = new State($"q{i}") { Type = StateType.Normal };
                States.Add(newState);
            }
            
            // Add special states: accept and reject
            States.Add(new State("qaccept") { Type = StateType.Accept }); // Accept state
            States.Add(new State("qreject") { Type = StateType.Reject }); // Reject state
        }

        private void OnTMPreferencesDetermined(int stateCount, HashSet<char> inputSymbols, HashSet<char> tapeSymbols)
        {
            mainData.StateCount = stateCount;
            mainData.InputSymbols = inputSymbols;
            mainData.TapeSymbols = tapeSymbols;
            
            SetupStates();
            
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