using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
    /// <summary>
    /// This is the base class for the all weapons type
    /// will handle basic feature of weapons.
    /// </summary>
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] WeaponTypeEnum weaponTypeEnum;
        [SerializeField] float fireRate = 1f;
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float range = 5f;
        
        [Header("Collision detection Config")]
        [SerializeField] float radiusIncrement = 0.5f; // Increment for each radius expansion step
        [SerializeField] LayerMask enemyLayer; // Layer on which enemies are placed


        protected Cinemachine.CinemachineCollisionImpulseSource impulseSource;
        private float timer;
        public WeaponTypeEnum weaponType => weaponTypeEnum;
        public float FireRate => fireRate;
        
        public virtual void Shoot()
        {
            timer += Time.deltaTime;
                
            if (timer>= fireRate)
            {
                Fire();
                timer = 0;
            }
        }

        protected abstract void Fire();

        /// <summary>
        /// Will Retrive the nearest alive enemy.
        /// with help of sonar system where collider will increase at constant distance till full range,
        /// once we hit the collider then we check alive or not ,
        /// </summary>
        /// <returns></returns>
        protected Transform FindNearestEnemy()
        {
            float currentRadius = 1; // Start with a small radius

            while (currentRadius <= range)
            {
                Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, currentRadius, enemyLayer);                
                if (hit != null)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].TryGetComponent<IDamagable>(out IDamagable damagable))
                        {
                            if (damagable.IsAllive)
                                return hit[i].transform;
                        }
                    }
                }

                currentRadius += radiusIncrement;
            }

            return null;
        }

        /// <summary>
        /// Will be fired when player select firerate improved powerUp
        /// </summary>
        /// <param name="amount">how much firearte improved</param>
        public void PowerUp_FireRate(float amount)
        {
            fireRate -= amount;
            if (fireRate <= 0.25f)
                fireRate = 0.25f;
        }

        /// <summary>
        /// Will be fired when player select Damage improved powerUp
        /// </summary>
        public void PowerUp_PrimaryWeaponDamage(float damage)
        {
            this.damage += damage;
        }

        public void Set_ImpulseSource(Cinemachine.CinemachineCollisionImpulseSource impulseSource)
        {
            this.impulseSource = impulseSource;
        }
    }
}