using System;
using System.Collections.Generic;

using Cysharp.Threading.Tasks;

using Quiz.AnswersSystem;
using Quiz.Utils;

using UnityEngine;
using UnityEngine.Events;

namespace Quiz.CardData
{
    public class CardBehaviourController : IDisposable
    {
        public readonly UnityEvent CardBehaviourFinished = new UnityEvent();

        private readonly AnswersModel _answersModel;

        private readonly UnityEvent _correctItemActivated;

        private readonly UnityEvent _wrongItemActivated;

        private readonly UnityEvent<CardView> _onCorrectAction;

        private readonly UnityEvent<Transform> _onWrongAction;

        private readonly TransformMover _transformMover = new TransformMover();

        private readonly TransformScaler _transformScaler = new TransformScaler();

        private readonly List<Transform> _movingTransforms = new List<Transform>();

        public CardBehaviourController(AnswersModel answersModel, UnityEvent correctItemActivated,
            UnityEvent wrongItemActivated,
            UnityEvent<CardView> onCorrectAction,
            UnityEvent<Transform> onWrongAction)
        {
            _answersModel = answersModel;
            _correctItemActivated = correctItemActivated;
            _wrongItemActivated = wrongItemActivated;
            _onCorrectAction = onCorrectAction;
            _onWrongAction = onWrongAction;

            onCorrectAction.AddListener(HandleCorrectAction);
            onWrongAction.AddListener(TryMoveWrongItem);
        }

        void IDisposable.Dispose()
        {
            _onCorrectAction.RemoveListener(HandleCorrectAction);
            _onWrongAction.RemoveListener(TryMoveWrongItem);
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
            DisableCards(_answersModel.GetUsedItems());

            ScaleCorrectItemAsync(cardView.GetSpriteContainer()).Forget();

            cardView.PlayParticles();
        }

        private void DisableCards(List<CardView> cards)
        {
            foreach (var item in cards)
            {
                item.SetCanClick(false);
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

            CardBehaviourFinished.Invoke();
        }
    }
}