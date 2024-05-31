using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{
    public class PlayerState_Surviving : Core.State<PlayerFSM,PlayerStateEnum>
    {
        [SerializeField] float maxBlendAmount_walk;
        [SerializeField] float maxBlendAmount_Run;
        [SerializeField] float lerpSpeed_BlendMove;

        [Header("Current Progress")]
        [SerializeField] Vector2 direction;
        [SerializeField] float anim_BlendMoveSpeed;

        bool isRunning;
        bool isMirrored;
        float blendAmount;

        public override void Enter()
        {
            isMirrored = false;
            fsm.player.FlipSpirtes(isMirrored);
        }

        public override void Exit()
        {

        }

        public override void GameUpdate()
        {
            GetInput();
            MovePlayer();
        }


        void GetInput()
        {
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            isRunning = Input.GetKey(KeyCode.LeftShift);
            if(direction.x < 0 != isMirrored && direction.magnitude != 0)
            {
                isMirrored = !isMirrored;
                fsm.player.FlipSpirtes(isMirrored);
            }

            blendAmount = direction.magnitude > 0 ? 1  :0;
            blendAmount *= isRunning ? 2 : 1;

            anim_BlendMoveSpeed =Mathf.Lerp(anim_BlendMoveSpeed,  blendAmount,Time.deltaTime * lerpSpeed_BlendMove );
        }


        void MovePlayer()
        {
            fsm.animator.SetAnimatorFloatKey(AnimatorParameterKeyEnum.MoveSpeed, anim_BlendMoveSpeed);
        }

        

        [SerializeField] string debug_AnimName;
        [ContextMenu("Check Animaiton")]
        public void Debug_PlayAnimation()
        {
            fsm.animator.Play(debug_AnimName, 0, 0);
        }
    }
}