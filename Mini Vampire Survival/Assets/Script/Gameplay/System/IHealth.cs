using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public interface IHealth
    {
        void Init(int maxHealth);
        void TakeDamage(int amount);
        void Heal(int amount);
        bool IsAlive { get; }
    }
}