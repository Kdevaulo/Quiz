using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;

namespace Quiz.Utils
{
    public class TransformMover : IDisposable
    {
        private const float ShakeStrength = 0.1f;

        private readonly Vector3[] _path =
        {
            Vector2.left * ShakeStrength, Vector2.right * ShakeStrength, Vector2.left * ShakeStrength, Vector2.zero
        };

        private float _duration = 0.5f;

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

        public async UniTask MoveByPathAsync(Transform transform)
        {
            await transform.DOLocalPath(_path, _duration, PathType.CatmullRom).WithCancellation(_cts.Token);
        }
    }
}