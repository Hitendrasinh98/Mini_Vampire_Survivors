using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.Player
{
    public enum PlayerStateEnum { None ,Idle ,surviving ,Die };
    public enum AnimatorParameterKeyEnum { IsMirrored};
    public enum AnimNameEnum { Idle};

    public class PlayerFSM : Base_FSM<PlayerFSM,PlayerStateEnum>
    {
        static readonly string ChannelKey = "[PlayerFsM] ";  //will be used to fillter logs 
        [field: SerializeField,Space(10)] public Player player { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            ChangeState(PlayerStateEnum.Idle);
        }

    }
}