using System;

using Cysharp.Threading.Tasks;

using Quiz.LevelSystem;
using Quiz.Utils;

using UnityEngine.Events;
using UnityEngine.UI;

namespace Quiz.RestartSystem
{
    public class RestartController : IDisposable
    {
        private readonly Image _blackoutImage;

        private readonly Image _restartImage;

        private readonly Button _restartButton;

        private readonly LevelController _levelController;

        private readonly UnityEvent _gameFinished;

        private readonly UnityEvent _restartButtonClicked;

        private readonly ColorFader<Image> _colorFader = new ColorFader<Image>();

        public RestartController(RestartView view, LevelController levelController, UnityEvent gameFinished)
        {
            _levelController = levelController;
            _gameFinished = gameFinished;

            _restartButtonClicked = view.RestartButtonClicked;
            _blackoutImage = view.BlackoutImage;
            _restartImage = view.ButtonImage;
            _restartButton = view.RestartButton;

            _restartButton.onClick.AddListener(HandleRestartPressed);
            gameFinished.AddListener(ShowRestartScreen);
        }

        void IDisposable.Dispose()
        {
            _restartButton.onClick.RemoveListener(HandleRestartPressed);
            _gameFinished.RemoveListener(ShowRestartScreen);
        }

        private void ShowRestartScreen()
        {
            ShowRestartScreenAsync().Forget();
            ShowRestartButtonAsync().Forget();
        }

        private void HandleRestartPressed()
        {
            RestartGameAsync().Forget();
        }

        private async UniTask RestartGameAsync()
        {
            _blackoutImage.gameObject.SetActive(true);
            await _colorFader.FadeIn(0.3f, _blackoutImage);

            await _colorFader.FadeOut(0f, _restartButton.image);
            await _colorFader.FadeOut(0f, _restartImage);
            _restartButton.gameObject.SetActive(false);
            _restartImage.gameObject.SetActive(false);

            _levelController.StartNextLevel();

            await _colorFader.FadeOut(0.3f, _blackoutImage);
            _blackoutImage.gameObject.SetActive(false);
        }

        private async UniTask ShowRestartScreenAsync()
        {
            _restartImage.gameObject.SetActive(true);
            await _colorFader.FadeIn(0.3f, _restartImage);
        }

        private async UniTask ShowRestartButtonAsync()
        {
            _restartButton.gameObject.SetActive(true);
            await _colorFader.FadeIn(0.3f, _restartButton.image);
        }
    }
}