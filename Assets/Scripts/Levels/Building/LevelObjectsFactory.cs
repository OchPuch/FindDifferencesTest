using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Levels.Building
{
    public class LevelObjectsFactory
    {
        private readonly Level _parentLevel;
        
        
        public LevelObjectsFactory(Level parentLevel)
        {
            _parentLevel = parentLevel;
        }
        
        public async Task<GameObject> CreateLevelObject(AssetReferenceGameObject levelObject, Vector3 position = default)
        {
            if (levelObject is null) throw new ArgumentNullException(nameof(levelObject), "Level object is null");
            
            var handle = Addressables.InstantiateAsync(levelObject, position, Quaternion.identity);
            await handle.Task;
            var levelInstance = handle.Result;
            levelInstance.transform.SetParent(_parentLevel.transform);
            
            _parentLevel.Closed += () =>
            {
                levelObject.ReleaseInstance(levelInstance);
            };
            
            return levelInstance;
        }

       
    }
}