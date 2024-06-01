using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.PlayerSystem
{
    public class PlayerState_Idle : Core.State<PlayerFSM, PlayerStateEnum>
    {
        
        public override void Enter()
        {
            fsm.animator.ResetAnimator();
            fsm.animator.Play(AnimNameEnum.Locomotion.ToString(), 0, 0);
            fsm.m_Movement.Set_FaceFlipped(false);
            Invoke(nameof(Invoke_ChangeStateToSurvive), 1);            
        }

        void Invoke_ChangeStateToSurvive()
        {
            fsm.ChangeState(PlayerStateEnum.surviving);
        }

        public override void Exit()
        {

        }
    }
}