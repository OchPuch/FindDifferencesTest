using System;
using UnityEngine;
using UnityEngine.UI;

namespace Levels.Building
{
    [Serializable]
    public class SceneLevelPlacer
    {
        [Header("Canvas references")] 
        [SerializeField] private CanvasScaler canvasRef;
        [SerializeField] private RectTransform levelRefA;
        [SerializeField] private RectTransform levelRefB;
        
        //Bounds is size of spriteRenderer background
        public void PlaceLevels(GameObject levelInstanceA, GameObject levelInstanceB, SpriteRenderer background)
        {
            if (levelInstanceA is null || levelInstanceB is null) return;
            
            Vector2 bounds = GetBoundsScreenSpace(background);
            
            PlaceLevel(levelInstanceA, bounds , levelRefA);
            PlaceLevel(levelInstanceB, bounds , levelRefB);
            
            ScaleLevel(levelInstanceA, bounds, levelRefA);
            ScaleLevel(levelInstanceB, bounds, levelRefB);
        }
        
        private Vector2 GetBoundsScreenSpace(SpriteRenderer background)
        {
            Vector2 screenBounds = Vector2.zero;
            if (Camera.main != null)
            {
                var position = background.transform.position;
                float screenX =
                    -Camera.main.WorldToScreenPoint(position - Vector3.right * background.bounds.extents.x).x
                    + Camera.main.WorldToScreenPoint(position + Vector3.right * background.bounds.extents.x).x;
                
                float screenY = 
                    -Camera.main.WorldToScreenPoint(position - Vector3.up * background.bounds.extents.y).y
                    + Camera.main.WorldToScreenPoint(position + Vector3.up * background.bounds.extents.y).y;
                
                screenBounds = new Vector2(screenX, screenY);
            }
            
            
            return screenBounds;
        }
        
        private void PlaceLevel(GameObject levelInstance, Vector2 bounds, RectTransform levelRef)
        {
            if (Camera.main != null)
            {
                Vector3 position = (levelRef.position);
                position = Camera.main.ScreenToWorldPoint(position);
                position.z = 0;
                
                levelInstance.transform.position = position;
            }
        }
        
        private void ScaleLevel(GameObject levelInstance, Vector2 bounds, RectTransform levelRef)
        {
            if (canvasRef != null)
            {
                Vector2 convertedBounds = new Vector2(bounds.x/Screen.width, bounds.y/Screen.height);
                Vector2 convertedRefSize = new Vector2(levelRef.rect.width/canvasRef.referenceResolution.x, levelRef.rect.height/canvasRef.referenceResolution.y);
                Vector2 ratio = new Vector2(convertedBounds.x / convertedRefSize.x, convertedBounds.y / convertedRefSize.y);
                
                float weightedRatio = Mathf.Lerp(ratio.x, ratio.y, canvasRef.matchWidthOrHeight);
                
                levelInstance.transform.localScale = Vector3.one / weightedRatio;
            }
        }
        
        
        
    }
}