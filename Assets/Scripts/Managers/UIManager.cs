using System;
using Enums;
using Extensions;
using Signals;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void OnEnable()
        {
          
            GameManager.Instance.OnGameStart += Play;
            GameManager.Instance.OnLevelPrep += OnLevelInitialize;
            
            GameManager.Instance.OnGameWin += OnGameWin;
            GameManager.Instance.OnGameLose += OnGameLose;
        }

        private void OnGameLose()
        {
            
        }

        private void OnGameWin()
        {
            
        }


        private void OnDisable()
        {
           
            GameManager.Instance.OnGameStart -= Play;
            GameManager.Instance.OnLevelPrep -= OnLevelInitialize;
            
            GameManager.Instance.OnGameWin -= OnGameWin;
            GameManager.Instance.OnGameLose -= OnGameLose;
        }

        private void OnLevelInitialize()
        {
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.TMPreferences, 0);
        }

        private void Play()
        {
            Debug.Log("UI Manager Play is called.");
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.MainMenu, 0);
        }
        
        
    }
}