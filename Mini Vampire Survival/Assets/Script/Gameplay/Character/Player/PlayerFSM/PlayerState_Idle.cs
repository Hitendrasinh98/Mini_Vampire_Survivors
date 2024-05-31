using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{
    public class PlayerState_Idle : Core.State<PlayerFSM, PlayerStateEnum>
    {
        
        public override void Enter()
        {
            fsm.animator.ResetAnimator();
            fsm.animator.Play(AnimNameEnum.Locomotion.ToString(), 0, 0);
            fsm.player.FlipSpirtes(false);
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