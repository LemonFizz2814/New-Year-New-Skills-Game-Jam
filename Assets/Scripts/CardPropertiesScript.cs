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
        public CardProperties[] cardProperty;
    }

    public enum CardProperties
    {
        Humanoid,
        Fantasy,
        Red,
        French,
        Building,
    }

    public CardData GetRandomCard()
    {
        return cardList[UnityEngine.Random.Range(0, cardList.Count)];
    }
}