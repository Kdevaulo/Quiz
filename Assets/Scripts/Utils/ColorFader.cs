using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace Quiz.Utils
{
    public class ColorFader<T> : IDisposable where T : Graphic
    {
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

        public async UniTask FadeIn(float duration, T graphic)
        {
            var color = graphic.color;

            await Fade(new Color(color.r, color.g, color.b, 1), duration, graphic);
        }

        public async UniTask FadeOut(float duration, T graphic)
        {
            var color = graphic.color;

            await Fade(new Color(color.r, color.g, color.b, 0), duration, graphic);
        }

        private async UniTask Fade(Color color, float duration, T graphic)
        {
            await graphic.DOColor(color, duration).WithCancellation(_cts.Token);
        }
    }
}