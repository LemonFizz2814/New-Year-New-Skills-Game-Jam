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
    private int score = 0;
    private int matchScore = 0;
    private CardData selectedCard;
    private int[] totalCards = new int[2] { 5, 8 };
    private int difficulty;

    private void Awake()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");
    }

    public void Init(GameManager _gameManager)
    {
        gameManager = _gameManager;

        DrawCards();
    }

    public IEnumerator AIsTurn(CardData _currentCardData)
    {
        DrawCards();

        selectedCard = _currentCardData;

        yield return new WaitForSeconds(turnTime);

        int count = 0;

        while (count < 5 && CheckCards(selectedCard))
        {
            gameManager.AISetMainCard(selectedCard);

            uiManager.SetAIScoreText(score, matchScore, matchedString);

            yield return new WaitForSeconds(turnTime);

            matchScore = 0;
            matchedString.Clear();
            count++;
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

    void DrawCards()
    {
        int cardsTotal = totalCards[difficulty] - cards.Count;
        Debug.Log($"cardsTotal {cardsTotal}");

        for (int i = 0; i < cardsTotal; i++)
        {
            cards.Add(cardPropertiesScript.GetRandomCard());
        }

        for (int i = 0; i < cards.Count; i++)
        {
            Debug.Log($"card {i}: {cards[i].cardName}");
        }
    }

    bool CheckCards(CardData _currentCardData)
    {
        Debug.Log($"checking: ----------------------------");
        foreach (CardData cardData in cards)
        {
            Debug.Log($"card: {cardData.cardName}");
            foreach (CardProperty selectedCardProperty in cardData.cardProperties)
            {
                foreach (CardProperty currentCardProperty in _currentCardData.cardProperties)
                {
                    Debug.Log($"{selectedCardProperty} checking with {currentCardProperty}");

                    if (selectedCardProperty == currentCardProperty)
                    {
                        Debug.Log($"match found {cardData.cardName} and {_currentCardData.cardName}");
                        matchScore = GetCardScore(cardData.cardProperties, _currentCardData.cardProperties);
                        score += matchScore;
                        selectedCard = cardData;
                        cards.Remove(cardData);
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
