using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public class EnemyState_Died : Core.State<EnemyFSM,EnemyStateEnum>
    {
        [SerializeField] float deathDelay = 2;
        public override void Enter()
        {
            fsm.animator.SetAnimatorBoolKey(AnimatorParameterKeyEnum.IsDied, true);
            Invoke(nameof(SpawnGem), deathDelay);
        }

        public override void Exit()
        {
            CancelInvoke(nameof(SpawnGem));
        }


        void SpawnGem()
        {
            Mediator.Instance.m_XPLevelManager.SpawnXpGems(fsm.t_Enemy.position, fsm.gemTypeEnum);
            Destroy(fsm.gameObject);
        }
    }
}