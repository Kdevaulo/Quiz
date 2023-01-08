using UnityEngine.Events;

namespace Quiz.LevelSystem
{
    public class LevelController
    {
        public readonly UnityEvent GameFinished = new UnityEvent();

        public readonly UnityEvent<int> LevelStarted = new UnityEvent<int>();

        private readonly LevelData[] _levelDataCollection;

        private readonly int _levelsCount;

        private int _levelCounter;

        private LevelData _currentData;

        public LevelController(LevelDataModel levelDataModel)
        {
            _levelDataCollection = levelDataModel.GetLevelDataCollection();
            _levelsCount = _levelDataCollection.Length;
            _levelCounter = 0;
        }

        public void StartNextLevel()
        {
            if (_levelCounter >= _levelsCount)
            {
                GameFinished.Invoke();
                _levelCounter = 0;

                return;
            }

            _currentData = _levelDataCollection[_levelCounter++];
            LevelStarted.Invoke(_currentData.GetCount());
        }
    }
}