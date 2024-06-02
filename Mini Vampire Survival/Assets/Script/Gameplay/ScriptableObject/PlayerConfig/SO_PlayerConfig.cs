using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mini_Vampire_Surviours.Gameplay.ConfigData
{
    [CreateAssetMenu(fileName ="So Player Config" , menuName ="Gameplay/PlayerConfig")]
    public class SO_PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public int PlayerMaxHealth { get; private set; }
        [field: SerializeField] public int StartXpLevel { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public int targetSurviveTime { get; private set; }//InSeconds
        [field: SerializeField] public WeaponSystem.WeaponTypeEnum primaryWeapon { get; private set; }
    }
}
