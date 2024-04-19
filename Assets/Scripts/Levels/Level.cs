using System;
using System.Collections.Generic;
using UnityEngine;

namespace Levels
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private ParticleSystem foundParticle;
        
        public event Action Completed;
        public event Action Closed;
        
        private readonly List<Difference> _differencesA = new List<Difference>();
        private readonly List<Difference> _differencesB = new List<Difference>();
        
        private int _totalDifferences;
        private int _foundDifferences;
        
        public void Init()
        {
            ConnectDifferences();
            _totalDifferences = _differencesA.Count;
        }
        
        public void Reset()
        {
            Closed?.Invoke();
            Closed = null;
            _differencesA.Clear();
            _differencesB.Clear();
            _foundDifferences = 0;
        }

        private void ConnectDifferences()
        {
            for (int i = 0; i < _differencesA.Count; i++)
            {
                _differencesA[i].SetConnectedDifference(_differencesB[i]);
                _differencesB[i].SetConnectedDifference(_differencesA[i]);
            }
        }

        public void AddDifference(Difference difference, bool groupA)
        {
            if (groupA)
            {
                _differencesA.Add(difference);
            }
            else
            {
                _differencesB.Add(difference);
            }
        }
        
        public void FoundDifference(Difference difference)
        {
            difference.Disable();
            Instantiate(foundParticle, difference.transform.position, Quaternion.identity);
            
            difference.GetConnectedDifference().Disable(); 
            Instantiate(foundParticle, difference.GetConnectedDifference().transform.position, Quaternion.identity);
            
            _foundDifferences++;
            
            if (_foundDifferences == _totalDifferences)
            {
                Completed?.Invoke();
            }
        }

        private void OnDestroy()
        {
            Closed?.Invoke();
            Closed = null;
        }
    }
}