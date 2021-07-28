using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    private Image _image;
    private Text _text;

    public void FadeIn(float duration)
    {
        if (_image != null)
        {
            Color endColor = _image.color;
            _image.color = new Color(endColor.r, endColor.g, endColor.b, 0);
            Fade(endColor, duration);
        }
        else
        {
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
            Fade(1, duration);
        }
    }
    public void FadeOut(float duration)
    {
        if (_image != null)
        {
            Color endColor = new Color(_image.color.r, _image.color.g, _image.color.b, 0);
            Fade(endColor, duration);
        }
        else
        {
            Fade(0, duration);
        }
    }
    private void Fade(float value, float duration)
    {
        _text.DOFade(1, duration);
    }
    private void Fade(Color endColor, float duration)
    {
        _image.DOColor(endColor, duration);
    }

    private void OnEnable()
    {
        if (!TryGetComponent(out _image))
        {
            if (!TryGetComponent(out _text))
            {
                throw new Exception($"Can't fade because {gameObject} does not have Image or Text");
            }
        }

    }
}
