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
    [SerializeField] private AIScript aiScript;
    [Space]
    [Header("Object Reference")]
    [SerializeField] private Transform mainCardPos;
    [Space]
    [Header("Variables")]
    [SerializeField] private float drawWaitTime;

    // private variables
    private GameObject mainCardObj;
    private CardData currentCardData;
    private bool playersTurn;
    private int score = 0;
    private const int scoreToWin = 50;

    private void Start()
    {
        mainCardObj = mainCardPos.GetChild(0).gameObject;
        mainCardObj.GetComponent<CardScript>().SetIsInHand(false);

        TurnStart();

        aiScript.Init(this);
    }

    public void SetMainCard(GameObject _cardObj)
    {
        mainCardObj = _cardObj;
        currentCardData = mainCardObj.GetComponent<CardScript>().GetCardData();
        mainCardObj.GetComponent<CardScript>().Init(playerHandScript, this, currentCardData);
        mainCardObj.GetComponent<Animator>().SetTrigger("SetMainCard");
    }

    public void TurnOver()
    {
        playersTurn = false;
        StartCoroutine(aiScript.AIsTurn());
    }
    public void TurnStart()
    {
        playersTurn = true;

        mainCardObj.GetComponent<CardScript>().SetCardData(cardPropertiesScript.GetRandomCard());
        SetMainCard(mainCardObj);
    }

    public IEnumerator CompareCards(CardData _selectedCardData, GameObject _cardObj)
    {
        int matchScore = 0;
        List<string> matchedString = new List<string>();

        foreach(CardProperty selectedCardProperty in _selectedCardData.cardProperties)
        {
            foreach (CardProperty currentCardProperty in currentCardData.cardProperties)
            {
                if (selectedCardProperty == currentCardProperty)
                {
                    matchedString.Add(selectedCardProperty.ToString() + "! +1\n");
                    matchScore++;
                }
            }
        }
        /*if(_selectedCardData.cardName.Substring(0) == currentCard.cardName.Substring(0))
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
        }*/

        // punish if no matches found
        if(matchScore == 0)
        {
            matchScore = -5;
            matchedString.Add("No matches found! -5");
        }

        score += matchScore;

        uiManager.SetScoreText(score, 0, matchScore, matchedString);
        uiManager.AnimateScore();

        if(score >= scoreToWin)
        {
            uiManager.DisplayWinScreen();
        }

        yield return new WaitForSeconds(0.4f);

        currentCardData = _selectedCardData;

        yield return new WaitForSeconds(drawWaitTime);
        playerHandScript.DrawCard();
    }

    public Transform GetMainCardPos()
    {
        return mainCardPos;
    }
    public GameObject GetMainCard()
    {
        return mainCardObj;
    }
    public bool GetPlayersTurn()
    {
        return playersTurn;
    }
    public int GetScoreToWin()
    {
        return scoreToWin;
    }
}
