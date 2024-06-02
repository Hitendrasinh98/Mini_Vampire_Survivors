using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public class EnemyState_Died : Core.State<EnemyFSM,EnemyStateEnum>
    {
        public override void Enter()
        {
            fsm.animator.SetAnimatorBoolKey(AnimatorParameterKeyEnum.IsDied, true);
            Destroy(fsm.gameObject, 2);
            Mediator.Instance.m_XPLevelManager.SpawnXpGems(fsm.t_Enemy.position, LevelUpSystem.XPGemTypeEnum.Enemy);
        }

        public override void Exit()
        {

        }
    }
}