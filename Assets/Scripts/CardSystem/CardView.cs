using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Quiz.CardSystem
{
    [AddComponentMenu(nameof(CardView) + " in " + nameof(CardSystem))]
    public class CardView : MonoBehaviour, IPointerClickHandler
    {
        public readonly UnityEvent<CardView> CardClicked = new UnityEvent<CardView>();

        public SpriteRenderer CardRenderer => _cardRenderer;
        public SpriteRenderer CellRenderer => _cellRenderer;

        public Transform SpriteContainer => _spriteContainer;

        public Transform CellContainer => _cellContainer;

        public string CardID => _cardID;

        [SerializeField] private SpriteRenderer _cardRenderer;

        [SerializeField] private SpriteRenderer _cellRenderer;

        [SerializeField] private Transform _spriteContainer;

        [SerializeField] private Transform _cellContainer;

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
    }
}