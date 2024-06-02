using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mini_Vampire_Surviours.Splash
{
    public class SplashManager : MonoBehaviour
    {
        public void OnClick_Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}
