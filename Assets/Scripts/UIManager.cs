using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using static SoundManager;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    [Space]
    [Header("Text Reference")]
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI aiScoreText;
    [SerializeField] private TextMeshProUGUI matchScoreText;
    [SerializeField] private TextMeshProUGUI matchedText;
    [SerializeField] private TextMeshProUGUI scoreToWinText;
    [SerializeField] private TextMeshProUGUI winFinalScoreText;
    [SerializeField] private TextMeshProUGUI gameoverFinalScoreText;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private Slider aiScoreSlider;
    [Space]
    [Header("Object Reference")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject aiTurnScreen;
    [SerializeField] private GameObject yourTurnScreen;
    [SerializeField] private GameObject instructionsScreen;
    [SerializeField] private GameObject endTurnButton;
    [Space]
    [Header("Variables")]
    [SerializeField] private float matchTextWait;

    private void Start()
    {
        scoreSlider.maxValue = gameManager.GetScoreToWin();
        aiScoreSlider.maxValue = gameManager.GetScoreToWin();
        scoreToWinText.text = gameManager.GetScoreToWin() + " to win";
        SetScoreText(0, 0, new List<string>());
        SetAIScoreText(0, 0, new List<string>());

        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        aiTurnScreen.SetActive(true);
        yourTurnScreen.SetActive(true);
        instructionsScreen.SetActive(true);
    }

    public void SetScoreText(int _score, int _matchScore, List<string> _matchedText)
    {
        playerScoreText.text = "Score: " + _score;
        scoreSlider.value    = _score;

        StartCoroutine(DisplayMatchedText(_matchedText, _matchScore));
    }

    public void SetAIScoreText(int _aiScore, int _matchScore, List<string> _matchedText)
    {
        aiScoreText.text = "Score: " + _aiScore;
        aiScoreSlider.value = _aiScore;

        StartCoroutine(DisplayMatchedText(_matchedText, _matchScore));
    }

    IEnumerator DisplayMatchedText(List<string> _matchedText, int _matchScore)
    {
        matchScoreText.text = "+" + _matchScore;
        matchedText.GetComponent<Animator>().SetTrigger("Show");
        matchedText.text = "";

        Debug.Log($"_matchedText {_matchedText.Count}");

        for (int i = 0; i < _matchedText.Count; i++)
        {
            yield return new WaitForSeconds(matchTextWait);
            matchedText.text += _matchedText[i];
        }
    }

    public void AnimateScore()
    {
        matchScoreText.GetComponent<Animator>().SetTrigger("FadeIn");
    }

    public void ShowEndTurnButton(bool _show)
    {
        endTurnButton.SetActive(_show);
    }

    public void DisplayAITurnScreen()
    {
        aiTurnScreen.GetComponent<Animator>().SetTrigger("Show");
        //aiTurnScreen.SetActive(_show);
    }
    public void DisplayYourTurnScreen()
    {
        yourTurnScreen.GetComponent<Animator>().SetTrigger("Show");
        //yourTurnScreen.SetActive(_show);
    }
    public void DisplayWinScreen(int _playerScore, int _aiScore)
    {
        soundManager.PlaySound(SoundType.GameWon);
        winScreen.SetActive(true);
        winFinalScoreText.text = _playerScore + " - " + _aiScore;
    }
    public void DisplayGameoverScreen(int _playerScore, int _aiScore)
    {
        soundManager.PlaySound(SoundType.Gameover);
        gameOverScreen.SetActive(true);
        gameoverFinalScoreText.text = _playerScore + " - " + _aiScore;
    }

    public void OnEndTurnButtonPressed()
    {
        soundManager.PlaySound(SoundType.ButtonPressed);
        gameManager.TurnOver();
    }
    public void OnStartPressed()
    {
        soundManager.PlaySound(SoundType.ButtonPressed);
        instructionsScreen.SetActive(false);
        gameManager.BeginGame();
    }

    public void QuitPressed()
    {
        soundManager.PlaySound(SoundType.ButtonPressed);
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartPressed()
    {
        soundManager.PlaySound(SoundType.ButtonPressed);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}