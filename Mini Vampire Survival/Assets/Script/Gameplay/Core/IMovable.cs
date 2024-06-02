using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public interface IMovable
    {
        void Init(Transform target, float moveSpeed, string locoMotionBlendTreeKey);
        void Move(Vector2 direction);
        void Set_FaceFlipped(bool isFliped);
    }
}