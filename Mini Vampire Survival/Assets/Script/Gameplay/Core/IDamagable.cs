using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public interface IDamagable
    {
        bool IsAllive { get; }
        void TakeDamage(float damageAmount);
    }
}