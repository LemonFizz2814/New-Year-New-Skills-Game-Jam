using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static CardPropertiesScript;

public class PlayerHandScript : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private CardPropertiesScript cardPropertiesScript;
    [SerializeField] private GameManager gameManager;
    [Space]
    [Header("Prefabs")]
    [SerializeField] private GameObject cardPrefab;
    [Space]
    [Header("Transforms")]
    [SerializeField] private Transform hand;
    [SerializeField] private Transform deckPos;
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

        for (int i = 0; i < totalCards; i++)
        {
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
        cardObj.GetComponent<CardScript>().Init(this, cardPropertiesScript.GetRandomCard());
        cards.Add(cardObj);

        RepositionCards();
    }

    private void RepositionCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            float totalSpacing = (cards.Count - 1) * cardSize;
            float startX = -totalSpacing / 2;

            float xPos = startX + i * cardSize;
            cards[i].transform.localPosition = new Vector3(xPos, 0, 0);
        }
    }

    public void RemoveCard(GameObject _cardObj)
    {
        cards.Remove(_cardObj);
        Destroy(_cardObj);
        RepositionCards();
    }

    public void CardSelected(CardData _cardData, GameObject _cardObj)
    {
        StartCoroutine(gameManager.CompareCards(_cardData));
        RemoveCard(_cardObj);
    }
}
