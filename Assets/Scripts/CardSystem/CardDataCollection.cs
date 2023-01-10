using System;

using UnityEngine;

namespace Quiz.CardSystem
{
    [Serializable]
    public class CardDataCollection
    {
        [SerializeField] private string _collectionName;

        [SerializeField] private CardData[] _cardDataCollection;

        public CardData[] GetCardDataCollection()
        {
            return _cardDataCollection;
        }
    }
}