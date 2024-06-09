using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
    /// <summary>
    /// This is a weapon thhat shoot magic projectile in direction and deal with medium range effect
    /// </summary>
    public class MagicWand : BaseWeapon
    {
        [Header("MagicWand Weapon Config")]

        [SerializeField] GameObject projectilePrefab;
        [SerializeField] Transform firePoint;
        [SerializeField] float bulletSpeed;


        [Header("current Progress")]
        [SerializeField] Transform target;
        Vector2 targetDirection;

        protected override void Fire()
        {
            UnityEngine.Profiling.Profiler.BeginSample("Nearest Enemy Search");
            target = FindNearestEnemy();
            UnityEngine.Profiling.Profiler.EndSample();

            if (target == null)
                return;
            
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile proj = projectile.GetComponent<Projectile>();
            targetDirection = target.position - firePoint.position;
            proj.Init(damage, range , bulletSpeed,targetDirection);
            Vector2 velocity = new Vector2(Random.Range(-.15f, .15f), Random.Range(-.15f, .15f));
            impulseSource.GenerateImpulse(velocity);

        }


        [ContextMenu("Check Enemy")]
        void Debug_Enemy()
        {

            target = FindNearestEnemy();

        }

        /// <summary>
        /// Will define the attack range
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
