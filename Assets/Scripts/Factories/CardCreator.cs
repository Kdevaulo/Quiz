using System;
using System.Collections.Generic;

using Quiz.AnswersSystem;
using Quiz.CardSystem;
using Quiz.UserEventHandleSystem;
using Quiz.Utils;

using UnityEngine;
using UnityEngine.Events;

using Object = UnityEngine.Object;

namespace Quiz.Factories
{
    public class CardCreator
    {
        public readonly UnityEvent CardsCreated = new UnityEvent();

        private readonly GameObject _cardPrefab;

        private readonly CardDataModel _dataModel;

        private readonly AnswersModel _answersModel;

        private readonly ColorSetter _colorSetter;

        private UserEventHandler _userEventHandler;

        public CardCreator(GameObject cardPrefab, CardDataModel dataModel, AnswersModel answersModel)
        {
            _cardPrefab = cardPrefab;
            _dataModel = dataModel;
            _answersModel = answersModel;
            _colorSetter = new ColorSetter(dataModel.CardColors);
        }

        public void SetEventHandler(UserEventHandler userEventHandler)
        {
            _userEventHandler = userEventHandler;
        }

        public void Initialize()
        {
            _dataModel.ChooseRandomDataCollection();
        }

        public void CreateCards(int cardsCount, Transform parent, Vector2[] positions)
        {
            var usedData = new List<CardData>();
            var createdCards = new List<CardView>();

            for (int i = 0; i < cardsCount; i++)
            {
                CardData selectedData;

                var stuckCounter = 0;

                do
                {
                    selectedData = _dataModel.GetRandomData();

                    if (++stuckCounter >= 100)
                    {
                        throw new Exception($"{nameof(CardCreator)} {nameof(CreateCards)} Stuck in loop");
                    }
                } while (usedData.Contains(selectedData));

                usedData.Add(selectedData);

                var id = selectedData.GetID();

                var card = Object.Instantiate(_cardPrefab);

                var cardTransform = card.transform;
                cardTransform.SetParent(parent);
                cardTransform.position = positions[i];

                var cardView = card.GetComponent<CardView>();
                cardView.SetID(id);

                var renderer = cardView.CardRenderer;
                renderer.sprite = selectedData.GetSprite();
                _colorSetter.SetUniqueColor(cardView.CellRenderer);

                renderer.gameObject.transform.rotation = Quaternion.Euler(selectedData.GetRotation());

                _userEventHandler.SubscribeEvents(cardView);

                createdCards.Add(cardView);
            }

            _answersModel.SetUsedItems(createdCards);
            CardsCreated.Invoke();
        }
    }
}