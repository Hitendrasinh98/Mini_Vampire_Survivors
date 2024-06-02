using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.ConfigData
{
    [CreateAssetMenu(fileName = "So Spawn Config", menuName = "Gameplay/Spawn Config")]
    public class So_SpawnConfig : ScriptableObject
    {
        [field: SerializeField] public List<SpawnData> avialableSpawnItem { get; private set; }
        [field: SerializeField] public List<WaveData> waveDataConfig { get; private set; }
    }

    [System.Serializable]
    public struct SpawnData
    {
        public int LevelRequired;
        public float spawnRate;
        public EnemySystem.EnemyFSM prefab;
    }

    [System.Serializable]
    public struct WaveData
    {
        public int minLevel;
        public int maxLevel;
        public float spawnMultipler;
    }
}