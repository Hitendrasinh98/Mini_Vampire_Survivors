using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.EnemySystem
{
    public enum EnemyStateEnum { None, Idle, Chase, Attack ,Died };
    public enum AnimatorParameterKeyEnum { MoveSpeed, AttackIndex, OnAttack, OnHit, IsDied };
    public enum AnimNameEnum { Locomotion };
    public class EnemyFSM : Base_FSM<EnemyFSM,EnemyStateEnum> , IEnemy
    {
        static readonly string ChannelKey = "[EnemyFSM] ";  //will be used to fillter logs 
        [field: SerializeField, Space(10)] public Enemy enemy { get; private set; }
        [field: SerializeField] public LevelUpSystem.XPGemTypeEnum gemTypeEnum { get; private set; }

        [field: SerializeField] public Health m_Health { get; private set; }
        [field: SerializeField] public Movement m_Movement { get; private set; }

        [Space(20)]
        [SerializeField] int maxHealth;
        [SerializeField] float moveSpeed;
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
            m_Health.AddObserver_OnDied(OnDied);
        }

        private void OnDestroy()
        {
            m_Health.RemoveObserver_OnDied(OnDied);
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


        

        public void ActivateSystem(Transform target )
        {
            Target = target; 
            m_Health.Init(maxHealth);
            m_Movement.Init(enemy.transform, moveSpeed, AnimatorParameterKeyEnum.MoveSpeed.ToString(), MaxLocomotionBlendAmount);
            t_Enemy = enemy.transform;
            t_Enemy.position = Vector2.zero;
            t_Enemy.localPosition = Vector2.zero;
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


        

        void OnDied()
        {
            ChangeState(EnemyStateEnum.Died);
        }

        public GameObject Get_GameObject()
        {
            return gameObject;
        }
    }
}