using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mini_Vampire_Surviours.Gameplay.UI
{
    public class Panel_PlayerHealth : MonoBehaviour
    {
        [SerializeField] Image fillbar_Health;
        [SerializeField] TextMeshProUGUI txt_health;


        private void Awake()
        {
            EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            EventManager.Instance.AddObserver_OnPlayerHit(OnPlayerGotHit);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            EventManager.Instance.RemoveObserver_OnPlayerHit(OnPlayerGotHit);
        }

        void OnGameStart(EventManager.GameStartData gameStartData)
        {
            Update_HelathUI(gameStartData.MaxHealth, gameStartData.MaxHealth);
        }

        void OnPlayerGotHit(float damazeTook , float currentHealth , int maxHealth)
        {
            Update_HelathUI(currentHealth, maxHealth);
        }


        /// <summary>
        /// Will used to update fillbar and text for player health
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="maxHealth"></param>
        void Update_HelathUI( float currentHealth ,int maxHealth)
        {
            float normalizeValue = Mathf.Clamp01(currentHealth / (float)maxHealth);
            fillbar_Health.fillAmount = normalizeValue;
            txt_health.text = currentHealth.ToString() +"/"+ maxHealth.ToString();
        }

      
    }
}