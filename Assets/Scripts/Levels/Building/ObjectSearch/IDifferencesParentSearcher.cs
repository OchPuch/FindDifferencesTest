using UnityEngine;

namespace Levels.Building.ObjectSearch
{
    public interface IDifferencesParentSearcher
    {
        GameObject SearchDifferencesParent(GameObject levelInstance);
    }
}