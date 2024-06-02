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
        public readonly string ChannelKey = "[EnemyFSM] ";  //will be used to fillter logs 
        
        [Space(10)]
        [SerializeField] ConfigData.So_EnemyConfig so_EnemyConfig;
        [field: SerializeField] public Enemy enemy { get; private set; }
        [field: SerializeField] public ConfigData.XPGemTypeEnum gemTypeEnum { get; private set; }
        [field: SerializeField] public Health m_Health { get; private set; }
        [field: SerializeField] public Movement m_Movement { get; private set; }




        [field: Header("Current Progress") ,SerializeField] public Transform Target{ get; private set; }

        public Animator animator { get { return enemy?.animator; } }
        public Transform t_Enemy { get; private set; }
        public float AttackRate => so_EnemyConfig.AttackRate;
        public float AttackRange => so_EnemyConfig.AttackRange;
        public float AttackDamage => so_EnemyConfig.AttackDamage;



        public IDamagable playerDamagable;

        protected override void Awake()
        {
            base.Awake();
            m_Health.AddObserver_OnDied(OnDied);
            EventManager.Instance.AddObserver_OnGameComeplete(DeActivateSystem);
        }

        private void OnDestroy()
        {
            m_Health.RemoveObserver_OnDied(OnDied);
            EventManager.Instance.RemoveObserver_OnGameComeplete(DeActivateSystem);
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
            t_Enemy = enemy.transform;
            
            Target = target; 
            playerDamagable = target.GetComponent<IDamagable>();

            m_Health.Init(so_EnemyConfig.maxHealth);
            m_Movement.Init(enemy.transform, so_EnemyConfig.moveSpeed, AnimatorParameterKeyEnum.MoveSpeed.ToString());
            enemy.Init(m_Health);

            ChangeState(EnemyStateEnum.Idle);
            ChangeState(EnemyStateEnum.Chase);
        }


        public void DeActivateSystem()
        {
            ChangeState(EnemyStateEnum.Idle);
        }


        public bool Check_IsPlayerInRange()
        {
            return Vector2.Distance(Target.position, t_Enemy.position) < so_EnemyConfig.AttackRange;
        }


        

        void OnDied()
        {
            ChangeState(EnemyStateEnum.Died);
        }

    }
}