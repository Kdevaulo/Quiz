using System;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private Vector2 _size;
    [SerializeField] private Color _blackoutColor;
    [SerializeField] private Color _fullBlackoutColor;
    [SerializeField] private float _fadeInDuration;

    private Image _image;

    public void FullBlackout()
    {
        DoBlackout(_fullBlackoutColor);
    }
    public void PartialBlackout()
    {
        DoBlackout(_blackoutColor);
    }
    private void DoBlackout(Color color)
    {
        GameObject blackout = new GameObject("Blackout");
        blackout.transform.SetParent(_parent);
        RectTransform rectTransform = blackout.AddComponent<RectTransform>();
        rectTransform.sizeDelta = _size;
        _image = blackout.AddComponent<Image>();
        _image.color = color;
        blackout.AddComponent<FadeInOut>().FadeIn(_fadeInDuration);
    }
}
