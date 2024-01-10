using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private GameManager gameManager;
    [Space]
    [Header("Text Reference")]
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI matchScoreText;
    [SerializeField] private TextMeshProUGUI matchedText;
    [Space]
    [Header("Object Reference")]
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameOverScreen;
    [Space]
    [Header("Variables")]
    [SerializeField] private float matchTextWait;

    private void Start()
    {
        SetScoreText(0, 0, new List<string>());
        winScreen.SetActive(false);
    }

    public void SetScoreText(int _score, int _matchScore, List<string> _matchedText)
    {
        totalScoreText.text = "Score: " + _score;
        matchScoreText.text = "Match score: " + _matchScore;

        StartCoroutine(DisplayMatchedText(_matchedText));
    }

    IEnumerator DisplayMatchedText(List<string> _matchedText)
    {
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

    public void DisplayWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void OnEndTurnButtonPressed()
    {

    }
}