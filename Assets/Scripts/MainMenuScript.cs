using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;


    public void OnPlayPressed()
    {
        SceneManager.LoadScene("MainGameScene");
    }
    public void OnQuitPressed()
    {
        Application.Quit();
    }

    public void ButtonHover()
    {
        audioSource.PlayOneShot(hoverSound);
    }
}
