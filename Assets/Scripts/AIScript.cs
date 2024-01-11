using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    [SerializeField] private float turnTime;
    [SerializeField] private UIManager uiManager;

    private GameManager gameManager;
    private int score;

    public void Init(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public IEnumerator AIsTurn()
    {
        Debug.Log("AI's turn");

        score += Random.Range(0, 4);
        yield return new WaitForSeconds(turnTime);

        uiManager.SetAIScoreText(score);

        if (score >= gameManager.GetScoreToWin())
        {
            uiManager.DisplayGameoverScreen();
        }
        else
        {
            gameManager.TurnStart();
        }
    }
}
