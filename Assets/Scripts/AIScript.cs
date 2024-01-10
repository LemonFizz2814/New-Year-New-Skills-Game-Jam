using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    private GameManager gameManager;
    private int score;

    public void Init(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public void AIsTurn()
    {
        score += Random.Range(0, 4);
    }
}
