using UnityEngine;

namespace Levels.Building.ObjectSearch
{
    public class DifferencesParentNameSearcher : IDifferencesParentSearcher
    {
        private readonly string _differencesParentName = "Отличия";
        public DifferencesParentNameSearcher(string differencesParentName = null)
        {
            if (!string.IsNullOrEmpty(differencesParentName)) _differencesParentName = differencesParentName;
        }
        
        public GameObject SearchDifferencesParent(GameObject levelInstance)
        {
            return levelInstance.transform.Find(_differencesParentName)?.gameObject;
        }
    }
}