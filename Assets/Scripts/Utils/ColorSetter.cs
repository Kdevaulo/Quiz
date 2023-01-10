using System.Collections.Generic;

using UnityEngine;

using Random = System.Random;

namespace Quiz.Utils
{
    public class ColorSetter
    {
        private readonly Color[] _targetColors;

        private readonly Random _random = new Random();

        private List<int> _randomIndexes = new List<int>();

        private int _index = 0;

        public ColorSetter(Color[] targetColors)
        {
            _targetColors = targetColors;

            GenerateIndexes();
        }

        public void SetUniqueColor(SpriteRenderer renderer)
        {
            renderer.color = _targetColors[GetUniqueIndex()];
        }

        private int GetUniqueIndex()
        {
            if (_index >= _targetColors.Length)
            {
                _randomIndexes = _randomIndexes.Shuffle();
                _index = 0;
            }

            return _randomIndexes[_index++];
        }

        private void GenerateIndexes()
        {
            for (int i = 0; i < _targetColors.Length; i++)
            {
                _randomIndexes.Add(i);
            }

            _randomIndexes = _randomIndexes.Shuffle();
        }
    }
}