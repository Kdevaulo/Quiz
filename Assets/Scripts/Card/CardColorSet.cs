using System;
using UnityEngine;
using UnityEngine.UI;

public class CardColorSet : MonoBehaviour
{
    [SerializeField] private Color[] colors;

    private void OnEnable()
    {
        GetComponent<Image>().color = ChooseColor();
    }
    private Color ChooseColor()
    {
        System.Random _random = new System.Random(GetInstanceID());
        return colors[_random.Next(colors.Length)];
    }
}
