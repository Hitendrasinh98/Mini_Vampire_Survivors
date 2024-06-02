using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.LevelUpSystem
{
    public enum XPGemTypeEnum {None, Zombie ,Bigger_Zombie ,Faster_Zombie ,LootBox ,Boss};

    public class XPLevelManager : MonoBehaviour
    {
        [SerializeField] XpGem prefab_XpGem;
        [SerializeField] List<LevelConfig> levelConfigs = new List<LevelConfig>();
        [SerializeField] List<GemSpawnConfig> xpGemTypeConfig = new List<GemSpawnConfig>();

        [Header("Current Progress")]
        [SerializeField] int currentLevel;
        [SerializeField] int currentXPGems;
        [SerializeField] int requiredXpGems;
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
            Core.EventManager.Instance.OnXPGemCollect?.Invoke(currentXPGems, requiredXpGems, currentLevel);
        }

        void Set_NextXpLevel()
        {
            if (currentLevel + 1 < levelConfigs.Count)
                requiredXpGems = levelConfigs[currentLevel + 1].xpGemsRequired;
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
            Core.EventManager.Instance.OnXPGemCollect?.Invoke(currentXPGems, requiredXpGems,currentLevel);
            CheckForLevelUp();
        }

        void CheckForLevelUp()
        {
            if (currentXPGems >= requiredXpGems)
            {
                currentLevel++;
                currentXPGems -= requiredXpGems;
                //Show Popup
                Set_NextXpLevel();
            }
        }

        GemSpawnConfig Get_XpGemSpawnData(XPGemTypeEnum xPGemType)
        {
            for (int i = 0; i < xpGemTypeConfig.Count; i++)
            {
                if (xpGemTypeConfig[i].XpGemType == xPGemType)
                {
                    return xpGemTypeConfig[i];
                }
            }
            Debug.LogError("Didnt  find the xpGem Config for " + xPGemType);
            return xpGemTypeConfig[0];
        }


    }

    [System.Serializable]
    struct LevelConfig
    {
        public int level;
        public int xpGemsRequired;
    }

    [System.Serializable]
    struct GemSpawnConfig
    {
        public XPGemTypeEnum XpGemType;
        public int spawnAmount;
        public int gemAmountPerItem;
        public float spawnRadius;
    }

}