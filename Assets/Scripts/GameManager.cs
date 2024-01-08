using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardPropertiesScript;

public class GameManager : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private CardPropertiesScript cardPropertiesScript;
    [SerializeField] private PlayerHandScript playerHandScript;
    [SerializeField] private UIManager uiManager;
    [Space]
    [Header("Object Reference")]
    [SerializeField] private GameObject mainCard;
    [Space]
    [Header("Variables")]
    [SerializeField] private float drawWaitTime;

    // private variables
    private CardData currentCard;
    private int score = 0;
    private const int scoreToWin = 10;

    private void Start()
    {
        currentCard = cardPropertiesScript.GetRandomCard();
        SetMainCard();
    }

    void SetMainCard()
    {
        mainCard.GetComponent<CardScript>().Init(playerHandScript, currentCard);
    }

    public IEnumerator CompareCards(CardData _selectedCardData)
    {
        int matchScore = 0;

        foreach(CardProperty selectedCardProperty in _selectedCardData.cardProperties)
        {
            foreach (CardProperty currentCardProperty in currentCard.cardProperties)
            {
                if (selectedCardProperty == currentCardProperty)
                {
                    Debug.Log($"Matched property! {selectedCardProperty.ToString()}");
                    matchScore++;
                }
            }
        }
        if(_selectedCardData.cardName.Substring(0) == currentCard.cardName.Substring(0))
        {
            Debug.Log($"Matched first letter!");
            matchScore++;
        }
        if(_selectedCardData.cardName.Length == currentCard.cardName.Length)
        {
            Debug.Log($"Matched name length!");
            matchScore++;
        }

        score += matchScore;

        uiManager.SetScoreText(score, matchScore);

        if(score >= scoreToWin)
        {
            Debug.Log("You Win!");
        }

        yield return new WaitForSeconds(0.5f);

        currentCard = _selectedCardData;
        SetMainCard();

        yield return new WaitForSeconds(drawWaitTime);
        playerHandScript.DrawCard();
    }
}
