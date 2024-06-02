using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mini_Vampire_Surviours.Gameplay.UISystem
{
    [DefaultExecutionOrder(-1)]
    public class UIManager : MonoBehaviour
    {
        static UIManager instance;
        public static UIManager Instance => instance;

        [SerializeField] List<Base_UiPage> avaialblePages = new List<Base_UiPage>();

        [Header("Current Progress")]
        [SerializeField] Base_UiPage activePage;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                DestroyImmediate(gameObject);

            Initalize();
        }

        void Initalize()
        {
            for (int i = 0; i < avaialblePages.Count; i++)
            {
                avaialblePages[i].init();
            }
            
        }




        public void ShowPage(UIPageIDEnum  pageId)
        {
            for (int i = 0; i < avaialblePages.Count; i++)
            {
                if(avaialblePages[i].PageID == pageId)
                {
                    avaialblePages[i].ShowPage();
                }
            }
        }

        public void HidePage(UIPageIDEnum pageId)
        {
            for (int i = 0; i < avaialblePages.Count; i++)
            {
                if (avaialblePages[i].PageID == pageId)
                {
                    avaialblePages[i].HidePage();
                }
            }
        }

        public Base_UiPage Get_UIPage (UIPageIDEnum pageId) 
        {
            for (int i = 0; i < avaialblePages.Count; i++)
            {
                if (avaialblePages[i].PageID == pageId)
                    return avaialblePages[i];
            }
            return null;
        }
    }
}
