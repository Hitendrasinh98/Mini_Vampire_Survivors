using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    /// <summary>
    /// Interface to add abstraction layer in diffrent type of enemies.
    /// </summary>
    public interface IEnemy
    {
        /// <summary>
        /// Will activate the enemy
        /// </summary>
        /// <param name="target"></param>
        void ActivateSystem(Transform target);
        
        /// <summary>
        /// Will Deactivate the enemy
        /// </summary>
        void DeActivateSystem();
    }

}