using System;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace UnityPurchasing
{
    public class BuyManager : MonoBehaviour
    {
        public event Action OpenedBuyWindow;
        public event Action ClosedBuyWindow;
        
        [SerializeField] private string environment;
        public async void Init() {
            // try {
            //     var options = new InitializationOptions()
            //         .SetEnvironmentName(environment);
            //
            //     await UnityServices.InitializeAsync(options);
            // }
            // catch (Exception exception) {
            //     // An error occurred during initialization.
            //     Debug.LogError($"An error occurred during initialization: {exception.Message}");
            // }
        }
        
        public void OpenBuyWindow()
        {
            OpenedBuyWindow?.Invoke();
        }
        
        public void CloseBuyWindow()
        {
            ClosedBuyWindow?.Invoke();
        }
        
        public event Action BonusTimeBought; 
        public void BuyBonusTime()
        {
            BonusTimeBought?.Invoke();
        }
    }
}
