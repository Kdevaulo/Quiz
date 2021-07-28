using UnityEngine;

public class CardDataChoose : MonoBehaviour
{
    [SerializeField] private CardBundleData[] _cardsData;

    private readonly System.Random _random = new System.Random();
    public CardData[] ChooseData()
    {
        return _cardsData[_random.Next(_cardsData.Length)].GetCardData();
    }
}
