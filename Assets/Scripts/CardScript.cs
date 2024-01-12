using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardPropertiesScript;
using TMPro;
using Unity.VisualScripting;
using System;

public class CardScript : MonoBehaviour
{
    // public variables
    [Header("Object Reference")]
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro propertiesText;
    [SerializeField] private SpriteRenderer cardSprite;
    [SerializeField] private GameObject propertiesSprite;
    //[SerializeField] private SpriteRenderer symbolSprite;
    [Space]
    [Header("Variables")]
    [SerializeField] private float lerpSpeed;

    // private variables
    private bool isInHand = false;
    private Vector3 lerpEndPos;
    private Vector3 lerpStartPos;
    private float lerpT = 1.1f;

    private Animator animator;
    private PlayerHandScript playerHandScript;
    private GameManager gameManager;
    private CardData cardData;

    void Awake()
    {
        animator = GetComponent<Animator>();
        propertiesSprite.SetActive(false);
    }

    private void Update()
    {
        if(lerpT <= 1)
        {
            transform.localPosition = Vector3.Lerp(lerpStartPos, lerpEndPos, lerpT);
            //transform.localEulerAngles = Vector3.Lerp(lerpStartPos.localEulerAngles, lerpEndPos.localEulerAngles, lerpT);

            lerpT += Time.deltaTime * lerpSpeed;
        }
    }

    public void Init(PlayerHandScript _playerHandScript, GameManager _gameManager, CardData _cardData)
    {
        playerHandScript = _playerHandScript;
        gameManager = _gameManager;
        cardData = _cardData;

        SetComponents();
    }

    public void SetComponents()
    {
        nameText.text = cardData.cardName;
        cardSprite.sprite = cardData.cardSprite;
        //symbolSprite.sprite = symbolSprites[(int)cardData.cardSymbol];

        propertiesText.text = "";
        for (int i = 0; i < cardData.cardProperties.Length; i++)
        {
            propertiesText.text += cardData.cardProperties[i].ToString() + "\n";
        }
    }

    public void StartLerpToPos(Vector3 _LerpEndPos)
    {
        lerpStartPos = transform.localPosition;
        lerpEndPos = _LerpEndPos;
        lerpT = 0;
    }

    void OnMouseDown()
    {
        if (isInHand && gameManager.GetPlayersTurn())
        {
            SetIsInHand(false);
            propertiesSprite.SetActive(!isInHand);
            playerHandScript.CardSelected(cardData, gameObject);
        }
    }
    void OnMouseOver()
    {
        if (isInHand && gameManager.GetPlayersTurn())
        {
            animator.SetBool("HoverOver", true);
        }
    }
    private void OnMouseExit()
    {
        if (isInHand && gameManager.GetPlayersTurn())
        {
            animator.SetBool("HoverOver", false);
        }
    }

    public void SetIsInHand(bool _isInHand)
    {
        isInHand = _isInHand;
        animator.SetBool("HoverOver", false);
        propertiesText.gameObject.SetActive(isInHand);
        propertiesSprite.SetActive(!isInHand);
    }
    public CardData GetCardData()
    {
        return cardData;
    }
    public void SetCardData(CardData _cardData)
    {
        cardData = _cardData;
    }
}