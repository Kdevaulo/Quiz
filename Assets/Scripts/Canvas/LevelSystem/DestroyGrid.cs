using UnityEngine;

public class DestroyGrid : MonoBehaviour
{
    [SerializeField] private float _delay;

    private GameObject _child;

    public void GameFinished()
    {
        CancelInvoke(nameof(DestroyParent));
    }
    public void GridDestroy(GameObject child)
    {
        _child = child;
        Invoke(nameof(DestroyParent), _delay);
    }
    private void DestroyParent()
    {
        Destroy(_child.transform.parent.gameObject);
    }
}
