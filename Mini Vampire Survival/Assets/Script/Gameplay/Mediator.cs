using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay
{
    /// <summary>
    /// This is a Mediator for all the systems communication    
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class Mediator : MonoBehaviour
    {
        static Mediator instance;
        public static Mediator Instance => instance;


        [field: SerializeField] public GameManager m_GameManager;
        [field: SerializeField] public LevelUpSystem.XPLevelManager m_XPLevelManager;
        [field: SerializeField] public PlayerSystem.Player m_Player;





        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);
        }

    }
}
