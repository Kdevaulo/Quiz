using UnityEngine;
using UnityEngine.Assertions;

using Random = System.Random;

namespace Quiz.CardSystem
{
    [CreateAssetMenu(fileName = nameof(CardDataModel), menuName = nameof(CardDataModel) + " in " + nameof(CardData))]
    public class CardDataModel : ScriptableObject
    {
        public Color[] CardColors => _cardColors;

        [SerializeField] private Color[] _cardColors;

        [SerializeField] private CardDataCollection[] _cardDataCollections;

        private readonly Random _random = new Random();

        private CardData[] _currentDataCollection;

        public void ChooseRandomDataCollection()
        {
            _currentDataCollection =
                _cardDataCollections[_random.Next(_cardDataCollections.Length)].GetCardDataCollection();
        }

        public CardData GetRandomData()
        {
            Assert.IsNotNull(_currentDataCollection,
                $"_currentDataCollection != null, " +
                $"call {nameof(ChooseRandomDataCollection)} before {nameof(GetRandomData)}");

            return _currentDataCollection[_random.Next(_currentDataCollection.Length)];
        }
    }
}