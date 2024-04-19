using UnityEngine;

namespace Levels.Building.ObjectSearch
{
    public class BackgroundNameSearcher : IBackgroundSearcher
    {
        readonly string _backgroundName = "Фон";
        public BackgroundNameSearcher(string backgroundName = null)
        {
            if (!string.IsNullOrEmpty(backgroundName)) _backgroundName = backgroundName;
        }
        
        public SpriteRenderer SearchBackground(GameObject levelInstance)
        {
            return levelInstance.transform.Find(_backgroundName)?.GetComponent<SpriteRenderer>();
        }
    }
}