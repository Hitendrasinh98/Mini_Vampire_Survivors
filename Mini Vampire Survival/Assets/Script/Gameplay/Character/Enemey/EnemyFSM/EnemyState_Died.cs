using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public class EnemyState_Died : Core.State<EnemyFSM,EnemyStateEnum>
    {
        [SerializeField] float deathDelay = 2;
        Coroutine deathRoutine;

        public override void Enter()
        {
            fsm.animator.SetAnimatorBoolKey(AnimatorParameterKeyEnum.IsDied, true);
            deathRoutine = StartCoroutine(Co_Death()); //Invoke(nameof(SpawnGem), deathDelay);
        }

        public override void Exit()
        {
            print("we are here");
            if (deathRoutine != null)
                StopCoroutine(deathRoutine);// CancelInvoke(nameof(SpawnGem));
        }


        IEnumerator Co_Death()
        {
            yield return new WaitForSeconds(deathDelay);
            SpawnGem();
        }

        void SpawnGem()
        {
            Mediator.Instance.m_XPLevelManager.SpawnXpGems(fsm.t_Enemy.position, fsm.gemTypeEnum);
            Destroy(fsm.gameObject);
        }
    }
}