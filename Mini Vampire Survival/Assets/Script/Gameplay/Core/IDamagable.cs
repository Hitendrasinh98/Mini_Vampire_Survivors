using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// Interface to add a abstraction layer for Damage System
    /// </summary>
    public interface IDamagable
    {
        bool IsAllive { get; }
        void TakeDamage(float damageAmount);
    }
}