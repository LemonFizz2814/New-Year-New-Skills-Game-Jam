using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    //[Space]
    //[Header("Variables")]
    //[SerializeField] private float drawWaitTime;

    // private variables
    private GameObject mainCardObj;
    private CardData currentCardData;
    private List<CardProperty> previousCardProperties = new List<CardProperty>();
    private bool playersTurn;
    private int score = 0;
    private const int scoreToWin = 25;

    private void Start()
    {
        mainCardObj = mainCardPos.GetChild(0).gameObject;
        mainCardObj.GetComponent<CardScript>().SetIsInHand(false);

        TurnStart(cardPropertiesScript.GetRandomCard());

        aiScript.Init(this);
    }

    public void SetMainCard(GameObject _cardObj)
    {
        mainCardObj = _cardObj;
        currentCardData = mainCardObj.GetComponent<CardScript>().GetCardData();
        mainCardObj.GetComponent<CardScript>().Init(playerHandScript, this, currentCardData);
        mainCardObj.GetComponent<Animator>().SetBool("SetMainCard", true);
    }

    public void TurnOver()
    {
        playersTurn = false;
        previousCardProperties.Clear();
        StartCoroutine(aiScript.AIsTurn(currentCardData));
        uiManager.DisplayAITurnScreen();
    }
    public void TurnStart(CardData _cardData)
    {
        playersTurn = true;

        mainCardObj.GetComponent<CardScript>().SetCardData(_cardData);
        SetMainCard(mainCardObj);

        StartCoroutine(playerHandScript.CheckDrawCards());
        uiManager.DisplayYourTurnScreen();
    }

    public void CompareCards(CardData _selectedCardData, GameObject _cardObj)
    {
        int matchScore = 0;
        bool turnOver = false;
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
        }

        score += matchScore;

        uiManager.SetScoreText(score, matchScore, matchedString);
        uiManager.AnimateScore();

        if (score >= scoreToWin)
        {
            uiManager.DisplayWinScreen();
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

    public void QuitPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void RestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
