using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// Interface to add a abstract layer for Health System to all the entity who will have health component
    /// </summary>
    public interface IHealth
    {
        /// <summary>
        /// Initlaize health component with this init funtion
        /// </summary>
        /// <param name="maxHealth"></param>
        void Init(int maxHealth);
        
        /// <summary>
        /// Call this when this healhtComponne Enity will recive any damage
        /// </summary>
        /// <param name="amount"></param>
        void TakeDamage(float amount);

        /// <summary>
        /// Call this when this healhtComponne Enity will recive any Health
        /// </summary>
        /// <param name="amount"></param>
        void Heal(float amount);
        bool IsAlive { get; }
    }
}