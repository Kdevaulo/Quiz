using System;
using UnityEngine;

public class ScaleCard : MonoBehaviour
{
    public void ScaleDown(GameObject card)
    {
        Scale cardScale;
        if (!card.TryGetComponent(out cardScale))
        {
            throw new Exception("Card Prefab does not have Scale component");
        }
        cardScale.ScaleDown();
    }
}
