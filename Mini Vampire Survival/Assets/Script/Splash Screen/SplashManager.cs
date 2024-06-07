using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mini_Vampire_Surviours.Splash
{
    /// <summary>
    /// handle the Splash Screen
    /// currenlly just load the gamescene on buttonclick
    /// </summary>
    public class SplashManager : MonoBehaviour
    {
        public void OnClick_Start()
        {
            SceneManager.LoadScene(1);
        }
    }
}
