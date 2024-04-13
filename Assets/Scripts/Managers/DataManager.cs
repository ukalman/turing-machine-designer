using System;
using Data;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;


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

        private void OnEnable()
        {
            // Subscribe to signals here
        }

        private void Start()
        {
            
        }

        private void OnDestroy()
        {
         
        }

        private void OnTMPreferencesCompleted()
        {
            // Saving a value
            PlayerPrefs.SetInt("StateCount", mainData.StateCount);
            PlayerPrefs.SetString("InputSymbols", string.Join(",", mainData.InputSymbols));
            PlayerPrefs.SetString("TapeSymbols", string.Join(",", mainData.TapeSymbols));

            /*
            // Loading a value
            stateCount = PlayerPrefs.GetInt("StateCount", defaultValue);
            inputSymbols = new HashSet<char>(PlayerPrefs.GetString("InputSymbols", "").ToCharArray());
            */
            
        }
        
        
       
    }
}