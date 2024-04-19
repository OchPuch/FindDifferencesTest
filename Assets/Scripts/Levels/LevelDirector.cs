using System;
using System.Threading.Tasks;
using Levels.Building;
using Levels.Data;
using UnityEngine;

namespace Levels
{
    public class LevelDirector : MonoBehaviour
    {
        [Header("Data")]
        [SerializeField] private LevelsData levelsData;
        [Header("Building")]
        [SerializeField] private Level level;
        [SerializeField] private LevelBuilder levelBuilder;
        private LevelObjectsFactory _levelObjectsFactory;

        public event Action CurrentLevelCompleted;
        
        private bool _isInitialized;
        
        public void Init()
        {
            if (_isInitialized) return;
            _levelObjectsFactory = new LevelObjectsFactory(level);
            level.Completed += OnLevelCompleted;
            _isInitialized = true;
        }
        
        public async Task LoadLevelAsync(int levelIndex)
        {
            level.Reset();
            
            int index = levelIndex % levelsData.levelObjects.Count;
            
            var handleA = _levelObjectsFactory.CreateLevelObject(levelsData.levelObjects[index]);
            var handleB = _levelObjectsFactory.CreateLevelObject(levelsData.levelObjects[index]);
            
            await Task.WhenAll(handleA, handleB);
            
            levelBuilder.Build(level, handleA.Result, handleB.Result);
            
            level.Init();
        }

        private void OnLevelCompleted()
        {
            CurrentLevelCompleted?.Invoke();
        }
    }
}