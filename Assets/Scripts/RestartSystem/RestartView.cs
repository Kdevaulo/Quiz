using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Quiz.RestartSystem
{
    [AddComponentMenu(nameof(RestartView) + " in " + nameof(RestartSystem))]
    public class RestartView : MonoBehaviour
    {
        public readonly UnityEvent RestartButtonClicked = new UnityEvent();

        public Image ButtonImage => _buttonImage;

        public Image BlackoutImage => _blackoutImage;

        public Button RestartButton => _restartButton;

        [SerializeField] private Image _buttonImage;

        [SerializeField] private Image _blackoutImage;

        [SerializeField] private Button _restartButton;

        public void HandleButtonClick()
        {
            RestartButtonClicked.Invoke();
        }
    }
}