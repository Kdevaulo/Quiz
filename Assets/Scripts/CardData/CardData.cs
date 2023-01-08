using System;

using UnityEngine;

namespace Quiz.CardData
{
    [Serializable]
    public class CardData
    {
        [SerializeField] private string _identifier;

        [SerializeField] private Vector3 _rotation;

        [SerializeField] private Sprite _sprite;

        public string GetID()
        {
            return _identifier;
        }

        public Sprite GetSprite()
        {
            return _sprite;
        }

        public Vector3 GetRotation()
        {
            return _rotation;
        }
    }
}