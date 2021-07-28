using UnityEngine;
using UnityEngine.Events;
public class CardClick : MonoBehaviour
{
    private ChooseCheckAnswer _answerChecker;

    public void SetAnswerChecker(ChooseCheckAnswer answerChecker)
    {
        _answerChecker = answerChecker;
    }
    public void PointerDown()
    {
        _answerChecker.AnswerCheck(gameObject);
    }

}
