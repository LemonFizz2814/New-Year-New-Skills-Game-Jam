using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIScript : MonoBehaviour
{
    [SerializeField] private float turnTime;

    private GameManager gameManager;
    private int score;

    public void Init(GameManager _gameManager)
    {
        gameManager = _gameManager;
    }

    public IEnumerator AIsTurn()
    {
        score += Random.Range(0, 4);
        yield return new WaitForSeconds(turnTime);
        gameManager.TurnStart();
    }
}
