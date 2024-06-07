using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    /// <summary>
    /// Genric Base class for FSM 
    /// any class with their respective state enum can be implement
    /// </summary>
    /// <typeparam name="TFSM"> Calss for FSM</typeparam>
    /// <typeparam name="TEnum">Enum for thier FSM State </typeparam>
    public abstract class Base_FSM<TFSM, TEnum> : MonoBehaviour where TFSM : Base_FSM<TFSM, TEnum> where TEnum : System.Enum
    {
        [SerializeField] List<State<TFSM, TEnum>> availableStates = new List<State<TFSM, TEnum>>();
        [SerializeField] protected State<TFSM, TEnum> activeState;
        [SerializeField] protected TEnum currentStateEnum;

        protected virtual void Awake()
        {
            InitialzieStates();
        }

        /// <summary>
        /// Initlaize all the states available to this FSM
        /// </summary>
        void InitialzieStates()
        {
            for (int i = 0; i < availableStates.Count; i++)
            {
                availableStates[i].Init((TFSM)this);
            }
        }
        /// <summary>
        /// Will Change state of fsm.
        /// will exit curretn state and then tranfer to new state
        /// </summary>
        /// <param name="changeStateToEnum"></param>
        public virtual void ChangeState(TEnum changeStateToEnum)
        {
            if (activeState != null && activeState.StateEnum.Equals(changeStateToEnum))
            {
                Debug.Log(" <color=red>Already on same State</color>");
                return;
            }
            activeState?.Exit();
            activeState = availableStates.Find(state => state.StateEnum.Equals(changeStateToEnum));
            activeState?.Enter();
            if (activeState != null)
                currentStateEnum = activeState.StateEnum;
            else
            {
                System.Array values = System.Enum.GetValues(typeof(TEnum));
                currentStateEnum = (TEnum)values.GetValue(0);
            }
        }



        
    }
}
