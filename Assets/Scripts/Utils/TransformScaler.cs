using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;

namespace Quiz.Utils
{
    public class TransformScaler : IDisposable
    {
        private float _duration = 1f;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        void IDisposable.Dispose()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        public async UniTask DisappearBounceAsync(Transform transform)
        {
            var multiplier = transform.localScale.x;

            await transform.DOScale(1.25f * multiplier, _duration / 4).WithCancellation(_cts.Token);
            await transform.DOScale(0f, _duration / 8).WithCancellation(_cts.Token);
        }

        public async UniTask AppearBounceAsync(Transform transform)
        {
            var multiplier = transform.localScale.x;

            await transform.DOScale(1.25f * multiplier, _duration / 8).WithCancellation(_cts.Token);
            await transform.DOScale(0.95f * multiplier, _duration / 6).WithCancellation(_cts.Token);
            await transform.DOScale(1f * multiplier, _duration / 6).WithCancellation(_cts.Token);
        }
    }
}