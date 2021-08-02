using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(ChangeLevel))]
public class ChooseCheckAnswer : MonoBehaviour
{
    public UnityEvent<string> AnswerChosen;
    public UnityEvent<GameObject> GoodAnswer;
    public UnityEvent<GameObject> WrongAnswer;

    private bool canButtonPress = true;
    private string _answer;
    private List<string> _answers = new List<string>();
    private readonly System.Random _random = new System.Random();

    public void Choose(List<string> data)
    {
        do
        {
            _answer = data[_random.Next(data.Count)];
        } while (_answers.Contains(_answer));
        _answers.Add(_answer);
        AnswerChosen.Invoke(_answer);
    }
    public void AnswerCheck(GameObject card)
    {
        if (canButtonPress)
        {
            if (card.name == _answer)
            {
                GoodAnswer.Invoke(card);
            }
            else
            {
                WrongAnswer.Invoke(card);
            }
        }
    }
    public void GameFinished()
    {
        StopCardCheck();
        AnswersClear();
    }
    public void StartCardCheck()
    {
        canButtonPress = true;
    }
    private void StopCardCheck()
    {
        canButtonPress = false;
    }
    private void AnswersClear()
    {
        _answers.Clear();
    }
}
