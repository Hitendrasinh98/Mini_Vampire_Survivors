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
            EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            EventManager.Instance.AddObserver_OnXPGemCollect(OnXpGemCollect);
            EventManager.Instance.AddObserver_OnXpLevelUP(OnXpLevelUp);
        }

        private void OnDestroy()
        {
            EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            EventManager.Instance.RemoveObserver_OnXPGemCollect(OnXpGemCollect);
            EventManager.Instance.RemoveObserver_OnXpLevelUP(OnXpLevelUp);
        }


        void OnGameStart(EventManager.GameStartData gameStartData)
        {
            OnXpLevelUp(gameStartData.XPLevel);
        }

        void OnXpLevelUp(int currentLevel)
        {
            txt_health.text = "Lvl " + (currentLevel + 1).ToString();
        }

        void OnXpGemCollect( int currentXpGems , int requirementGems )
        {
            Update_XpLevelUI(currentXpGems, requirementGems);
        }


        /// <summary>
        /// Will used to update fillbar and text for player XpLevel
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
