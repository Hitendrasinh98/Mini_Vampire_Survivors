using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.StatesSystem
{
    [DefaultExecutionOrder(-1)]
    public class StatesManager : MonoBehaviour
    {
        static StatesManager instance;
        public static StatesManager Instance=>instance;

        [field: SerializeField] public int totalSurvived { get; private set; }
        [field: SerializeField] public int totalKilled{ get; private set; }
        [field: SerializeField] public bool IsWon{ get; private set; }


        System.Action<int> OnSurvivingTimeIncrease;  //action<seconds>
        public void AddObserver_OnSurviveTimeIncrease(System.Action<int> callback) => OnSurvivingTimeIncrease += callback;
        public void RemoveObserver_OnSurviveTimeIncrease(System.Action<int> callback) => OnSurvivingTimeIncrease -= callback;


        Coroutine surviveRoutine;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);

        }

        private void Start()
        {
            Core.EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);
        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            Core.EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
        }

        void OnGameStart(Core.EventManager.GameStartData gameStartData)
        {
            surviveRoutine = StartCoroutine( Co_StartSurvingTimer());
        }


        void OnGameComplete()
        {
            StopCoroutine(surviveRoutine);
        }

        public void OnKilledEnemy()
        {
            totalKilled++;
        }

        IEnumerator Co_StartSurvingTimer()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                totalSurvived++;
                OnSurvivingTimeIncrease?.Invoke(totalSurvived);
            }
        }

        public void Set_ResultStatus(bool isWon)
        {
            this.IsWon = IsWon;                
        }

    }
}