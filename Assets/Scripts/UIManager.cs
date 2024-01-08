using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    // public variables
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI matchScoreText;

    private void Start()
    {
        SetScoreText(0, 0);
    }

    public void SetScoreText(int _score, int _matchScore)
    {
        totalScoreText.text = "Total score: " + _score;
        matchScoreText.text = "Match score: " + _matchScore;
    }
}