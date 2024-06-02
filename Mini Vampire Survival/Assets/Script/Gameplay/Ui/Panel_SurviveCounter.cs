using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    public class Panel_SurviveCounter : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt_Survived;
        string totalSurviedInString;
        int minutes, seconds;

        private void Awake()
        {
            txt_Survived.gameObject.SetActive(false);
            Core.EventManager.Instance.AddObserver_OnGameStart(OnGameStart);
            StatesSystem.StatesManager.Instance.AddObserver_OnSurviveTimeIncrease(Update_TotalSurviveUi);    
        }


        private void OnDestroy()
        {
            Core.EventManager.Instance.RemoveObserver_OnGameStart(OnGameStart);
            StatesSystem.StatesManager.Instance.RemoveObserver_OnSurviveTimeIncrease(Update_TotalSurviveUi);
        }
        void OnGameStart(Core.EventManager.GameStartData gameStartData)
        {
            txt_Survived.gameObject.SetActive(true);
        }

        void Update_TotalSurviveUi(int totalSurvived)
        {
            minutes = Mathf.FloorToInt(totalSurvived / 60F);
            seconds = Mathf.FloorToInt(totalSurvived % 60F);
            totalSurviedInString = string.Format("{0:00}:{1:00}", minutes, seconds);
            txt_Survived.text = totalSurviedInString;
        }
    }
}