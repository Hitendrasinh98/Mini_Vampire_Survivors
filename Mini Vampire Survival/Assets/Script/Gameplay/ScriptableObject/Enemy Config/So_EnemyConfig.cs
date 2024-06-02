using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.ConfigData
{
    [CreateAssetMenu(fileName = "So Enemy Config ", menuName = "Gameplay/Enemy Config")]
    public class So_EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public int maxHealth { get; private set; }
        [field: SerializeField] public float moveSpeed { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [field: SerializeField] public float AttackRate { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
    }
}
