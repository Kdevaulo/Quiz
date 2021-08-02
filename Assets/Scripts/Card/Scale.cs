using UnityEngine;
using DG.Tweening;

public class Scale : MonoBehaviour
{
    [SerializeField] private float _duration;

    public void ScaleUp(float scale)
    {
        transform.DOScale(0, 0);
        transform.DOScale(scale, _duration);
    }
    public void ScaleDown()
    {
        transform.DOScale(0, _duration);
    }
}

