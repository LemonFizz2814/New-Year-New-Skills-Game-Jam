using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardPropertiesScript;

public class AIScript : MonoBehaviour
{
    [SerializeField] private float turnTime;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private CardPropertiesScript cardPropertiesScript;

    private List<CardData> cards = new List<CardData>();
    private GameManager gameManager;
    private int score;

    public void Init(GameManager _gameManager)
    {
        gameManager = _gameManager;

        for (int i = 0; i < 5; i++)
        {
            cards.Add(cardPropertiesScript.GetRandomCard());
        }
    }

    public IEnumerator AIsTurn(CardData _currentCardData)
    {
        Debug.Log("AI's turn");

        yield return new WaitForSeconds(turnTime);
        CardData selectedCard = CheckCards(_currentCardData);
        yield return new WaitForSeconds(1.0f);

        uiManager.SetAIScoreText(score);

        if (score >= gameManager.GetScoreToWin())
        {
            uiManager.DisplayGameoverScreen();
        }
        else
        {
            gameManager.TurnStart(selectedCard);
        }
    }

    CardData CheckCards(CardData _currentCardData)
    {
        foreach(CardData cardData in cards)
        {
            foreach (CardProperty selectedCardProperty in cardData.cardProperties)
            {
                foreach (CardProperty currentCardProperty in _currentCardData.cardProperties)
                {
                    if (selectedCardProperty == currentCardProperty)
                    {
                        score += GetCardScore(cardData.cardProperties, _currentCardData.cardProperties);
                        return cardData;
                    }
                }
            }
        }

        Debug.Log("Couldn't find any");
        return _currentCardData;
    }

    int GetCardScore(CardProperty[] _cardData, CardProperty[] _currentCardData)
    {
        int count = 0;
        foreach (CardProperty selectedCardProperty in _cardData)
        {
            foreach (CardProperty currentCardProperty in _currentCardData)
            {
                if (selectedCardProperty == currentCardProperty)
                {
                    count++;
                }
            }
        }

        return count;
    }
}
