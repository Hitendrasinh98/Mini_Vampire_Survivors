using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    public class Text_TotlaKilled : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt_TotalKilled;

        private void Awake()
        {
            OnTotalKilledIncrease(0);
            StatesSystem.StatsManager.Instance.AddObserver_OnTotalKilledIncrease(OnTotalKilledIncrease);
        }
        private void OnDestroy()
        {
            StatesSystem.StatsManager.Instance.RemoveObserver_OnTotalKilledIncrease(OnTotalKilledIncrease);
        }

        void OnTotalKilledIncrease(int totalKilled)
        {
            txt_TotalKilled.text = totalKilled.ToString();
        }
    }
}