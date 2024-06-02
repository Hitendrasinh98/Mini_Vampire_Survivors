using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.ConfigData
{
    public enum XPGemTypeEnum { None, Zombie, Bigger_Zombie, Faster_Zombie, LootBox, Boss };

    [CreateAssetMenu(fileName = "So XpLevel Config", menuName = "Gameplay/XpLevelConfig")]
    public class So_XpLevelConfig : ScriptableObject
    {
        [field: SerializeField] public List<LevelConfig> levelConfigs { get; private set; }
        [field: SerializeField] public List<GemSpawnConfig> xpGemTypeConfig { get; private set; }
        [field: SerializeField] public List<LevelUpPower> levelUpPowerConfig { get; private set; }
    }

    [System.Serializable]
    public struct LevelConfig
    {
        public int level;
        public int xpGemsRequired;
    }

    [System.Serializable]
    public struct GemSpawnConfig
    {
        public XPGemTypeEnum XpGemType;
        public int spawnAmount;
        public int gemAmountPerItem;
        public float spawnRadius;
    }

    public enum LevelUPPowerEnum { none, Health, FireRate, Damage };
    [System.Serializable]
    public struct LevelUpPower
    {
        public LevelUPPowerEnum powerType;
        public float amount;
        public string discription;
    }
}