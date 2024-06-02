using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public interface IEnemy
    {
        GameObject Get_GameObject();
        void ActivateSystem(Transform target);
        void DeActivateSystem();
    }

}