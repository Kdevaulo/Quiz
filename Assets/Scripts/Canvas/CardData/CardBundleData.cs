using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Bundle Data", order = 1)]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private CardData[] _cardData;

    public CardData[] GetCardData()
    {
        CardData[] cardData = new CardData[_cardData.Length];
        Array.Copy(_cardData, cardData, _cardData.Length);
        return cardData;
    }
}
