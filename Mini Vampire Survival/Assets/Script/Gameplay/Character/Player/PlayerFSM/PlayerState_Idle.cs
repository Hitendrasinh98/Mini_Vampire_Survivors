using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{
    public class PlayerState_Idle : Core.State<PlayerFSM, PlayerStateEnum>
    {
        
        public override void Enter()
        {
            fsm.player.animator.ResetAnimator();
            fsm.player.animator.Play(AnimNameEnum.Idle.ToString(), 0, 0);
        }

        public override void Exit()
        {

        }
    }
}