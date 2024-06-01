using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mini_Vampire_Surviours.Gameplay.Enemy
{
    public class EnemyState_Chase : Core.State<EnemyFSM, EnemyStateEnum>
    {
        Vector2 direction;

        public override void Enter()
        {
            fsm.animator.Play(AnimNameEnum.Locomotion.ToString(), 0, 0);
        }

        public override void GameFixedUpdate()
        {
            ChasePlayer();
        }

        public override void Exit()
        {

        }



        void ChasePlayer()
        {
            if(fsm.Check_IsPlayerInRange())
            {
                fsm.ChangeState(EnemyStateEnum.Attack);
            }
            else
            {
                direction = (fsm.Target.position - fsm.t_Enemy.position).normalized;
                fsm.m_Movement.Move(direction);
            }
        }

    }
}