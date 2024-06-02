using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    public class Panel_LevelUp : Base_UiPage
    {
        [SerializeField] List<LevelUpSystem.Rect_PowerUp> rect_PowerUps = new List<LevelUpSystem.Rect_PowerUp>();

        public void Set_UI(List<LevelUpSystem.LevelUpPower> levelUpPowerConfig)
        {
            for (int i = 0; i < levelUpPowerConfig.Count; i++)
            {
                string type = levelUpPowerConfig[i].powerType.ToString();
                string amount = levelUpPowerConfig[i].amount.ToString();
                string discription = levelUpPowerConfig[i].discription;
                int index = i;
                void OnClick_Select()
                {
                    UISystem.UIManager.Instance.HidePage(UISystem.UIPageIDEnum.LevelUp);
                    Core.EventManager.Instance.OnPowerUpSelect(levelUpPowerConfig[index].powerType, levelUpPowerConfig[index].amount);
                    Time.timeScale = 1;
                }

                if(i >= rect_PowerUps.Count)
                {
                    Debug.LogError("We need to add more Rect PowerUps");
                    continue;
                }
                rect_PowerUps[i].Set_UI(type, amount, discription, OnClick_Select);
            }
        }
    }
}
