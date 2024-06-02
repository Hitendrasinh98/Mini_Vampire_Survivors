using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.LevelUpSystem
{
    public class XpGem : MonoBehaviour
    {
        [SerializeField] ConfigData.XPGemTypeEnum xpGemType;


        public void Init(ConfigData.XPGemTypeEnum xpGemType)
        {
            this.xpGemType = xpGemType; 
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                XpGemCollectedByPlayer();
            }
        }



        void XpGemCollectedByPlayer()
        {
            Mediator.Instance.m_XPLevelManager.OnCollectGem(xpGemType);
            Destroy(gameObject);
        }
    }
}
