using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mini_Vampire_Surviours.Gameplay.LevelUpSystem
{
    public class Rect_PowerUp : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txt_Type;
        [SerializeField] TextMeshProUGUI txt_Amount;
        [SerializeField] TextMeshProUGUI txt_Discription;
        [SerializeField] Button btn_Select; 
        public void Set_UI(string type , string amount , string dis , System.Action onClick)
        {
            txt_Type.text = type;
            txt_Amount.text = amount;
            txt_Discription.text = dis;
            btn_Select.onClick.RemoveAllListeners();
            btn_Select.onClick.AddListener(()=> onClick());

        }
    }
}