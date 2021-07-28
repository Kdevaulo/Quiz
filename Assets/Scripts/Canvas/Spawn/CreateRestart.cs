using UnityEngine;
using UnityEngine.UI;

public class CreateRestart : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    [SerializeField] private Vector2 _size;
    [SerializeField] private Transform _parent;
    [SerializeField] Button.ButtonClickedEvent _event;

    public void Create()
    {
        GameObject restart = new GameObject("Restart");
        RectTransform rectTransform = restart.AddComponent<RectTransform>();
        rectTransform.SetParent(_parent);
        rectTransform.sizeDelta = _size;
        restart.AddComponent<Image>().sprite = _sprite;
        restart.AddComponent<Button>().onClick = _event;
    }
}
