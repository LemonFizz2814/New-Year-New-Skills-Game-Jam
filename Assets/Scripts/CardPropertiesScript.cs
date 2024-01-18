using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPropertiesScript : MonoBehaviour
{
    [SerializeField] private List<CardData> cardList = new List<CardData>();

    [Serializable]
    public struct CardData
    {
        public string cardName;
        public Sprite cardSprite;
        public CardProperty[] cardProperties;
    }

    public enum CardProperty
    {
        None,
        Humanoid,
        Fantasy,
        Red,
        Fast,
        Building,
        Edible,
        Sharp,
        Transport,
        Plant,
        Soft,
        Little,
        Big,
        Metal,
        Animal,
        Orange,
        Green,
        Spooky,
        Dangerous,
        Cute,
        Planet,
        Yellow,
        Famous,
        Detailed,
        Water,
        Flight,
        White,
        Round,
        Long,
        Dead,
        Stripped,
        Stinky,
        Political,
    }

    public CardData GetRandomCard()
    {
        return cardList[UnityEngine.Random.Range(0, cardList.Count)];
    }
}