using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.PlayerSystem
{ 
    /// <summary>
    /// Represent the player entity where all the world interaction will happen ,ex collision , Animation event trigger ,etc
    /// </summary>
    public class Player : MonoBehaviour, IDamagable
    {
        // Reference to the Cinemachine Impulse Source to genrate effect exact from player pos
        [SerializeField] Cinemachine.CinemachineImpulseSource impulseSource; 

        [field: SerializeField] public Animator animator { get; private set; }
        /// <summary>
        /// PLace where primary weapon will locate
        /// </summary>
        [field: SerializeField] public Transform  PrimaryWeaponSlot { get; private set; }
        /// <summary>
        /// Place where secondary weapon will locate
        /// </summary>
        [field: SerializeField] public Transform SecondaryWeaponSlot { get; private set; }

        // Health Component witch represent the health of the player
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
            Genrate_CameraShake();
            OnTookDamage?.Invoke(damageAmount);
        }


        /// <summary>
        /// With the help of cinamchine Impulse source we will shake the camnera bit
        /// </summary>
        void Genrate_CameraShake()
        {
            Vector2 velocity = new Vector2(Random.Range(-.05f, .05f), Random.Range(-.05f, .05f));
            impulseSource.GenerateImpulse(velocity);
        }
    }

}