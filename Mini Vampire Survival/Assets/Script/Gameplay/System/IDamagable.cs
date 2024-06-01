using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public interface IDamagable
    {
        void TakeDamage(float damageAmount);
    }
}