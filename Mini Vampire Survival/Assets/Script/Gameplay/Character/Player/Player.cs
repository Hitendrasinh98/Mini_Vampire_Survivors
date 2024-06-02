using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.PlayerSystem
{ 
    public class Player : MonoBehaviour, IDamagable
    {
        [SerializeField] Cinemachine.CinemachineImpulseSource impulseSource; // Reference to the Cinemachine Impulse Source

        [field: SerializeField] public Animator animator { get; private set; }
        [field: SerializeField] public Transform  PrimaryWeaponSlot { get; private set; }
        [field: SerializeField] public Transform SecondaryWeaponSlot { get; private set; }

        IHealth healthComponent;

        public bool IsAllive => healthComponent.IsAlive;

        System.Action<float> OnTookDamage; //TookDamage
        public void AddObserver_OnHit(System.Action<float> callback) => OnTookDamage += callback;
        public void RemoveObserver_OnHit(System.Action<float> callback) => OnTookDamage -= callback;





        /// <summary>
        /// Use this to initialize sometjing specifically for this player
        /// </summary>
        public void Init(IHealth healthComponent)
        {
            this.healthComponent = healthComponent;
        }

        public void TakeDamage(float damageAmount)
        {
            Vector2 velocity = new Vector2(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
            impulseSource.GenerateImpulse(velocity);
            OnTookDamage?.Invoke(damageAmount);
        }
    }

}