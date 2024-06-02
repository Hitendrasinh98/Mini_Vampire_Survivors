using Mini_Vampire_Surviours.Gameplay.ConfigData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.LevelUpSystem
{

    public class XPLevelManager : MonoBehaviour
    {
        [SerializeField] ConfigData.So_XpLevelConfig so_XpLevelConfig;
        [SerializeField] XpGem prefab_XpGem;
       

        [Header("Current Progress")]
        [SerializeField] int currentXPGems;
        [SerializeField] int requiredXpGems;
        [field: SerializeField] public int currentLevel { get; private set; }
        [field: SerializeField] public bool isMaxedOut { get; private set; }

        
        
        
        private void Awake()
        {
            Core.EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
        }

        void OnGameStart( Core.EventManager.GameStartData gameStartData)
        {
            currentLevel = gameStartData.XPLevel;
            Set_NextXpLevel();
            Core.EventManager.Instance.OnXPGemCollect?.Invoke(currentXPGems, requiredXpGems);
        }

        void Set_NextXpLevel()
        {
            if (currentLevel + 1 < so_XpLevelConfig.levelConfigs.Count)
                requiredXpGems = so_XpLevelConfig.levelConfigs[currentLevel + 1].xpGemsRequired;
            else
            {
                isMaxedOut = true;
                requiredXpGems = 0;
            }
        }


        public void SpawnXpGems(Vector2 spawnPos, XPGemTypeEnum xpGemType)
        {
            if (isMaxedOut)
                return;

            GemSpawnConfig data = Get_XpGemSpawnData(xpGemType);
            int spawnAmount = data.spawnAmount;
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector2 randomPos = Random.insideUnitCircle * data.spawnRadius;
                Vector2 ramdomSpawnPos = new Vector3(spawnPos.x + randomPos.x, spawnPos.y + randomPos.y);
                XpGem gem = Instantiate(prefab_XpGem, ramdomSpawnPos, Quaternion.identity);
                gem.Init(xpGemType);
            }
        }

        public void OnCollectGem(XPGemTypeEnum xPGemType)
        {
            if (isMaxedOut)
                return;


            int gemAmount = Get_XpGemSpawnData(xPGemType).gemAmountPerItem;            
            currentXPGems += gemAmount;
            Core.EventManager.Instance.OnXPGemCollect?.Invoke(currentXPGems, requiredXpGems);
            CheckForLevelUp();
        }

        void CheckForLevelUp()
        {
            if (currentXPGems >= requiredXpGems)
            {
                currentLevel++;
                currentXPGems -= requiredXpGems;
                Set_NextXpLevel();
                Set_UI();
                Core.EventManager.Instance.OnXpLevelUP?.Invoke(currentLevel);
                Core.EventManager.Instance.OnXPGemCollect?.Invoke(currentXPGems, requiredXpGems);
            }
        }
        void Set_UI()
        {
            UISystem.Panel_LevelUp panel_LevelUp = (UISystem.Panel_LevelUp)UISystem.UIManager.Instance.Get_UIPage(UISystem.UIPageIDEnum.LevelUp);
            if(panel_LevelUp== null)
            {
                Debug.LogError("Failed to get the PanelLeveluP refrence");
                return;
            }

            panel_LevelUp.Set_UI(so_XpLevelConfig.levelUpPowerConfig);
            UISystem.UIManager.Instance.ShowPage(UISystem.UIPageIDEnum.LevelUp);
            Time.timeScale = 0;

        }


        GemSpawnConfig Get_XpGemSpawnData(XPGemTypeEnum xPGemType)
        {
            for (int i = 0; i < so_XpLevelConfig.xpGemTypeConfig.Count; i++)
            {
                if (so_XpLevelConfig.xpGemTypeConfig[i].XpGemType == xPGemType)
                {
                    return so_XpLevelConfig.xpGemTypeConfig[i];
                }
            }
            Debug.LogError("Didnt  find the xpGem Config for " + xPGemType);
            return so_XpLevelConfig.xpGemTypeConfig[0];
        }


    }

    

}