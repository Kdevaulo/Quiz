using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private Level[] _levels;
    public UnityEvent<int> LevelStarted;
    public UnityEvent GameFinished;

    private int _currentLevel = 0;

    public void StartNewLevel()
    {
        if (_currentLevel == 0)
        {
            StartLevel();
        }
        else
        {
            Invoke(nameof(StartLevel), _delay);
        }
    }
    private void StartLevel()
    {
        if (_currentLevel < _levels.Length)
        {
            LevelStarted.Invoke(_levels[_currentLevel++].GetCount());
        }
        else
        {
            _currentLevel = 0;
            GameFinished.Invoke();
        }
    }
    private void Start()
    {
        StartNewLevel();
    }

}
