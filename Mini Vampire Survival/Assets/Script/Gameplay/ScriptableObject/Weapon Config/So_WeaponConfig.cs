using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.ConfigData
{
    [CreateAssetMenu(fileName = "So Weapon Config", menuName = "Gameplay/Weapon Config")]
    public class So_WeaponConfig : ScriptableObject
    {
        [field: SerializeField] public List<WeaponSystem.BaseWeapon> availableWeapons { get; private set; }

    }
}