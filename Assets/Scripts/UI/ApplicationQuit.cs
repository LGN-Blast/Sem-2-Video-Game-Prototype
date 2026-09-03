using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationQuit : MonoBehaviour
{
    public void QuitToIntro()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}

