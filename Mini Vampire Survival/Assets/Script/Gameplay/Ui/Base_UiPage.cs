using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    public enum UIPageIDEnum { None ,GameHud ,LevelUp ,Result };
    public abstract class Base_UiPage : MonoBehaviour, IUiPage
    {
        [SerializeField] UIPageIDEnum pageID;

        public UIPageIDEnum PageID => pageID;


        public virtual void init()
        {
            gameObject.SetActive(false);
        }

        public virtual void DeActivate()
        {
            
        }

        public virtual void HidePage()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void ShowPage()
        {
            gameObject.SetActive(true);
        }
    }
}
