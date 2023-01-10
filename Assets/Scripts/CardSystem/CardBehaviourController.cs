using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using Quiz.AnswersSystem;
using Quiz.Utils;

using UnityEngine;
using UnityEngine.Events;

namespace Quiz.CardSystem
{
    public class CardBehaviourController : IDisposable
    {
        public readonly UnityEvent CardEndBehaviourFinished = new UnityEvent();

        private readonly AnswersModel _answersModel;

        private readonly UnityEvent _onCorrectItemChoose;

        private readonly UnityEvent<CardView> _onCorrectAction;

        private readonly UnityEvent<Transform> _onWrongAction;

        private readonly TransformMover _transformMover = new TransformMover();

        private readonly TransformScaler _transformScaler = new TransformScaler();

        private readonly List<Transform> _movingTransforms = new List<Transform>();

        private bool _isFirstStart = true;

        public CardBehaviourController(AnswersModel answersModel, UnityEvent onCorrectItemChoose,
            UnityEvent<CardView> onCorrectAction,
            UnityEvent<Transform> onWrongAction)
        {
            _answersModel = answersModel;
            _onCorrectItemChoose = onCorrectItemChoose;
            _onCorrectAction = onCorrectAction;
            _onWrongAction = onWrongAction;

            onCorrectItemChoose.AddListener(HandleFirstStart);
            onCorrectAction.AddListener(HandleCorrectAction);
            onWrongAction.AddListener(TryMoveWrongItem);
        }

        void IDisposable.Dispose()
        {
            _onCorrectItemChoose.RemoveListener(HandleFirstStart);
            _onCorrectAction.RemoveListener(HandleCorrectAction);
            _onWrongAction.RemoveListener(TryMoveWrongItem);
        }

        private void HandleFirstStart()
        {
            // note: it would be better to add visualization customization options in the level,
            // but for quick development I decided to hardcode it
            if (!_isFirstStart)
            {
                return;
            }

            DoCardAppearanceAsync().Forget();
            _isFirstStart = false;
        }

        private void TryMoveWrongItem(Transform targetTransform)
        {
            if (_movingTransforms.Contains(targetTransform))
            {
                return;
            }

            MoveWrongItemAsync(targetTransform).Forget();
        }

        private void HandleCorrectAction(CardView cardView)
        {
            SetCardsClickValue(_answersModel.GetUsedItems(), false);

            ScaleCorrectItemAsync(cardView.SpriteContainer).Forget();

            cardView.PlayParticles();
        }

        private async UniTask DoCardAppearanceAsync()
        {
            var usedItems = _answersModel.GetUsedItems();

            SetCardsClickValue(usedItems, false);
            await AppearCardsAsync();
            SetCardsClickValue(usedItems, true);
        }

        private async UniTask AppearCardsAsync()
        {
            var cardViews = _answersModel.GetUsedItems();

            foreach (var item in cardViews)
            {
                _transformScaler.SetZeroScale(item.CellContainer);
            }

            foreach (var item in cardViews)
            {
                await _transformScaler.AppearBounceAsync(item.CellContainer);
            }
        }

        private async UniTask MoveWrongItemAsync(Transform targetTransform)
        {
            _movingTransforms.Add(targetTransform);
            await _transformMover.MoveByPathAsync(targetTransform);
            _movingTransforms.Remove(targetTransform);
        }

        private async UniTask ScaleCorrectItemAsync(Transform targetTransform)
        {
            await _transformScaler.DisappearBounceAsync(targetTransform);

            CardEndBehaviourFinished.Invoke();
        }

        private void SetCardsClickValue(List<CardView> cards, bool value)
        {
            foreach (var item in cards)
            {
                item.SetCanClick(value);
            }
        }
    }
}