using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[RequireComponent(typeof(CardDataChoose), typeof(ChooseCheckAnswer), typeof(CardBounce))]
public class CardSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _card;
    [SerializeField] private Vector2 _localScale;

    private bool _isFirstTime = true;
    private CardBounce _cardBounce;
    private ChooseCheckAnswer _answerChecker;
    private readonly System.Random _random = new System.Random();

    public void SpawnCards(int cardsCount, Transform parent)
    {
        CardData[] cardData = GetComponent<CardDataChoose>().ChooseData();

        if (cardData.Length < cardsCount)
        {
            throw new System.Exception("cardData < cardsCount");
        }

        List<int> usedData = new List<int>();
        List<string> dataNames = new List<string>();

        for (int i = 0; i < cardsCount; i++)
        {
            int selected;
            do
            {
                selected = _random.Next(cardData.Length);
            }
            while (usedData.Contains(selected));
            usedData.Add(selected);

            CardData selectedData = cardData[selected];
            GameObject card = Instantiate(_card, parent);
            string name = selectedData.GetID();
            card.name = name;

            if (!card.TryGetComponent(out CardClick cardClick))
            {
                throw new System.Exception("Card Prefab does not have CardClick component");
            }

            cardClick.SetAnswerChecker(_answerChecker);
            dataNames.Add(name);

            GameObject data = new GameObject("data");
            data.transform.SetParent(card.transform);
            data.transform.localPosition = new Vector2(0, 0);
            data.transform.localScale = _localScale;
            Image image = data.AddComponent<Image>();
            image.sprite = selectedData.GetSprite();
            image.SetNativeSize();
            image.fillMethod = Image.FillMethod.Vertical;

            if (_isFirstTime)
            {
                _cardBounce.Appear(data, _localScale.x);
            }
        }
        _isFirstTime = false;

        _answerChecker.Choose(dataNames);
    }
    private void OnEnable()
    {
        _answerChecker = GetComponent<ChooseCheckAnswer>();
        _cardBounce = GetComponent<CardBounce>();
    }
}
