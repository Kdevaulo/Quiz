using UnityEngine;
using DG.Tweening;

public class LeftRightMovement : MonoBehaviour
{
    public void Move(Vector3[] path, float duration)
    {
        transform.DOLocalPath(path, duration, PathType.CatmullRom);
    }
}
