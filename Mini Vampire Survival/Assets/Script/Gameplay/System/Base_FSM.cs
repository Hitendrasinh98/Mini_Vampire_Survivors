using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Core
{
    public abstract class Base_FSM<TFSM, TEnum> : MonoBehaviour where TFSM : Base_FSM<TFSM, TEnum> where TEnum : System.Enum
    {
        [SerializeField] List<State<TFSM, TEnum>> availableStates = new List<State<TFSM, TEnum>>();
        [SerializeField] State<TFSM, TEnum> activeState;


        protected virtual void Awake()
        {
            for (int i = 0; i < availableStates.Count; i++)
            {
                availableStates[i].Init((TFSM)this);
            }
        }


        public void ChangeState(TEnum changeStateToEnum)
        {
            if (activeState != null && activeState.StateEnum.Equals(changeStateToEnum))
            {
                Debug.Log(" <color=red>Already on same State</color>");
                return;
            }
            activeState?.Exit();
            activeState = availableStates.Find(state => state.StateEnum.Equals(changeStateToEnum));
            activeState?.Enter();
        }
    }
}
