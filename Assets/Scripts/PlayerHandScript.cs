using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CardPropertiesScript;
using static SoundManager;

public class PlayerHandScript : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private CardPropertiesScript cardPropertiesScript;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;
    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject cardPrefab;
    [Space]
    [Header("Transforms")]
    [SerializeField] private Transform hand;
    [SerializeField] private Transform deckPos;
    [Space]
    [Header("Variables")]
    [SerializeField] private float drawWaitTime;
    [Space]
    [Header("Debugging")]
    [SerializeField] private bool isDebugging;

    // private variables
    private List<GameObject> cards = new List<GameObject>();
    private const int totalCards = 5;
    private const float cardSize = 1.6f;

    private void Start()
    {
        // clear out old hand of cards
        foreach(Transform child in hand)
        {
            Destroy(child.gameObject);
        }
    }

    public IEnumerator CheckDrawCards()
    {
        int total = totalCards - cards.Count;

        if(total > 0)
        {
            soundManager.PlaySound(SoundType.DrawCards);
        }

        for (int i = 0; i < total; i++)
        {
            yield return new WaitForSeconds(drawWaitTime);
            DrawCard();
        }
    }

    private void Update()
    {
        if (isDebugging)
        {
            // Debugging keys
            if (Input.GetKeyDown(KeyCode.E))
            {
                DrawCard();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void DrawCard()
    {
        GameObject cardObj = Instantiate(cardPrefab, deckPos.position, Quaternion.identity, hand);
        cardObj.transform.localEulerAngles = Vector3.zero;

        CardScript cardScript = cardObj.GetComponent<CardScript>();
        cardScript.Init(this, gameManager, soundManager, cardPropertiesScript.GetRandomCard());
        cardScript.SetIsInHand(true);

        cards.Add(cardObj);

        RepositionCards();
    }

    public void DiscardAllCards()
    {
        for(int i = cards.Count - 1; i >= 0; i--)
        {
            Destroy(cards[i]);
        }

        cards.Clear();

        gameManager.TurnOver();
    }

    private void RepositionCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float totalSpacing = (cards.Count - 1) * cardSize;
            float startX = -totalSpacing / 2;

            float xPos = startX + i * cardSize;
            cards[i].GetComponent<CardScript>().StartLerpToPos(new Vector3(xPos, 0, 0));
            //cards[i].transform.localPosition = new Vector3(xPos, 0, 0);
        }
    }

    public void RemoveCard(GameObject _cardObj)
    {
        cards.Remove(_cardObj);
        //Destroy(_cardObj);
        RepositionCards();
    }

    public void CardSelected(CardData _cardData, GameObject _cardObj)
    {
        gameManager.CompareCards(_cardData, _cardObj);

        _cardObj.transform.SetParent(gameManager.GetMainCardPos());
        _cardObj.GetComponent<CardScript>().StartLerpToPos(Vector3.zero);

        RemoveCard(_cardObj);
        Destroy(gameManager.GetMainCard(), 0.5f);
        gameManager.SetMainCard(_cardObj);
    }
}
