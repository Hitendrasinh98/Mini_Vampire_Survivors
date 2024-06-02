using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
    public abstract class BaseWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] WeaponTypeEnum weaponTypeEnum;
        [SerializeField] float fireRate = 1f;
        [SerializeField] protected float damage = 10f;
        [SerializeField] protected float range = 5f;
        
        [Header("Collision detection Config")]
        [SerializeField] float radiusIncrement = 0.5f; // Increment for each radius expansion step
        [SerializeField] LayerMask enemyLayer; // Layer on which enemies are placed

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

        protected Transform FindNearestEnemy()
        {
            float currentRadius = 1; // Start with a small radius

            while (currentRadius <= range)
            {
                Collider2D hit = Physics2D.OverlapCircle(transform.position, currentRadius, enemyLayer);
                if (hit != null)
                {
                    if(hit.TryGetComponent<Core.IDamagable>(out Core.IDamagable damagable))
                    {
                        if (damagable.IsAllive)
                            return hit.transform;
                    }
                }

                currentRadius += radiusIncrement;
            }

            return null;
        }

        public void PowerUp_FireRate(float amount)
        {
            fireRate -= amount;
            if (fireRate <= 0.25f)
                fireRate = 0.25f;
        }

        public void PowerUp_PrimaryWeaponDamage(float damage)
        {
            this.damage += damage;
        }
    }
}