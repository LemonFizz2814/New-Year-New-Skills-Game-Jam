using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hoverSound;

    private void Start()
    {
        //Screen.SetResolution(1600, 900, true);
        PlayerPrefs.SetInt("Difficulty", 0);
    }

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

    public void DifficultyChange(TMP_Dropdown change)
    {
        PlayerPrefs.SetInt("Difficulty", change.value);
        Debug.Log($"change.value {change.value}, {PlayerPrefs.GetInt("Difficulty")}");
    }
}
