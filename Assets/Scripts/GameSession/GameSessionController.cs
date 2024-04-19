using System;
using Data;
using Levels;
using UnityEngine;

namespace GameSession
{
    public class GameSessionController : MonoBehaviour
    {
        public event Action<int> LevelStarted;
        public event Action<int, bool> LevelEnded; 
        private int _currentLevel;
        
        private LevelDirector _levelDirector;
        private ProgressSaver _progressSaver;
        
        private bool _isInitialized;
        
        public void Init(GameTimer gameTimer, LevelDirector levelDirector)
        {
            if (_isInitialized) return;
            
            _progressSaver = new ProgressSaver();
            LoadProgress(_progressSaver.LoadProgress());

            _levelDirector = levelDirector;
            
            LevelEnded += (level, success) =>
            {
                if (!success) return;
                _currentLevel++;
                _progressSaver.SaveProgress(GetProgressData());
            };
            
            LevelStarted += (int level) =>
            {
                gameTimer.ResetTimer();
                gameTimer.StartTimer();
            };
            
            gameTimer.TimeIsUp += () =>
            {
                LevelEnded?.Invoke(_currentLevel, false);
            };
            
            levelDirector.CurrentLevelCompleted += () =>
            {
                LevelEnded?.Invoke(_currentLevel, true);
            };
            
            _isInitialized = true;
        }
        
        public async void StartLevel()
        {
            await _levelDirector.LoadLevelAsync(_currentLevel);
            LevelStarted?.Invoke(_currentLevel);
        }
        private void LoadProgress(ProgressData progressData)
        {
            _currentLevel = progressData.level;
        }

        private ProgressData GetProgressData()
        {
            return new ProgressData
            {
                level = _currentLevel
            };
        }
    }
}