using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.PlayerSystem
{
    public enum PlayerStateEnum { None ,Idle ,surviving ,Died }; //Enum for PlayerFSM State 
    public enum AnimatorParameterKeyEnum { MoveSpeed ,AttackIndex ,OnAttack ,OnHit ,IsDied}; //Enum for Player Animator Parameter keys
    public enum AnimNameEnum { Locomotion }; //Enum for Player Animator state name

    /// <summary>
    /// This is the FSM for Player 
    /// conrtoll player states/Behaviour and tranfer between each other smoothlly
    /// allow us to import and remove any state/behaviour from the FSM without effecting exiting flow of player mechnics
    /// </summary>
    public class PlayerFSM : Base_FSM<PlayerFSM,PlayerStateEnum>
    {
        static readonly string ChannelKey = "[PlayerFsM] ";  //will be used to fillter logs 
        [field: SerializeField,Space(10)] public Player player { get; private set; }
        /// <summary>
        /// Health Component where health realted funtionality handles
        /// </summary>
        [field: SerializeField] public Health m_Health { get; private set; }
        /// <summary>
        /// Movemtn Component witch is used to move this player 
        /// </summary>
        [field: SerializeField] public Movement m_Movement { get; private set; }
        

        public Animator animator { get { return player?.animator; } }

        protected override void Awake()
        {
            base.Awake();
            EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            EventManager.Instance.AddObserver_OnGameComeplete(OnGameComplete);
            EventManager.Instance.AddObserver_OnPowerUpSelect(OnPowerSelect);


        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            EventManager.Instance.RemoveObserver_OnGameComeplete(OnGameComplete);
            EventManager.Instance.RemoveObserver_OnPowerUpSelect(OnPowerSelect);
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

        /// <summary>
        /// Raised when Game Start with starting Config data
        /// </summary>
        /// <param name="gameStartData"></param>
        void OnGameStart(EventManager.GameStartData gameStartData)
        {
            Initialize(gameStartData.MaxHealth, gameStartData.MoveSpeed);
            ChangeState(PlayerStateEnum.Idle);
        }

        /// <summary>
        /// Raised when game complete event is raised
        /// </summary>
        void OnGameComplete()
        {
            ChangeState(PlayerStateEnum.None);
        }

        /// <summary>
        /// Called when the PowerSelect is fired
        /// </summary>
        /// <param name="powerUpType"></param>
        /// <param name="amount"></param>
        void OnPowerSelect(ConfigData.LevelUPPowerEnum powerUpType , float amount)
        {
            if(powerUpType == ConfigData.LevelUPPowerEnum.Health)
            {
                m_Health.PowerHealth(amount);
                float remaingHealth = m_Health.remainingHealth;
                int maxHealth = m_Health.maxHealth;
                EventManager.Instance.OnPlayerGotHit?.Invoke(0, remaingHealth, maxHealth);
            }
        }

        /// <summary>
        /// Inititalized of health and mvoemetn of this player 
        /// </summary>
        /// <param name="maxHealth"></param>
        /// <param name="moveSpeed"></param>
        void Initialize(int maxHealth , float moveSpeed)
        {
            m_Health.Init(maxHealth);
            m_Movement.Init(player.transform,moveSpeed, AnimatorParameterKeyEnum.MoveSpeed.ToString());
            player.Init(m_Health);
            Debug.Log(ChannelKey + "Player FSM Started");
        }

    }
}