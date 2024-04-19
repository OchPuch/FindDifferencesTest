using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
#endif 
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Levels.Data
{
    [CreateAssetMenu(fileName = "LevelsData", menuName = "LevelsData", order = 0)]
    public class LevelsData : ScriptableObject
    {
        public List<AssetReferenceGameObject> levelObjects;
    
        [Header("Source")]
        [SerializeField] private bool getAssetsFromThisFolder = true;
        [SerializeField] [HideIf("getAssetsFromThisFolder")]private string customFolder = "Assets/Levels";
#if UNITY_EDITOR
        [Space(10)]
        [Header("Addressables")]
        [Tooltip("Using default group if target group is not set.")]
        [SerializeField] private AddressableAssetGroup targetGroup;
        [SerializeField] private AssetLabelReference assetLabel;
#endif

        private void OnValidate()
        {
#if UNITY_EDITOR
            if (!getAssetsFromThisFolder)
                customFolder = AssetDatabase.GetAssetPath(this).Replace("/" + name + ".asset", "");
#endif
        }

        [Button("Add Levels From Folder")]
        private void AddLevelsFromFolder()
        {
#if UNITY_EDITOR
        
            var folder = getAssetsFromThisFolder ? AssetDatabase.GetAssetPath(this).Replace("/" + name + ".asset", "") : customFolder;
            var guids = AssetDatabase.FindAssets("t:GameObject", new[] {folder});
        
            var settings = AddressableAssetSettingsDefaultObject.Settings;
            var group = targetGroup is not null ? settings.FindGroup(targetGroup.Name) : settings.DefaultGroup;
        
            var entriesAdded = new List<AddressableAssetEntry>();
        
            foreach (var guid in guids)
            {
                var entry = settings.CreateOrMoveEntry(guid, group, readOnly: false, postEvent: false);
                entry.address = AssetDatabase.GUIDToAssetPath(guid);
                entry.labels.Add(assetLabel.labelString);
                entriesAdded.Add(entry);
            
                AssetReferenceGameObject assetReference = new AssetReferenceGameObject(guid);
                if (levelObjects.Find(x => x.AssetGUID == guid) is null) levelObjects.Add(assetReference);
            }
        
            foreach (AddressableAssetEntry entry in group.entries.Except(entriesAdded).ToArray()) group.RemoveAssetEntry(entry);
            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entriesAdded, true);
#endif
        }
    
    
    
    
    
    
    }
}