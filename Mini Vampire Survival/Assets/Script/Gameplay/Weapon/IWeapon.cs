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

        void PowerUp_FireRate(float amount);
        void PowerUp_PrimaryWeaponDamage(float damage);
        

    }
}