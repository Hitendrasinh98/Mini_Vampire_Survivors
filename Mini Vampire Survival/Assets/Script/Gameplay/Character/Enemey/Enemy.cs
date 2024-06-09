using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    /// <summary>
    /// Will be Attach to Enemy object.
    /// will controll all the world intraction. ex : collision, Animator Events ,  etc... 
    /// </summary>
    public class Enemy : MonoBehaviour ,IDamagable
    {
        [field: SerializeField] public Animator animator { get; private set; }


        IHealth healthComponent;

        /// <summary>
        /// Whenever this enitity recive damage , it will notify every one through this callback
        /// </summary>
        System.Action<float> OnTookDamage; //TookDamage
        public void AddObserver_OnHit(System.Action<float> callback) => OnTookDamage += callback;
        public void RemoveObserver_OnHit(System.Action<float> callback) => OnTookDamage -= callback;

        /// <summary>
        /// Dependency on health componnet to know if this enitity is alive or not
        /// </summary>
        /// <param name="healhtComponnet"></param>
        public void Init(IHealth healhtComponnet)
        {
            this.healthComponent = healhtComponnet;
        }

        /// <summary>
        /// Will Raise event with damageAmount that this entity took
        /// </summary>
        /// <param name="damageAmount"></param>
        public void TakeDamage(float damageAmount)
        {
            OnTookDamage?.Invoke(damageAmount);
        }

        public bool IsAllive => healthComponent!=null? healthComponent.IsAlive: false;
    }
}