using System;
using System.Threading.Tasks;
using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using GameSession;
using UnityEngine;

namespace gameGUI
{
    /// <summary>
    /// Uses Appodeal SDK to show adds
    /// </summary>
    public class AdManager : MonoBehaviour
    {
        [Header("Appodeal")]
        [SerializeField] private string appKey;
        
        public void Init()
        {
            Appodeal.SetTesting(true);
            int adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo | AppodealAdType.Mrec;
            AppodealCallbacks.Sdk.OnInitialized += OnInitializationFinished;
            Appodeal.Initialize(appKey, adTypes);
        }

        private void OnInitializationFinished(object sender, SdkInitializedEventArgs e)
        {
            Debug.Log("Appodeal initialized");
        }

        
        public async Task ShowAddsOnRestart()
        {
            if (Appodeal.IsLoaded(AppodealAdType.Interstitial))
            {
                Appodeal.Show(AppodealAdType.Interstitial);
                bool isClosed = false;
                AppodealCallbacks.Interstitial.OnClosed += (sender, args) => isClosed = true;
                Appodeal.Show(AppodealAdType.Interstitial);
                await Task.Run(() =>
                {
                    while (!isClosed)
                    {
                        Task.Yield();
                    }
                });
            }
        }
    }
}
