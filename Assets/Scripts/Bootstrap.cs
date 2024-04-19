using gameGUI;
using GameSession;
using Levels;
using UnityEngine;
using UnityPurchasing;

public class Bootstrap : MonoBehaviour
{
    [Header("Gameplay")]
    [SerializeField] private LevelDirector levelDirector;
    [SerializeField] private GameTimer gameTimer;
    [SerializeField] private GameSessionController gameSessionController;
    [Header("GUI")]
    [SerializeField] private  GameplayGUI gameplayGUI;
    [Header("Monetization")]
    [SerializeField] private AdManager adManager;
    [SerializeField] private BuyManager buyManager;
    
    
    public void Awake()
    {
        adManager.Init();
        buyManager.Init();
        
        buyManager.BonusTimeBought += gameTimer.AddBonusTime;
        buyManager.OpenedBuyWindow += gameTimer.PauseTimer;
        buyManager.ClosedBuyWindow += gameTimer.ResumeTimer;
        gameTimer.Init();
        
        levelDirector.Init();
        
        gameSessionController.Init(gameTimer, levelDirector);
        
        gameplayGUI.Init(gameSessionController, gameTimer, adManager);
        
        gameSessionController.StartLevel();
    }
    
}