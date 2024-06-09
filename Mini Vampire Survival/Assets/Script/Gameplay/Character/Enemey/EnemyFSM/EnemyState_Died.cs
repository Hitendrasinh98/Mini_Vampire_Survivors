using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    /// <summary>
    /// This is where we define enemyies death behaviour
    /// </summary>
    public class EnemyState_Died : Core.State<EnemyFSM,EnemyStateEnum>
    {
        [SerializeField] float deathDelay = 2;
        Coroutine deathRoutine;

        public override void Enter()
        {
            fsm.animator.SetAnimatorBoolKey(AnimatorParameterKeyEnum.IsDied, true);
            StatesSystem.StatsManager.Instance.OnKilledEnemy(); // notify stats manager that this enemy entity is dead
            deathRoutine = StartCoroutine(Co_Death()); //Invoke(nameof(SpawnGem), deathDelay);
        }

        public override void Exit()
        {
            if (deathRoutine != null)
                StopCoroutine(deathRoutine);// CancelInvoke(nameof(SpawnGem));
        }

        /// <summary>
        /// Co Routine to controll the Death delay and then spawning the Gems flow
        /// </summary>
        /// <returns></returns>
        IEnumerator Co_Death()
        {
            yield return new WaitForSeconds(deathDelay);
            SpawnGem();
        }

        /// <summary>
        /// Will spawn the gems based on the gemType. 
        /// ex. every enmey will have little diffrent behaviour and diffrent gems spawn data
        /// </summary>
        void SpawnGem()
        {
            Mediator.Instance.m_XPLevelManager.SpawnXpGems(fsm.t_Enemy.position, fsm.gemTypeEnum);
            Destroy(fsm.gameObject);
        }
    }
}