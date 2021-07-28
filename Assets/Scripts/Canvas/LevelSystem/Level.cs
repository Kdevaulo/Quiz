using System;
using UnityEngine;

[Serializable]
public class Level
{
    [SerializeField] private string _name;
    [SerializeField] private int _cardsCount;
    public int GetCount()
    {
        return _cardsCount;
    }
}
