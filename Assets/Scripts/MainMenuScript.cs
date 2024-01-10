using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnPlayPressed()
    {
        SceneManager.LoadScene("MainGameScene");
    }
    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
