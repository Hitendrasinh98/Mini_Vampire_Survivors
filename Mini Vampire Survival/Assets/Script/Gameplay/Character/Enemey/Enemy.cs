using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public class Enemy : MonoBehaviour ,IDamagable
    {
        [field: SerializeField] public Animator animator { get; private set; }



        System.Action<float> OnTookDamage; //TookDamage
        public void AddObserver_OnHit(System.Action<float> callback) => OnTookDamage += callback;
        public void RemoveObserver_OnHit(System.Action<float> callback) => OnTookDamage -= callback;


        public void TakeDamage(float damageAmount)
        {
            OnTookDamage?.Invoke(damageAmount);
        }
    }
}