using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.WeaponSystem
{
    public interface IWeapon
    {
        WeaponTypeEnum weaponType { get; }
        float FireRate { get; }
        void Shoot();

        /// <summary>
        /// Called when player select power of Firerate improved
        /// </summary>
        /// <param name="amount"></param>
        void PowerUp_FireRate(float amount);
        /// <summary>
        /// Called when player select power of Weapon Damage increase
        /// </summary>
        /// <param name="damage"></param>
        void PowerUp_PrimaryWeaponDamage(float damage);
        

    }
}