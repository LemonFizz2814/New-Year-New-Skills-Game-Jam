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

    private void Start()
    {
        SetScoreText(0, 0, "");
        winScreen.SetActive(false);
    }

    public void SetScoreText(int _score, int _matchScore, string _matchedText)
    {
        totalScoreText.text = "Total score: " + _score;
        matchScoreText.text = "Match score: " + _matchScore;
        matchedText.text = _matchedText;
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