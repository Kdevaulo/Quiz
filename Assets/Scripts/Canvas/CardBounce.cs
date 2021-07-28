using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class CardBounce : MonoBehaviour
{
    [SerializeField] private float _stageDuration;

    private Vector2[] vectors = {
        new Vector2(0, 0),
        new Vector2(1.2f, 1.2f),
        new Vector2(0.95f, 0.95f),
        new Vector2(1, 1) };

    public void Appear(GameObject card, float scale)
    {
        Transform cardTransform = card.transform;
        cardTransform.DOScale(vectors[0], 0);
        cardTransform.DOScale(vectors[1] * scale, _stageDuration).SetDelay(_stageDuration);
        cardTransform.DOScale(vectors[2] * scale, _stageDuration).SetDelay(_stageDuration * 2);
        cardTransform.DOScale(vectors[3] * scale, _stageDuration).SetDelay(_stageDuration * 3);
    }
    public void Disappear(GameObject card, float scale)
    {

    }
}
