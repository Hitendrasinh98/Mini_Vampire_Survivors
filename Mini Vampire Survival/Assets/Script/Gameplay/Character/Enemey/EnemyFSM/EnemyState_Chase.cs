using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public class EnemyState_Chase : Core.State<EnemyFSM, EnemyStateEnum>
    {
        Vector2 direction;

        public override void Enter()
        {
            fsm.animator.Play(AnimNameEnum.Locomotion.ToString(), 0, 0);
            fsm.animator.SetAnimatorFloatKey(AnimatorParameterKeyEnum.MoveSpeed, 0);
            fsm.enemy.AddObserver_OnHit(OnTookDamage);
        }

        public override void GameFixedUpdate()
        {
            ChasePlayer();
        }

        public override void Exit()
        {
            fsm.enemy.RemoveObserver_OnHit(OnTookDamage);
            fsm.animator.SetAnimatorFloatKey(AnimatorParameterKeyEnum.MoveSpeed, 0);
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

        void OnTookDamage(float damageAmount)
        {
            fsm.animator.TrigerAnimation(AnimatorParameterKeyEnum.OnHit);
            fsm.m_Health.TakeDamage(damageAmount);
        }

    }
}