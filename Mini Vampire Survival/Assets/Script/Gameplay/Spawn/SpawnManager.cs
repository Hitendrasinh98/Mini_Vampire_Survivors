using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.spawnSystem
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] List<SpawnData> avialableSpawnItem = new List<SpawnData>();
        [SerializeField] float spawnOffset;

        [Header("Current Progress")]
        [SerializeField] Transform enemyTarget;
        [SerializeField] private int currentLevel;
        [SerializeField] private float screenWidth;
        [SerializeField] private float screenHeight;
        
        List<Coroutine> activeCorotine = new List<Coroutine>();
        List<EnemySystem.IEnemy> activeEnemies = new List<EnemySystem.IEnemy>();

        void Awake()
        {
            // Get screen dimensions
            screenHeight = Camera.main.orthographicSize;
            screenWidth = screenHeight * Camera.main.aspect;

            Core.EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.AddObserver_OnXpLevelUP(OnXPLevelUp);
            Core.EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);

        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.RemoveObserver_OnXpLevelUP(OnXPLevelUp);
            Core.EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
        }

        void OnGameStart(Core.EventManager.GameStartData gameStartData)
        {
            enemyTarget = Mediator.Instance.t_Player;
            currentLevel = gameStartData.XPLevel;
            AddEnemySpanerCoroting(0, currentLevel);
        }

        void OnXPLevelUp(int newLevel)
        {
            AddEnemySpanerCoroting(currentLevel, newLevel);
            currentLevel = newLevel;
        }


        void OnGameComplete()
        {
            for (int i = 0; i < activeCorotine.Count; i++)
            {
                StopCoroutine(activeCorotine[i]);
            }

            for (int i = 0; i < activeEnemies.Count; i++)
            {
                activeEnemies[i]?.DeActivateSystem();
            }
        }


        void AddEnemySpanerCoroting(int fromLevel ,  int toLevel)
        {
            foreach (SpawnData enemyData in avialableSpawnItem)
            {
                if (fromLevel >= enemyData.LevelRequired && enemyData.LevelRequired <= toLevel)
                {
                    Coroutine spawn_Corotine = StartCoroutine(SpawnEnemy(enemyData));
                    activeCorotine.Add(spawn_Corotine);
                }
            }
        }

        IEnumerator SpawnEnemy(SpawnData enemyData)
        {
            while (true)
            {
                yield return new WaitForSeconds(enemyData.spawnRate);

                // Get a random position at the border of the screen
                Vector3 spawnPosition = GetRandomBorderPosition();
                EnemySystem.EnemyFSM enemyComponent = Instantiate(enemyData.prefab, spawnPosition, Quaternion.identity);
                enemyComponent.ActivateSystem(enemyTarget);
                activeEnemies.Add(enemyComponent);
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

   


        [System.Serializable]
        struct SpawnData
        {
            public int LevelRequired;
            public float spawnRate;
            public EnemySystem.EnemyFSM prefab;
            
            private EnemySystem.IEnemy enemyComponent;

            public EnemySystem.IEnemy Get_EnemyComponent()
            {
                if (enemyComponent == null)
                {
                    Debug.Log("null , Getting Enemy Component");
                    enemyComponent = prefab.GetComponent<EnemySystem.IEnemy>();
                }
                return enemyComponent;
            }
        }
    }

}