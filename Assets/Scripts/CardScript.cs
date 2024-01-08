using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardPropertiesScript;
using TMPro;
using Unity.VisualScripting;

public class CardScript : MonoBehaviour
{
    [SerializeField] private TextMeshPro nameText;
    [SerializeField] private TextMeshPro propertiesText;

    private bool isInHand = false;

    private Animator animator;
    private PlayerHandScript playerHandScript;
    private CardData cardData;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(PlayerHandScript _playerHandScript, CardData _cardData)
    {
        playerHandScript = _playerHandScript;
        cardData = _cardData;
        isInHand = true;

        SetText();
    }

    public void SetText()
    {
        nameText.text = cardData.cardName;

        propertiesText.text = "";
        for (int i = 0; i < cardData.cardProperties.Length; i++)
        {
            propertiesText.text += cardData.cardProperties[i].ToString() + "\n";
        }
    }

    void OnMouseDown()
    {
        if (isInHand)
        {
            isInHand = false;
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
}