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

            Core.EventManager.Instance.OnGameStart?.Invoke(gameStartData);
        }
    }
}