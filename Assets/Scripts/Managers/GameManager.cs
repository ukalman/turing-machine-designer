using System;
using Enums;
using Extensions;

using UnityEngine;
using UnityEngine.Events;


namespace Managers
{
    
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        
        private GameStates _currentGameState = GameStates.None;
        public GameStates CurrentGameState => _currentGameState;

        public Transform GameFrame;
        public GameObject TMPrefab;
        
        
        public UnityAction OnGameStart;
        public UnityAction OnTMDesign;
        public UnityAction OnTMStateRules;
        public UnityAction OnTMStartExecution;
        public UnityAction OnLevelPrep;
        public UnityAction OnGameplay;
        public UnityAction OnLevelEnd;
        public UnityAction OnPause;
        public UnityAction OnGameWin;
        public UnityAction OnGameLose;
        
        
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
        

        private void Start()
        {
            OnTMStartExecution += () => { Instantiate(TMPrefab, GameFrame);};
            ChangeGameState(GameStates.Start); 
        }
        
        public void ChangeGameState(GameStates newGameState)
        {
            if (_currentGameState == newGameState)
                return; 

            _currentGameState = newGameState;

            switch (newGameState)
            {
                case GameStates.None:
                    break;
                case GameStates.Start:
                    OnGameStart?.Invoke();
                    break;
                case GameStates.TMDesign:
                    OnTMDesign?.Invoke();
                    break;
                case GameStates.TMStateRules:
                    OnTMStateRules?.Invoke();
                    break;
                case GameStates.TMStartExecution:
                    OnTMStartExecution?.Invoke();
                    break;
                case GameStates.LevelPrep:
                    OnLevelPrep?.Invoke();
                    break;
                case GameStates.Gameplay:
                    OnGameplay?.Invoke();
                    break;
                case GameStates.Win:
                    OnGameWin?.Invoke();
                    break;
                case GameStates.Lose:
                    OnGameLose?.Invoke();
                    break;
                case GameStates.LevelEnd:
                    OnLevelEnd?.Invoke();
                    break;
                case GameStates.Pause:
                    OnPause?.Invoke();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newGameState), newGameState, null);
            }

            Debug.Log($"[GameManager]: Game state changed to: {newGameState}");
        }
        
        public void StartGame() => ChangeGameState(GameStates.Start);
        public void StartTMDesign() => ChangeGameState(GameStates.TMDesign);
        public void StartTMStateRules() => ChangeGameState(GameStates.TMStateRules);
        public void StartTMExecution() => ChangeGameState(GameStates.TMStartExecution);
        public void PrepLevel() => ChangeGameState(GameStates.LevelPrep);
        public void StartGameplay() => ChangeGameState(GameStates.Gameplay);
        public void WinGame() => ChangeGameState(GameStates.Win);
        public void LoseGame() => ChangeGameState(GameStates.Lose);
        public void EndLevel() => ChangeGameState(GameStates.LevelEnd);
        public void PauseGame() => ChangeGameState(GameStates.Pause);
        public void ResumeGame() => ChangeGameState(GameStates.Gameplay);
        
        
        /*private void OnGUI()
        {
            GUIStyle guiStyle = new GUIStyle(GUI.skin.label)
            {
                fontSize = 10,
                fontStyle = (UnityEngine.FontStyle)FontStyle.Bold
            };

            Color textColor = Color.red;

            guiStyle.normal.textColor = textColor;

            GUI.Label(new Rect(30, 80, 300, 150), "CurrentLevel: " + DataManager.Instance.mainData.currentLevel, guiStyle);
            GUI.Label(new Rect(30, 120, 300, 150), "CurrentLevelPiece: " + DataManager.Instance.mainData.currentLevelPiece, guiStyle);
            GUI.Label(new Rect(30, 160, 300, 150), "WordLength: " + DataManager.Instance.mainData.currentWordLength, guiStyle);
            GUI.Label(new Rect(30, 200, 300, 150), "Health: " + DataManager.Instance.mainData.currentHealth, guiStyle);
        }*/
    }
}