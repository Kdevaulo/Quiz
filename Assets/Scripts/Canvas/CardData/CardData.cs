using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private string _identifier;
    [SerializeField] private Sprite _sprite;

    public string GetID()
    {
        return _identifier;
    }
    public Sprite GetSprite()
    {
        return _sprite;
    } 
}
