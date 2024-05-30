using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is base Class for all the FSM realted system ex : PlayerFSM , EnemyFSM ,etc
/// where each FSM can have multiple states ex: spawn,Idle,Chase ,Die ...
/// </summary>

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public abstract class State<TFSM, TEnum> : MonoBehaviour where TFSM : class where TEnum : System.Enum
    {
        [field: SerializeField] public TEnum StateEnum { get; private set; }
        protected TFSM fsm;  // ex either playerFSM or EnemyFSM

        /// <summary>
        /// use this to initialize state ,will call only once in lifetime         
        /// consider like a awake 
        /// </summary>
        /// <param name="fsm">Pass the Fsm to initialize with it</param>
        public virtual void Init(TFSM fsm)
        {
            this.fsm = fsm;
        }

        /// <summary>
        /// use this to ready state every time when we switch the states
        /// consider like a OnEneble
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// use this to Disable state every time when we switch the states
        /// consider like a OnDisable
        /// </summary>
        public abstract void Exit();


        /// <summary>
        /// use this to gameLogic every frame 
        /// consider like a Update
        /// </summary>
        public virtual void GameUpdate()
        {

        }

        /// <summary>
        /// use this to physicsLogic every frame 
        /// consider like a PhysicUpdate
        /// </summary>
        public virtual void GameFixedUpdate()
        {

        }

        /// <summary>
        /// use this to Update every frame 
        /// consider like a LateUpdate
        /// </summary>
        public virtual void GameLateUpdate()
        {

        }

    }
}