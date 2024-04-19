using System;
using gameGUI;
using UnityEngine;

namespace GameSession
{
    public class GameTimer : MonoBehaviour
    {
        public event Action<float> TimeChanged;
        public event Action<float> MaxTimeChanged;
        public event Action TimeIsUp;

        [SerializeField] [Min(0)] private float time;
        [SerializeField] [Min(0)] private float bonusTime;

        private float _maxTime;
        private float _currentTime;
        private bool _isRunning;
        
        private bool _isInitialized;
        public void Init()
        {
            if (_isInitialized) return;
            _maxTime = time;
            _isInitialized = true;
        }

        public void StartTimer()
        {
            _currentTime = 0;
            _isRunning = true;
            TimeChanged?.Invoke(_currentTime);
        }

        public void PauseTimer()
        {
            _isRunning = false;
        }

        public void ResumeTimer()
        {
            _isRunning = true;
        }

        public void ResetTimer()
        {
            _currentTime = 0;
            _isRunning = false;
            TimeChanged?.Invoke(_currentTime);
        }

        public void AddBonusTime()
        {
            _maxTime += bonusTime;
            MaxTimeChanged?.Invoke(_maxTime);
        }

        private void Update()
        {
            if (!_isRunning) return;
            _currentTime += Time.deltaTime;
            TimeChanged?.Invoke(_currentTime);
            if (_currentTime >= _maxTime)
            {
                TimeIsUp?.Invoke();
                _isRunning = false;
            }
        }

    }
}
