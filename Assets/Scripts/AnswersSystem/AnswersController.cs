using System;

using Quiz.CardData;

using UnityEngine;
using UnityEngine.Events;

using Random = UnityEngine.Random;

namespace Quiz.AnswersSystem
{
    public class AnswersController : IDisposable
    {
        public readonly UnityEvent<string> CorrectItemChosen = new UnityEvent<string>();

        public readonly UnityEvent CorrectItemActivated = new UnityEvent();

        public readonly UnityEvent<CardView> OnCorrectAction = new UnityEvent<CardView>();

        public readonly UnityEvent WrongItemActivated = new UnityEvent();

        public readonly UnityEvent<Transform> OnWrongAction = new UnityEvent<Transform>();

        private readonly AnswersModel _answersModel;

        private readonly UnityEvent _cardsCreated;

        private string _chosenID;

        public AnswersController(AnswersModel answersModel, UnityEvent cardsCreated)
        {
            _answersModel = answersModel;
            _cardsCreated = cardsCreated;

            cardsCreated.AddListener(ChooseCorrectItem);
        }

        void IDisposable.Dispose()
        {
            _cardsCreated.RemoveListener(ChooseCorrectItem);
        }

        public void CheckAnswer(CardView view)
        {
            if (_chosenID == view.GetID())
            {
                CorrectItemActivated.Invoke();
                OnCorrectAction.Invoke(view);
            }
            else
            {
                WrongItemActivated.Invoke();
                OnWrongAction.Invoke(view.GetSpriteContainer());
            }
        }

        private void ChooseCorrectItem()
        {
            var items = _answersModel.GetUsedItems();

            var chosenOne = items[Random.Range(0, items.Count)];
            _chosenID = chosenOne.GetID();

            CorrectItemChosen.Invoke(_chosenID);
        }
    }
}