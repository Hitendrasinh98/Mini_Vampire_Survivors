using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.PlayerSystem
{
    public enum PlayerStateEnum { None ,Idle ,surviving ,Died };
    public enum AnimatorParameterKeyEnum { MoveSpeed ,AttackIndex ,OnAttack ,OnHit ,IsDied};
    public enum AnimNameEnum { Locomotion };

    public class PlayerFSM : Base_FSM<PlayerFSM,PlayerStateEnum>
    {
        static readonly string ChannelKey = "[PlayerFsM] ";  //will be used to fillter logs 
        [field: SerializeField,Space(10)] public Player player { get; private set; }
        [field: SerializeField] public Health m_Health { get; private set; }
        [field: SerializeField] public Movement m_Movement { get; private set; }
        
        [Space(20)]        
        [SerializeField] float MaxLocomotionBlendAmount;


        public Animator animator { get { return player?.animator; } }

        protected override void Awake()
        {
            base.Awake();
            EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);

        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
        }

        private void Update()
        {
            if (currentStateEnum == PlayerStateEnum.None)
                return;
            activeState.GameUpdate();
        }


        private void FixedUpdate()
        {
            if (currentStateEnum == PlayerStateEnum.None)
                return;
            activeState.GameFixedUpdate();
        }


        private void LateUpdate()
        {
            if (currentStateEnum == PlayerStateEnum.None)
                return;
            activeState.GameLateUpdate();
        }


        void OnGameStart(EventManager.GameStartData gameStartData)
        {
            Initialize(gameStartData.MaxHealth, gameStartData.MoveSpeed);
            ChangeState(PlayerStateEnum.Idle);
        }

        void OnGameComplete()
        {
            ChangeState(PlayerStateEnum.None);
        }

        void Initialize(int maxHealth , float moveSpeed)
        {
            m_Health.Init(maxHealth);
            m_Movement.Init(player.transform,moveSpeed, AnimatorParameterKeyEnum.MoveSpeed.ToString(), MaxLocomotionBlendAmount);
            player.Init(m_Health);
            Debug.Log(ChannelKey + "Player FSM Started");
        }

    }
}