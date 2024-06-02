using Mini_Vampire_Surviours.Gameplay.EnemySystem;
using Mini_Vampire_Surviours.Gameplay.PlayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay
{


    public class Debug_UiManger : MonoBehaviour
    {
        [SerializeField] Enemy enemy;
        [SerializeField] EnemyFSM enemyFSM;
        [SerializeField] float damageAmount;



       
        

        public void Onclick_DamazeEnemy()
        {
            enemy.TakeDamage(damageAmount);
        }


        public void Onclick_EnmeyInit()
        {
            enemyFSM.ActivateSystem(Mediator.Instance.t_Player);
        }
    }
}