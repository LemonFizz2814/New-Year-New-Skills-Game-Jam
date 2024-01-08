using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CardPropertiesScript;

public class GameManager : MonoBehaviour
{
    // public variables
    [Header("Script Reference")]
    [SerializeField] private CardPropertiesScript cardPropertiesScript;

    // private variables
    private CardData currentCard;

    private void Start()
    {
        
    }
}
