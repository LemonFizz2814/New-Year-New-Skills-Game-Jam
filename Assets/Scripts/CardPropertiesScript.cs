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
        [NonSerialized] public CardSymbol cardSymbol;
    }

    public enum CardProperty
    {
        None,
        Humanoid,
        Fantasy,
        Red,
        French,
        Building,
        Edible,
        Sharp,
        Sweet,
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
        Blue,
        Water,
        White,
        Round,
        Long,
        Fast,
    }

    public enum CardSymbol
    {
        Spade,
        Heart,
        Diamond,
        Club,
    }

    public CardData GetRandomCard()
    {
        return cardList[UnityEngine.Random.Range(0, cardList.Count)];
    }
}