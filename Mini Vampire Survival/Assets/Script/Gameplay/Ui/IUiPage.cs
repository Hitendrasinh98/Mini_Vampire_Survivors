using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    public interface IUiPage
    {
        UIPageIDEnum PageID { get; }
        void init();
        void ShowPage();
        void HidePage();
        void DeActivate();
    }
}
