using System;
using Levels.Building.ObjectSearch;
using UnityEngine;

namespace Levels.Building
{
    [Serializable]
    public class LevelBuilder
    {
        [SerializeField] private float colliderSizeMultiplier = 1f;
        [SerializeField] private SceneLevelPlacer sceneLevelPlacer;
        
        private IBackgroundSearcher _backGroundSearch = new BackgroundNameSearcher("Фон");
        private IDifferencesParentSearcher _differencesParentSearch = new DifferencesParentNameSearcher("Отличия");

        public bool Build(Level level, GameObject levelInstanceA, GameObject levelInstanceB)
        {
            if (levelInstanceA is null || levelInstanceB is null || level is null) return false;
            var backgroundA = _backGroundSearch.SearchBackground(levelInstanceA);
            var backgroundB = _backGroundSearch.SearchBackground(levelInstanceB);
            if (backgroundA is null) return false;

            Vector3 backGroundOffset = backgroundA.transform.localPosition;
            backgroundA.transform.localPosition = Vector3.zero;
            backgroundB.transform.localPosition = Vector3.zero;
            
            if (!BuildInstance(levelInstanceA, level, false, backGroundOffset)) return false;
            if (!BuildInstance(levelInstanceB, level, true, backGroundOffset)) return false;
            
            sceneLevelPlacer.PlaceLevels(levelInstanceA, levelInstanceB, backgroundA);
            return true;
        }
        
        private bool BuildInstance(GameObject levelInstance, Level level, bool disableSr, Vector3 offset = default)
        {
            var differencesParent = _differencesParentSearch.SearchDifferencesParent(levelInstance);
            if (differencesParent is null) return false;
            
            differencesParent.transform.localPosition -= offset;
            
            foreach (Transform child in differencesParent.transform)
            {
                if (child == differencesParent.transform) continue;
                
                var sr = child.GetComponent<SpriteRenderer>();
                if (sr is null) continue;
                sr.enabled = !disableSr;
                
                var col = child.gameObject.AddComponent<CircleCollider2D>();
                col.radius = Mathf.Max(sr.bounds.extents.x, sr.bounds.extents.y);
                col.radius *= colliderSizeMultiplier;
                
                var dif = child.gameObject.AddComponent<Difference>();
                
                dif.Init(level, col, sr);
            }
            
            return true;
        }
    }
    
    
}
