using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
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
            target = FindNearestEnemy();
            if (target == null)
                return;

            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Projectile proj = projectile.GetComponent<Projectile>();
            targetDirection = target.position - firePoint.position;
            proj.Init(damage, range , bulletSpeed,targetDirection);
        }


        [ContextMenu("Check Enemy")]
        void Debug_Enemy()
        {
            target = FindNearestEnemy();
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }
    }
}
