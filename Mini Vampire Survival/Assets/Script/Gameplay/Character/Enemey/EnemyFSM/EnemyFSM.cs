using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Enemy
{
    public enum EnemyStateEnum { None, Idle, Chase, Attack ,Died };
    public enum AnimatorParameterKeyEnum { MoveSpeed, AttackIndex, OnAttack, OnHit, IsDied };
    public enum AnimNameEnum { Locomotion };
    public class EnemyFSM : Base_FSM<EnemyFSM,EnemyStateEnum>
    {
        static readonly string ChannelKey = "[EnemyFSM] ";  //will be used to fillter logs 
        [field: SerializeField, Space(10)] public Enemy enemy { get; private set; }
        [field: SerializeField] public Health m_Health { get; private set; }
        [field: SerializeField] public Movement m_Movement { get; private set; }

        [Space(20)]        
        [SerializeField] float MaxLocomotionBlendAmount;
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }



        [field: Header("Current Progress") ,SerializeField] public Transform Target{ get; private set; }

        public Animator animator { get { return enemy?.animator; } }
        public Transform t_Enemy { get; private set; }

        public IDamagable playerDamagable;

        protected override void Awake()
        {
            base.Awake();
        }



        private void Update()
        {
            if (currentStateEnum == EnemyStateEnum.None)
                return;
            activeState.GameUpdate();
        }


        private void FixedUpdate()
        {
            if (currentStateEnum == EnemyStateEnum.None)
                return;
            activeState.GameFixedUpdate();
        }


        private void LateUpdate()
        {
            if (currentStateEnum == EnemyStateEnum.None)
                return;
            activeState.GameLateUpdate();
        }


        

        public void ActivateSystem(Transform target ,int maxHealth, float moveSpeed )
        {
            Target = target; 
            enemy.Init();
            m_Health.Init(maxHealth);
            m_Movement.Init(enemy.transform, moveSpeed, AnimatorParameterKeyEnum.MoveSpeed.ToString(), MaxLocomotionBlendAmount);
            t_Enemy = enemy.transform;
            playerDamagable = target.GetComponent<IDamagable>();
            if (playerDamagable == null)
                Debug.LogError(ChannelKey + "We didnt get the damazable component on player");
            ChangeState(EnemyStateEnum.Idle);
            ChangeState(EnemyStateEnum.Chase);
        }


        public void DeActivateSystem()
        {
            ChangeState(EnemyStateEnum.Idle);
        }


        public bool Check_IsPlayerInRange()
        {
            return Vector2.Distance(Target.position, t_Enemy.position) < AttackRange;
        }
    }
}