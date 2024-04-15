using Enums;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class MainMenuPanelController : MonoBehaviour
    {
        [BoxGroup("Ui References")][SerializeField] Button settingsButton;
        
        [BoxGroup("Ui References")][SerializeField] Button playButton;

        private void Start()
        {
            //settingsButton.onClick.AddListener(Pause);
            playButton.onClick.AddListener(Play);
        }

        private void Pause()
        {
            GameManager.Instance.PauseGame();
            CoreUISignals.Instance.OnOpenPanel?.Invoke(UIPanelTypes.Settings, 1);
        }

        public void Play()
        {
            Debug.Log("Play button clicked");
            //GameManager.Instance.PrepLevel();
            GameManager.Instance.StartTMDesign();
        }
        
    }
}