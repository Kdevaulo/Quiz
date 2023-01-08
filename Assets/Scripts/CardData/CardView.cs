using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Quiz.CardData
{
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public readonly UnityEvent<CardView> CardClicked = new UnityEvent<CardView>();

        [SerializeField] private Image _childImage;

        [SerializeField] private Transform _spriteContainer;

        [SerializeField] private ParticleSystem _particleSystem;

        private string _cardID;

        private bool _canClick = true;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if (_canClick)
            {
                CardClicked.Invoke(this);
            }
        }

        public void PlayParticles()
        {
            _particleSystem.Play();
        }

        public void SetCanClick(bool value)
        {
            _canClick = value;
        }

        public void SetID(string targetName)
        {
            _cardID = targetName;
        }

        public string GetID()
        {
            return _cardID;
        }

        public Transform GetSpriteContainer()
        {
            return _spriteContainer;
        }

        public Image GetCardImage()
        {
            return _childImage;
        }
    }
}