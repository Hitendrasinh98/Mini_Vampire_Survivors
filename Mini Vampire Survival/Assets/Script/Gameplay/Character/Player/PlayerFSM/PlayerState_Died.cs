using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{
    public class PlayerState_Died : State<PlayerFSM,PlayerStateEnum>
    {
        public override void Enter()
        {
            fsm.animator.SetAnimatorBoolKey(AnimatorParameterKeyEnum.IsDied , true);
        }

        public override void Exit()
        {

        }


    }
}