using System;

using Quiz.AnswersSystem;
using Quiz.CardData;
using Quiz.Factories;
using Quiz.GridSystem;
using Quiz.LevelSystem;
using Quiz.RestartSystem;
using Quiz.SoundSystem;
using Quiz.TitleSystem;
using Quiz.UserEventHandleSystem;
using Quiz.Utils;

using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class Startup : MonoBehaviour, IDisposable
    {
        [SerializeField] private Transform _gridParent;

        [SerializeField] private Image _blackoutImage;

        [SerializeField] private GameObject _cardPrefab;

        [SerializeField] private SoundPlayer _soundPlayer;

        [SerializeField] private TitleView _titleView;

        [SerializeField] private RestartView restartView;

        [SerializeField] private CardDataModel _cardModel;

        [SerializeField] private LevelDataModel _levelDataModel;

        [SerializeField] private GridModel _gridModel;

        private SoundController _soundController;

        private LevelController _levelController;

        private GridController _gridController;

        private TitleController _titleController;

        private AnswersController _answersController;

        private CardBehaviourController _cardBehaviourController;

        private RestartController _restartController;

        private CardCreator _cardCreator;

        private AnswersModel _answersModel;

        private UserEventHandler _userEventHandler;

        private ColorFader<Image> _blackoutColorFader;

        private void Awake()
        {
            _levelController = new LevelController(_levelDataModel);
            _answersModel = new AnswersModel();
            _cardCreator = new CardCreator(_cardPrefab, _cardModel, _answersModel);
            _answersController = new AnswersController(_answersModel, _cardCreator.CardsCreated);
            _userEventHandler = new UserEventHandler(_answersController);
            _titleController = new TitleController(_titleView, _answersController.CorrectItemChosen);
            _gridController = new GridController(_cardCreator, _gridParent, _gridModel, _levelController.LevelStarted);
            _soundController = new SoundController(_soundPlayer, _answersController.CorrectItemActivated,
                _answersController.WrongItemActivated);
            _restartController = new RestartController(restartView, _levelController, _levelController.GameFinished);

            _cardBehaviourController = new CardBehaviourController(_answersModel,
                _answersController.CorrectItemActivated,
                _answersController.WrongItemActivated, _answersController.OnCorrectAction,
                _answersController.OnWrongAction);

            _cardCreator.SetEventHandler(_userEventHandler);
            _cardBehaviourController.CardBehaviourFinished.AddListener(_levelController.StartNextLevel);
        }

        private void Start()
        {
            _cardCreator.Initialize();
            _levelController.StartNextLevel();
        }

        void IDisposable.Dispose()
        {
            _cardBehaviourController.CardBehaviourFinished.RemoveListener(_levelController.StartNextLevel);
        }
    }
}