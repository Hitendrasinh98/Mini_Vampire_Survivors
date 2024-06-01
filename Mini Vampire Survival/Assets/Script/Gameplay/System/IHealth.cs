using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public interface IHealth
    {
        void Init(int maxHealth);
        void TakeDamage(float amount);
        void Heal(float amount);
        bool IsAlive { get; }
    }
}