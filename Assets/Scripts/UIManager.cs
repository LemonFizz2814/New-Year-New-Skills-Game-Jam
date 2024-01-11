using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private GameManager gameManager;
    [Space]
    [Header("Text Reference")]
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI aiScoreText;
    [SerializeField] private TextMeshProUGUI matchScoreText;
    [SerializeField] private TextMeshProUGUI matchedText;
    [SerializeField] private TextMeshProUGUI scoreToWinText;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private Slider aiScoreSlider;
    [Space]
    [Header("Object Reference")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject aiTurnScreen;
    [SerializeField] private GameObject yourTurnScreen;
    [Space]
    [Header("Variables")]
    [SerializeField] private float matchTextWait;

    private void Start()
    {
        scoreSlider.maxValue = gameManager.GetScoreToWin();
        aiScoreSlider.maxValue = gameManager.GetScoreToWin();
        scoreToWinText.text = gameManager.GetScoreToWin() + " to win";
        SetScoreText(0, 0, 0, new List<string>());
        winScreen.SetActive(false);

        aiTurnScreen.SetActive(true);
        yourTurnScreen.SetActive(true);
    }

    public void SetScoreText(int _score, int _aiScore, int _matchScore, List<string> _matchedText)
    {
        playerScoreText.text = "Score: " + _score;
        aiScoreText.text     = "Score: " + _aiScore;
        scoreSlider.value    = _score;
        aiScoreSlider.value  = _aiScore;

        matchScoreText.text = "+" + _matchScore;

        StartCoroutine(DisplayMatchedText(_matchedText));
    }

    IEnumerator DisplayMatchedText(List<string> _matchedText)
    {
        matchedText.GetComponent<Animator>().SetTrigger("Show");
        matchedText.text = "";

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
    public void DisplayWinScreen()
    {
        winScreen.SetActive(true);
    }
    public void DisplayGameoverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void OnEndTurnButtonPressed()
    {
        gameManager.TurnOver();
    }
}