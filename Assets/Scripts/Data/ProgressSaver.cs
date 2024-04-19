using System;
using UnityEngine;

namespace Data
{
    //Лучше использовать бинарный формат для сохранения прогресса и шифровать его, чтобы нельзя было его изменить
    public class ProgressSaver 
    {
        public void SaveProgress(ProgressData progressData)
        {
            string json = JsonUtility.ToJson(progressData);
            System.IO.File.WriteAllText("progress.json", json);
        }

        public ProgressData LoadProgress()
        {
            try
            {
                string json = System.IO.File.ReadAllText("progress.json");
                return JsonUtility.FromJson<ProgressData>(json);
            }
            catch (Exception)
            {
                Debug.LogWarning("Progress file not found");
                return new ProgressData();
            }
        }
    }
}