using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Mini_Vampire_Surviours.Gameplay.Enemy
{
    public class EnemyState_Attack : Core.State<EnemyFSM, EnemyStateEnum>
    {
        float AttackRate;
        float currentTimer;

        public override void Enter()
        {
            fsm.animator.Play(AnimNameEnum.Locomotion.ToString(), 0, 0);
            fsm.animator.SetAnimatorFloatKey(AnimatorParameterKeyEnum.MoveSpeed, 0);
            AttackRate = fsm.AttackRate;
            currentTimer = AttackRate;
        }

        

        public override void Exit()
        {
            currentTimer = 0;
        }

        public override void GameFixedUpdate()
        {
            if (!fsm.Check_IsPlayerInRange())
            {
                fsm.ChangeState(EnemyStateEnum.Chase);
                return;
            }

            CheckForAttack();
        }


        void CheckForAttack()
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= AttackRate)
            {
                Attack();
                currentTimer = 0;
            }
        }

        void Attack()
        {
            int attackIndex = 0;//Random.Range(0, 3)
            fsm.animator.SetAnimatorIntKey(AnimatorParameterKeyEnum.AttackIndex, attackIndex);
            fsm.animator.TrigerAnimation(AnimatorParameterKeyEnum.OnAttack);
            fsm.playerDamagable.TakeDamage(fsm.AttackDamage);
        }

    }
}