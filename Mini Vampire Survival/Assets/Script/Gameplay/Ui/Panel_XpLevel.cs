using Mini_Vampire_Surviours.Gameplay.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mini_Vampire_Surviours.Gameplay.UI
{
    public class Panel_XpLevel : MonoBehaviour
    {
        [SerializeField] Slider slider_XpLevel;
        [SerializeField] TextMeshProUGUI txt_health;        

        private void Awake()
        {
            EventManager.Instance.AddObserver_OnXPGemCollect(OnXpGemCollect);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveObserver_OnXPGemCollect(OnXpGemCollect);
        }


        void OnXpGemCollect( int currentXpGems , int requirementGems , int currentLevel)
        {
            Update_XpLevelUI(currentXpGems, requirementGems);
            txt_health.text ="Lvl "+ (currentLevel+1).ToString();
        }


        /// <summary>
        /// Will used to update fillbar and text for player health
        /// </summary>
        /// <param name="currentHealth"></param>
        /// <param name="maxHealth"></param>
        void Update_XpLevelUI(int currentXpGems, int requirementGems)
        {
            float normalizeValue = Mathf.Clamp01(currentXpGems / (float)requirementGems);
            slider_XpLevel.value = normalizeValue;
        }


    }
}
