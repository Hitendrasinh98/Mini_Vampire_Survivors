using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// This is a EventManager witch will be used to brodcast information between publisher and observers
    /// This is Singleton class so anyone can assigne events and fire events when needed
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class EventManager : MonoBehaviour
    {
        static EventManager instance;
        public static EventManager Instance { get { return instance; }  }


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);
        }


        /// <summary>
        /// Used to Notify everyone for New Game Start
        /// </summary>
        public Action<GameStartData> OnGameStart;        
        public void AddObserver_OnGameStart(Action<GameStartData> callback) => OnGameStart += callback;        
        public void RemoveObserver_OnGameStart(Action<GameStartData> callback) => OnGameStart -= callback;

        public struct GameStartData
        {
            public int MaxHealth;
            public int XPLevel;
            public float MoveSpeed;
            public WeaponSystem.WeaponTypeEnum primaryWeapon;
        }

        /// <summary>
        /// Used to Notify everyone for Player got damaze
        /// will notify with damazeAmount , Current Health , Max Helath 
        /// </summary>
        public Action<float, float ,int> OnPlayerGotHit;  //action <tookDamaze , currentHealth , maxHealth        
        public void AddObserver_OnPlayerHit(Action<float, float, int> callback) => OnPlayerGotHit += callback;
        public void RemoveObserver_OnPlayerHit(Action<float, float, int> callback) => OnPlayerGotHit -= callback;


        


        /// <summary>
        /// Used to Notify everyone for Player Collect some Xp Gems
        /// will notify with xpAmountCollected, CurrentXPAmount, MaxXPAmountNeeded
        /// </summary>
        public Action<int, int> OnXPGemCollect;  //action <currentXP, MaxXPNeededCurrentLevel>
        public void AddObserver_OnXPGemCollect(Action<int, int > callback) => OnXPGemCollect += callback;
        public void RemoveObserver_OnXPGemCollect(Action<int, int > callback) => OnXPGemCollect -= callback;

        /// <summary>
        /// Used to Notify everyone for Player Xp level Up
        /// will notify with xpAmountCollected, CurrentXPAmount, MaxXPAmountNeeded
        /// </summary>
        public Action<int> OnXpLevelUP;  //action <currentXP, MaxXPNeededCurrentLevel>
        public void AddObserver_OnXpLevelUP(Action<int> callback) => OnXpLevelUP += callback;
        public void RemoveObserver_OnXpLevelUP(Action<int> callback) => OnXpLevelUP -= callback;



        /// <summary>
        /// Used to Notify everyone for current Game complete either won or gameOver
        /// </summary>
        public Action OnGameComeplete;
        public void AddObserver_OnGameComeplete(Action callback) => OnGameComeplete += callback;
        public void RemoveObserver_OnGameComeplete(Action callback) => OnGameComeplete  -= callback;


    }
}