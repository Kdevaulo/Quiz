using System;

using UnityEngine;

namespace Quiz.LevelSystem
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] private string _name;

        [SerializeField] private int _cardsCount;

        public int GetCount()
        {
            return _cardsCount;
        }
    }
}