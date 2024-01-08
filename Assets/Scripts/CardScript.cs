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

    private bool isInHand = true;

    private Animator animator;
    private PlayerHandScript playerHandScript;
    private CardData cardStruct;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(PlayerHandScript _playerHandScript, CardData _cardStruct)
    {
        playerHandScript = _playerHandScript;
        cardStruct = _cardStruct;

        SetText();
    }

    public void SetText()
    {
        nameText.text = cardStruct.cardName;

        propertiesText.text = "";
        for (int i = 0; i < cardStruct.cardProperty.Length; i++)
        {
            propertiesText.text += cardStruct.cardProperty[i].ToString() + "\n";
        }
    }

    void OnMouseDown()
    {
        if (isInHand)
        {
            isInHand = false;
            playerHandScript.CardSelected();
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