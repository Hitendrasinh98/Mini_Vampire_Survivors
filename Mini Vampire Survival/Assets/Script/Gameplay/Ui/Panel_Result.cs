using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    public class Panel_Result : Base_UiPage
    {
        [SerializeField] GameObject go_WON;
        [SerializeField] GameObject go_GameOver;
        
        [Space(10)]
        [SerializeField] TextMeshProUGUI txt_Survived;
        [SerializeField] TextMeshProUGUI txt_Killed;

        public override void ShowPage()
        {
            Set_Ui();
            base.ShowPage();
        }

        public void Set_Ui()
        {
            int totalSurvived = StatesSystem.StatesManager.Instance.totalSurvived;
            int totalKilled  = StatesSystem.StatesManager.Instance.totalKilled;
            bool isWon = StatesSystem.StatesManager.Instance.IsWon;

            go_WON.SetActive(isWon);
            go_GameOver.SetActive(!isWon);

            int minutes = (int)(totalSurvived / 60f);
            int seconds = (int)(totalSurvived % 60f);
            txt_Survived.text = minutes.ToString() + ":" + seconds.ToString();
            txt_Killed.text = totalKilled.ToString();
        }


        public void OnClick_Restart()
        {
            SceneManager.LoadSceneAsync(1);
        }

        public void OnClick_Home()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
