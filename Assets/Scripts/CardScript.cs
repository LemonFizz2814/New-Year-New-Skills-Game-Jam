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
    //[SerializeField] private SpriteRenderer symbolSprite;
    [Space]
    [Header("Variables")]
    [SerializeField] private float lerpSpeed;
    [SerializeField] private Sprite[] symbolSprites;

    // private variables
    private bool isInHand = false;
    private Vector3 lerpEndPos;
    private Vector3 lerpStartPos;
    private float lerpT = 1.1f;

    private Animator animator;
    private PlayerHandScript playerHandScript;
    private CardData cardData;

    void Start()
    {
        animator = GetComponent<Animator>();
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

    public void Init(PlayerHandScript _playerHandScript, CardData _cardData, bool _setRandomSymbol)
    {
        playerHandScript = _playerHandScript;
        cardData = _cardData;
        isInHand = true;

        if(_setRandomSymbol)
        {
            cardData.cardSymbol = (CardSymbol)UnityEngine.Random.Range(0, Enum.GetValues(typeof(CardSymbol)).Length);
        }

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
        if (isInHand)
        {
            isInHand = false;
            propertiesText.text = "";
            playerHandScript.CardSelected(cardData, gameObject);
        }
    }
    void OnMouseOver()
    {
        if (isInHand)
        {
            animator.SetBool("HoverOver", true);
        }
    }
    private void OnMouseExit()
    {
        if (isInHand)
        {
            animator.SetBool("HoverOver", false);
        }
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