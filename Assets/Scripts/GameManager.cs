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
    [SerializeField] private Transform mainCardPos;
    [Space]
    [Header("Variables")]
    [SerializeField] private float drawWaitTime;

    // private variables
    private GameObject mainCard;
    private CardData currentCard;
    private int score = 0;
    private const int scoreToWin = 50;

    private void Start()
    {
        currentCard = cardPropertiesScript.GetRandomCard();
        SetMainCard(mainCardPos.GetChild(0).gameObject);
    }

    public void SetMainCard(GameObject _cardObj)
    {
        mainCard = _cardObj;
        mainCard.GetComponent<CardScript>().Init(playerHandScript, currentCard, false);
    }

    public IEnumerator CompareCards(CardData _selectedCardData, GameObject _cardObj)
    {
        int matchScore = 0;
        string matchedString = "";

        foreach(CardProperty selectedCardProperty in _selectedCardData.cardProperties)
        {
            foreach (CardProperty currentCardProperty in currentCard.cardProperties)
            {
                if (selectedCardProperty == currentCardProperty)
                {
                    Debug.Log($"Matched property! {selectedCardProperty.ToString()}");
                    matchedString += selectedCardProperty.ToString() + "! +1\n";
                    matchScore++;
                }
            }
        }
        if(_selectedCardData.cardName.Substring(0) == currentCard.cardName.Substring(0))
        {
            Debug.Log($"Matched first letter!");
            matchedString += "First letter! +1\n";
            matchScore++;
        }
        if(_selectedCardData.cardName.Length == currentCard.cardName.Length)
        {
            Debug.Log($"Matched name length!");
            matchedString += "Name length! +1\n";
            matchScore++;
        }
        if(_selectedCardData.cardSymbol == currentCard.cardSymbol)
        {
            Debug.Log($"Matched card symbols!");
            matchedString += "Card symbols! +1\n";
            matchScore++;
        }

        score += matchScore;

        uiManager.SetScoreText(score, matchScore, matchedString);
        uiManager.AnimateScore();

        if(score >= scoreToWin)
        {
            Debug.Log("You Win!");
            uiManager.DisplayWinScreen();
        }

        yield return new WaitForSeconds(0.4f);

        currentCard = _selectedCardData;

        yield return new WaitForSeconds(drawWaitTime);
        playerHandScript.DrawCard();
    }

    public Transform GetMainCardPos()
    {
        return mainCardPos;
    }
}
