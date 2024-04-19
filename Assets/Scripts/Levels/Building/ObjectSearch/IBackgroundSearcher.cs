using UnityEngine;

namespace Levels.Building.ObjectSearch
{
    public interface IBackgroundSearcher
    {
        public SpriteRenderer SearchBackground(GameObject levelInstance);
    }
}