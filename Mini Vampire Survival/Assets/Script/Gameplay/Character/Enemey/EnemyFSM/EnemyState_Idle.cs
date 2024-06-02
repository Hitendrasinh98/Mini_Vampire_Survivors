using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public class EnemyState_Idle : Core.State<EnemyFSM, EnemyStateEnum>
    {
        public override void Enter()
        {
            fsm.animator.ResetAnimator();
            fsm.animator.Play(AnimNameEnum.Locomotion.ToString(), 0, 0);
            fsm.t_Enemy.position = Vector2.zero;
            fsm.t_Enemy.localPosition = Vector2.zero;
        }

        public override void Exit()
        {
        }


      
    }
}