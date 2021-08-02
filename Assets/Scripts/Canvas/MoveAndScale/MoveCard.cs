using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCard : MonoBehaviour
{
    [SerializeField] private string _dataName;
    [SerializeField] private float _duration;
    [SerializeField] private Vector3[] _path;

    public void Move(GameObject card)
    {
        GameObject child = card.transform.Find(_dataName).gameObject;
        child.AddComponent<LeftRightMovement>().Move(_path,_duration);
    }
}
