using Mini_Vampire_Surviours.Gameplay.Enemy;
using Mini_Vampire_Surviours.Gameplay.PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay
{


    public class Debug_UiManger : MonoBehaviour
    {
        [SerializeField] Player player;
        [SerializeField] EnemyFSM enemyFSM;
        [SerializeField] float damageAmount;

        private void Awake()
        {
            Core.EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);
        }

        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
        }

        void OnGameComplete()
        {
            enemyFSM.DeActivateSystem();
        }
        public void Onclick_Damaze()
        {
            player.TakeDamage(damageAmount);
        }

        public void Onclick_EnmeyInit()
        {
            enemyFSM.ActivateSystem(player.transform, 100, 2.5f);
        }
    }
}