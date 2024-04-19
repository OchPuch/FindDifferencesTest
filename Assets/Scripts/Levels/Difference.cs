using System;
using UnityEngine;

namespace Levels
{
    public class Difference : MonoBehaviour
    {
        private Level _level;
        private Difference _connectedDifference;
        private CircleCollider2D _collider;
        
        public void Init(Level level, CircleCollider2D col, SpriteRenderer sr)
        {
            _level = level;
            _collider = col;
            _level.AddDifference(this, sr.enabled);
        }

        private void OnMouseDown()
        {
            if (_connectedDifference is null) return;
            _level.FoundDifference(this);
        }
        
        public void SetConnectedDifference(Difference difference)
        {
            _connectedDifference = difference;
        }
        
        public Difference GetConnectedDifference()
        {
            return _connectedDifference;
        }

        public void Disable()
        {
            _collider.enabled = false;
        }
    }
}