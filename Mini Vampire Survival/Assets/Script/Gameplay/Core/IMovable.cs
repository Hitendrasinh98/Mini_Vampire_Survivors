using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// Interface to add abstact layer for Movement System to any Entity
    /// </summary>
    public interface IMovable
    {
        /// <summary>
        /// Initilaize the Movement componnet with the target Transform and movespeed
        /// </summary>
        /// <param name="target">Transform Object that will be moving </param>
        /// <param name="moveSpeed">At what speed it will move</param>
        /// <param name="locoMotionBlendTreeKey"> In Animator how fast animation want to play </param>
        void Init(Transform target, float moveSpeed, string locoMotionBlendTreeKey);
        void Move(Vector2 direction);
        void Set_FaceFlipped(bool isFliped);
    }
}