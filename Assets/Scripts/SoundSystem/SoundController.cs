using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine.Events;

namespace Quiz.SoundSystem
{
    public class SoundController : IDisposable
    {
        private readonly SoundPlayer _soundPlayer;

        private readonly UnityEvent _correctItemClicked;

        private readonly UnityEvent _wrongItemClicked;

        private bool _backgroundIsPlaying;

        private bool _correctIsPlaying;

        private bool _wrongIsPlaying;

        private CancellationTokenSource _cts = new CancellationTokenSource();

        public SoundController(SoundPlayer soundPlayer, UnityEvent correctItemClicked,
            UnityEvent wrongItemClicked)
        {
            _soundPlayer = soundPlayer;
            _correctItemClicked = correctItemClicked;
            _wrongItemClicked = wrongItemClicked;

            _correctItemClicked.AddListener(TryPlayCorrectSound);
            _wrongItemClicked.AddListener(TryPlayWrongSound);

            TryPlayBackgroundSound();
        }

        void IDisposable.Dispose()
        {
            if (_cts != null && !_cts.IsCancellationRequested)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }

            _correctItemClicked.RemoveListener(TryPlayCorrectSound);
            _wrongItemClicked.RemoveListener(TryPlayWrongSound);
        }

        private void TryPlayWrongSound()
        {
            if (_wrongIsPlaying)
            {
                return;
            }

            PlayWrongSoundAsync().Forget();
        }

        private void TryPlayCorrectSound()
        {
            if (_correctIsPlaying)
            {
                return;
            }

            PlayCorrectSoundAsync().Forget();
        }

        private void TryPlayBackgroundSound()
        {
            if (_backgroundIsPlaying)
            {
                return;
            }

            PlayBackgroundSoundAsync().Forget();
        }

        private async UniTask PlayWrongSoundAsync()
        {
            _wrongIsPlaying = true;

            await _soundPlayer.PlayWrongSoundAsync(_cts.Token);

            _wrongIsPlaying = false;
        }

        private async UniTask PlayCorrectSoundAsync()
        {
            _correctIsPlaying = true;

            await _soundPlayer.PlayCorrectSoundAsync(_cts.Token);

            _correctIsPlaying = false;
        }

        private async UniTask PlayBackgroundSoundAsync()
        {
            _backgroundIsPlaying = true;

            while (!_cts.IsCancellationRequested)
            {
                await _soundPlayer.PlayBackgroundSoundAsync(_cts.Token);
            }

            _backgroundIsPlaying = false;
        }
    }
}