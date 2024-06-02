using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] ConfigData.SO_PlayerConfig soPlayerConfig;


        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            StartGame();
        }


        void StartGame()
        {
            Core.EventManager.GameStartData gameStartData = new Core.EventManager.GameStartData();
            gameStartData.MaxHealth = soPlayerConfig.PlayerMaxHealth;
            gameStartData.XPLevel = soPlayerConfig.StartXpLevel;
            gameStartData.MoveSpeed = soPlayerConfig.MoveSpeed;
            gameStartData.primaryWeapon = soPlayerConfig.primaryWeapon;

            UISystem.UIManager.Instance.ShowPage(UISystem.UIPageIDEnum.GameHud);
            Core.EventManager.Instance.OnGameStart?.Invoke(gameStartData);
            StatesSystem.StatesManager.Instance.AddObserver_OnSurviveTimeIncrease(OnSurvieTimeIncrease);
        }

        private void OnDestroy()
        {
            StatesSystem.StatesManager.Instance.RemoveObserver_OnSurviveTimeIncrease(OnSurvieTimeIncrease);
        }

        public void OnPlayerDied()
        {
            StatesSystem.StatesManager.Instance.Set_ResultStatus(false);
            Core.EventManager.Instance.OnGameComeplete?.Invoke();
            UISystem.UIManager.Instance.HidePage(UISystem.UIPageIDEnum.GameHud);
            UISystem.UIManager.Instance.ShowPage(UISystem.UIPageIDEnum.Result);
        }

        void OnSurvieTimeIncrease(int totalSeconds)
        {
            if(totalSeconds >= soPlayerConfig.targetSurviveTime)
            {
                StatesSystem.StatesManager.Instance.Set_ResultStatus( true);
                Core.EventManager.Instance.OnGameComeplete?.Invoke();
                UISystem.UIManager.Instance.HidePage(UISystem.UIPageIDEnum.GameHud);
                UISystem.UIManager.Instance.ShowPage(UISystem.UIPageIDEnum.Result);
            }
        }

    }
}