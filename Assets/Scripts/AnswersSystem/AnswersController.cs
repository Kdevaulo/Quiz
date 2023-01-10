using System;
using System.Collections.Generic;
using System.Linq;

using Quiz.CardSystem;
using Quiz.Utils;

using UnityEngine;
using UnityEngine.Events;

namespace Quiz.AnswersSystem
{
    public class AnswersController : IDisposable
    {
        public readonly UnityEvent<string> CorrectItemChosen = new UnityEvent<string>();

        public readonly UnityEvent<CardView> OnCorrectAction = new UnityEvent<CardView>();

        public readonly UnityEvent<Transform> OnWrongAction = new UnityEvent<Transform>();

        public readonly UnityEvent OnCorrectItemChoose = new UnityEvent();

        public readonly UnityEvent CorrectItemActivated = new UnityEvent();

        public readonly UnityEvent WrongItemActivated = new UnityEvent();

        private readonly AnswersModel _answersModel;

        private readonly UnityEvent _cardsCreated;

        private string _chosenID;

        private List<string> _chosenItems = new List<string>();

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
            if (_chosenID == view.CardID)
            {
                CorrectItemActivated.Invoke();
                OnCorrectAction.Invoke(view);
            }
            else
            {
                WrongItemActivated.Invoke();
                OnWrongAction.Invoke(view.SpriteContainer);
            }
        }

        private void ChooseCorrectItem()
        {
            var items = _answersModel.GetUsedItems();

            items = items.Shuffle();

            var unusedItem = items.FirstOrDefault(x => !_chosenItems.Contains(x.CardID));
            // note: the unique correct answer in the playmode is not fully implemented

            if (unusedItem == null)
            {
                _chosenItems.Clear();
                unusedItem = items.First();
            }

            var id = unusedItem.CardID;

            _chosenID = id;

            _chosenItems.Add(id);

            CorrectItemChosen.Invoke(_chosenID);
            OnCorrectItemChoose.Invoke();
        }
    }
}