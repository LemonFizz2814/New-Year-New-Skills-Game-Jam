using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardPropertiesScript;
using static SoundManager;

public class GameManager : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private CardPropertiesScript cardPropertiesScript;
    [SerializeField] private PlayerHandScript playerHandScript;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AIScript aiScript;
    [SerializeField] private SoundManager soundManager;
    [Space]
    [Header("Object Reference")]
    [SerializeField] private Transform mainCardPos;
    //[Space]
    //[Header("Variables")]
    //[SerializeField] private float drawWaitTime;

    // private variables
    private GameObject mainCardObj;
    private CardData currentCardData;
    private List<CardProperty> previousCardProperties = new List<CardProperty>();

    private bool playersTurn;
    private bool gameOver = false;
    private int score = 0;
    private int[] scoreToWin = new int[2] { 25, 50 };
    private int difficulty;

    private void Awake()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty");
        Debug.Log($"Difficulty {PlayerPrefs.GetInt("Difficulty")}");
    }

    private void Start()
    {
        uiManager.ShowEndTurnButton(false);
        aiScript.Init(this);
    }

    public void BeginGame()
    {
        mainCardObj = mainCardPos.GetChild(0).gameObject;
        mainCardObj.GetComponent<CardScript>().SetIsInHand(false);

        TurnStart(cardPropertiesScript.GetRandomCard());
    }

    public void SetMainCard(GameObject _cardObj)
    {
        mainCardObj = _cardObj;
        currentCardData = mainCardObj.GetComponent<CardScript>().GetCardData();
        mainCardObj.GetComponent<CardScript>().Init(playerHandScript, this, soundManager, currentCardData);
        mainCardObj.GetComponent<Animator>().SetBool("SetMainCard", true);
    }

    public void TurnOver()
    {
        soundManager.PlaySound(SoundType.NextTurn);

        playersTurn = false;
        previousCardProperties.Clear();
        StartCoroutine(aiScript.AIsTurn(currentCardData));
        uiManager.DisplayAITurnScreen();
        uiManager.ShowEndTurnButton(false);
    }
    public void TurnStart(CardData _cardData)
    {
        soundManager.PlaySound(SoundType.NextTurn);

        playersTurn = true;

        mainCardObj.GetComponent<CardScript>().SetCardData(_cardData);
        SetMainCard(mainCardObj);

        StartCoroutine(playerHandScript.CheckDrawCards());
        uiManager.DisplayYourTurnScreen();
        uiManager.ShowEndTurnButton(true);
    }

    public void CompareCards(CardData _selectedCardData, GameObject _cardObj)
    {
        bool turnOver = false;
        int matchScore = 0;
        List<string> matchedString = new List<string>();

        foreach (CardProperty selectedCardProperty in _selectedCardData.cardProperties)
        {
            foreach (CardProperty currentCardProperty in currentCardData.cardProperties)
            {
                if (selectedCardProperty == currentCardProperty)
                {
                    int comboScore = CheckCombo(selectedCardProperty);
                    matchedString.Add(selectedCardProperty.ToString() + "! +" + (1 + comboScore) + "\n");
                    matchScore += 1 + comboScore;
                }
            }
        }

        // punish if no matches found
        if(matchScore == 0)
        {
            matchScore = -5;
            matchedString.Add("No matches found! -5");
            turnOver = true;

            soundManager.PlaySound(SoundType.LosePoint);
        }
        else
        {
            //soundManager.PlaySound(SoundType.ScorePoint);
        }

        score += matchScore;

        score = Mathf.Clamp(score, 0, 50);

        uiManager.SetScoreText(score, matchScore, matchedString);
        uiManager.AnimateScore();

        if (score >= scoreToWin[difficulty])
        {
            uiManager.DisplayWinScreen(score, aiScript.GetScore());
        }

        if(turnOver)
        {
            TurnOver();
        }
    }

    int CheckCombo(CardProperty _selectedCardProperty)
    {
        /*foreach(CardProperty previosCardProperty in previousCardProperties)
        {
            if (_selectedCardProperty == previosCardProperty)
            {
                return 1;
            }
        }

        previousCardProperties.Add(_selectedCardProperty);*/
        return 0;
    }

    public void AISetMainCard(CardData _cardData)
    {
        CardScript cardScript = mainCardObj.GetComponent<CardScript>();
        cardScript.SetCardData(_cardData);
        cardScript.SetComponents();
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
        return scoreToWin[difficulty];
    }
    public int GetScore()
    {
        return score;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }
}
