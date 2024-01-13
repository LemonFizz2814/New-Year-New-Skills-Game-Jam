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
    private List<string> matchedString = new List<string>();
    private GameManager gameManager;
    private int score;
    private int matchScore;
    private CardData selectedCard;

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
        matchScore = 0;
        matchedString.Clear();

        selectedCard = _currentCardData;

        yield return new WaitForSeconds(turnTime);

        while (CheckCards(_currentCardData))
        {
            gameManager.AISetMainCard(selectedCard);

            uiManager.SetAIScoreText(score, matchScore, matchedString);
            yield return new WaitForSeconds(turnTime);
        }

        if (score >= gameManager.GetScoreToWin())
        {
            uiManager.DisplayGameoverScreen(gameManager.GetScore(), score);
        }
        else
        {
            gameManager.TurnStart(selectedCard);
        }
    }

    bool CheckCards(CardData _currentCardData)
    {
        foreach(CardData cardData in cards)
        {
            foreach (CardProperty selectedCardProperty in cardData.cardProperties)
            {
                foreach (CardProperty currentCardProperty in _currentCardData.cardProperties)
                {
                    if (selectedCardProperty == currentCardProperty)
                    {
                        matchScore = GetCardScore(cardData.cardProperties, _currentCardData.cardProperties);
                        score += matchScore;
                        selectedCard = cardData;
                        return true;
                    }
                }
            }
        }

        Debug.Log("Couldn't find any");
        return false;
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
                    matchedString.Add(selectedCardProperty.ToString() + "! +1 \n");
                    count++;
                }
            }
        }

        return count;
    }

    public int GetScore()
    {
        return score;
    }
}
