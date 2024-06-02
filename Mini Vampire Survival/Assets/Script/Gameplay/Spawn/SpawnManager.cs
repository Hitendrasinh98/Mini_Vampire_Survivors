using Mini_Vampire_Surviours.Gameplay.ConfigData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.spawnSystem
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] ConfigData.So_SpawnConfig so_SpawnConfig;
        [SerializeField] float spawnOffset;

        [Header("Current Progress")]
        [SerializeField] Transform enemyTarget;
        [SerializeField] private int currentLevel;
        [SerializeField] private float screenWidth;
        [SerializeField] private float screenHeight;
        [SerializeField] WaveData currentwave;

        List<Coroutine> activeCorotine = new List<Coroutine>();

        GameObject EnemyParent ;
        void Awake()
        {
            // Get screen dimensions
            screenHeight = Camera.main.orthographicSize;
            screenWidth = screenHeight * Camera.main.aspect;

            Core.EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.AddObserver_OnXpLevelUP(OnXPLevelUp);
            Core.EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);
            EnemyParent = new GameObject("Enemy Parent");
        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.RemoveObserver_OnXpLevelUP(OnXPLevelUp);
            Core.EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
        }

        void OnGameStart(Core.EventManager.GameStartData gameStartData)
        {
            enemyTarget = Mediator.Instance.m_Player.transform;
            currentLevel = gameStartData.XPLevel;
            Start_Wave();
        }

        void OnXPLevelUp(int newLevel)
        {
            currentLevel = newLevel;
            Set_Wave();
            Start_Wave();
        }


        void OnGameComplete()
        {
            Stop_Allspawnner();
        }


        void Set_Wave()
        {
            currentwave = so_SpawnConfig.waveDataConfig[0];
            for (int i = 0; i < so_SpawnConfig.waveDataConfig.Count; i++)
            {
                if(currentLevel>= so_SpawnConfig.waveDataConfig[i].minLevel  && currentLevel <= so_SpawnConfig.waveDataConfig[i].maxLevel)
                {
                    currentwave = so_SpawnConfig.waveDataConfig[i];
                    break;
                }
            }
        }

        void Start_Wave()
        {
            Set_Wave();
            Stop_Allspawnner();
            foreach (SpawnData enemyData in so_SpawnConfig.avialableSpawnItem)
            {
                if (currentLevel >= enemyData.LevelRequired )
                {
                    Coroutine spawn_Corotine = StartCoroutine(SpawnEnemy(enemyData));
                    activeCorotine.Add(spawn_Corotine);
                }
            }
        }

        void Stop_Allspawnner()
        {
            for (int i = 0; i < activeCorotine.Count; i++)
            {
                StopCoroutine(activeCorotine[i]);
            }
            activeCorotine.Clear();
        }

        IEnumerator SpawnEnemy(SpawnData enemyData)
        {
            while (true)
            {
                yield return new WaitForSeconds(enemyData.spawnRate * currentwave.spawnMultipler);

                // Get a random position at the border of the screen
                Vector3 spawnPosition = GetRandomBorderPosition();
                EnemySystem.EnemyFSM enemyComponent = Instantiate(enemyData.prefab, spawnPosition, Quaternion.identity,EnemyParent.transform);
                enemyComponent.ActivateSystem(enemyTarget);
            }
        }

       
        Vector3 GetRandomBorderPosition()
        {
            float x, y;
            int side = Random.Range(0, 4); // 0 = top, 1 = right, 2 = bottom, 3 = left
            Vector2 spawnPos;
            switch (side)
            {
                case 0: // Top
                    x = Random.Range(-screenWidth, screenWidth);
                    y = screenHeight + spawnOffset;
                    break;
                case 1: // Right
                    x = screenWidth + spawnOffset;
                    y = Random.Range(-screenHeight, screenHeight);
                    break;
                case 2: // Bottom
                    x = Random.Range(-screenWidth, screenWidth);
                    y = -screenHeight - spawnOffset;
                    break;
                case 3: // Left
                    x = -screenWidth - spawnOffset;
                    y = Random.Range(-screenHeight, screenHeight);
                    break;
                default:
                    x = 0;
                    y = 0;
                    break;
            }
            spawnPos = enemyTarget.position + new Vector3(x, y,0);
            return spawnPos;
        }

   


      
    }

}