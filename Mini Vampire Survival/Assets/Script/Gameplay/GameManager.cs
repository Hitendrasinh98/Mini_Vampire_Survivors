using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] int PlayerMaxHealth = 100;
        [SerializeField] int StartXpLevel = 1;
        [SerializeField] float MoveSpeed = 7;
        [SerializeField] int targetSurviveTime = 300; //InSeconds
        [SerializeField] WeaponSystem.WeaponTypeEnum primaryWeapon;


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
            gameStartData.MaxHealth = PlayerMaxHealth;
            gameStartData.XPLevel = StartXpLevel;
            gameStartData.MoveSpeed = MoveSpeed;
            gameStartData.primaryWeapon = primaryWeapon;

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
            Core.EventManager.Instance.OnGameComeplete?.Invoke();
            UISystem.UIManager.Instance.HidePage(UISystem.UIPageIDEnum.GameHud);
            UISystem.UIManager.Instance.ShowPage(UISystem.UIPageIDEnum.Result);
        }

        void OnSurvieTimeIncrease(int totalSeconds)
        {
            if(totalSeconds >= targetSurviveTime)
            {
                Core.EventManager.Instance.OnGameComeplete?.Invoke();
                UISystem.UIManager.Instance.HidePage(UISystem.UIPageIDEnum.GameHud);
                UISystem.UIManager.Instance.ShowPage(UISystem.UIPageIDEnum.Result);
            }
        }

    }
}