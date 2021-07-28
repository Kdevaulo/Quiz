using UnityEngine;
using UnityEngine.UI;

public class CreateText : MonoBehaviour
{
    [SerializeField] private string _prefixText;
    [SerializeField] private int _fontSize;
    [SerializeField] private Font _font;
    [SerializeField] private Color _color;
    [SerializeField] private Vector2 _position;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private Transform _parent;

    private Text _text;
    private bool _isFirstSpawn = true;

    public void SetFirstSpawn()
    {
        _isFirstSpawn = true;
    }
    public void SpawnText(string text)
    {
        if (_text != null)
        {
            Destroy(_text.gameObject);
        }

        GameObject textContainer = new GameObject("Text");
        _text = textContainer.AddComponent<Text>();
        ContainerSetting(textContainer.transform);
        TextSetting(text);

        if (_isFirstSpawn)
        {
            textContainer.AddComponent<FadeInOut>().FadeIn(_fadeInDuration);
            _isFirstSpawn = false;
        }
    }
    private void DestroyText(Text text)
    {
        Destroy(text.gameObject);
    }
    private void ContainerSetting(Transform containerTransform)
    {
        containerTransform.position = _position;
        containerTransform.SetParent(_parent);
    }
    private void TextSetting(string text)
    {
        _text.text = _prefixText + text;
        _text.fontSize = _fontSize;
        _text.alignment = TextAnchor.MiddleCenter;
        _text.font = _font;
        _text.color = _color;
        _text.horizontalOverflow = HorizontalWrapMode.Overflow;
    }
}
