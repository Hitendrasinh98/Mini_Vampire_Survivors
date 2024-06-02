using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public interface IEnemy
    {
        void ActivateSystem(Transform target);
        void DeActivateSystem();
    }

}