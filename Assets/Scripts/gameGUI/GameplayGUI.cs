using System.Collections.Generic;
using GameSession;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace gameGUI
{
    public class GameplayGUI : MonoBehaviour
    {
        [Header("Loading")]
        [SerializeField] private GameObject loadingPanel;
        
        [Header("Timer")]
        [SerializeField] private TextMeshProUGUI timerText;
        
        [Header("Level")]
        [SerializeField] private TextMeshProUGUI levelText;
        
        [Header("Level End Panels")]
        [SerializeField] private GameObject levelCompletedPanel;
        [SerializeField] private GameObject levelFailedPanel;
        
        [Header("Restart Buttons")]
        [SerializeField] private List<Button> restartButtons;
        
        
        private bool _isInitialized;
        public void Init(GameSessionController gameSessionController,  GameTimer gameTimer, AdManager adManager)
        {
            if (_isInitialized) return;
            
            HideLevelEndPanels();
            ShowLoadingPanel();
            
            gameSessionController.LevelEnded += (level, success) =>
            {
                ShowLevelEndPanel(success);
                UpdateLevel(level);
            };
            
            gameSessionController.LevelStarted += (level) =>
            {
                UpdateLevel(level);
                HideLoadingPanel();
            };
            
            gameTimer.TimeChanged += DisplayTime;
            
            

            foreach (var b in restartButtons)
            {
                b.onClick.AddListener(OnRestart);
                continue;

                async void OnRestart()
                {
                    HideLevelEndPanels();
                    ShowLoadingPanel();
                    await adManager.ShowAddsOnRestart();
                    gameSessionController.StartLevel();
                }
            }
            
            _isInitialized = true;
        }
        
        void DisplayTime(float timeToDisplay)
        {
            float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        
        private void ShowLevelEndPanel(bool success)
        {
            if (success)
            {
                levelCompletedPanel.SetActive(true);
            }
            else
            {
                levelFailedPanel.SetActive(true);
            }
        }
        
        private void HideLevelEndPanels()
        {
            levelCompletedPanel.SetActive(false);
            levelFailedPanel.SetActive(false);
        }
        
        private void ShowLoadingPanel()
        {
            loadingPanel.SetActive(true);
        }
        
        private void HideLoadingPanel()
        {
            loadingPanel.SetActive(false);
        }
        
        private void UpdateLevel(int progressDataLevel)
        {
            levelText.text = $"Уровень: {progressDataLevel}";
        }
    }
}