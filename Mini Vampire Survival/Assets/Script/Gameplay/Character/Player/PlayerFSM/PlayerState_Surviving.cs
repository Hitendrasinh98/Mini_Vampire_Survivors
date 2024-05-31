using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{
    public class PlayerState_Surviving : Core.State<PlayerFSM,PlayerStateEnum>
    {
        Vector2 inputDirection;

        public override void Enter()
        {

        }

        public override void Exit()
        {

        }

        public override void GameUpdate()
        {
            MovePlayer();
        }


        void MovePlayer()
        {
            inputDirection.x = Input.GetAxis("Horizontal");
            inputDirection.y = Input.GetAxis("Vertical");
            fsm.m_Movement.Move(inputDirection);
        }


        
        [SerializeField] string debug_AnimName;
        [ContextMenu("Check Animaiton")]
        public void Debug_PlayAnimation()
        {
            fsm.animator.Play(debug_AnimName, 0, 0);
        }


    }
}