using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CardSpawn))]
public class CreateGrid : MonoBehaviour
{
    [SerializeField] private Color _gridColor;
    [SerializeField] private Sprite _gridSprite;
    [SerializeField] private Transform _parent;
    [SerializeField] private Vector2 _spacing;
    [SerializeField] private Vector2 _cellSize;
    [SerializeField] private int _objectWidthCount;

    private float _width;
    private float _height;

    public void SpawnGrid(int cardsCount)
    {
        GameObject grid = new GameObject("Grid");
        CalculateSize(cardsCount);
        GridSetting(grid);
        LayoutGroupSetting(grid.AddComponent<GridLayoutGroup>());
        GetComponent<CardSpawn>().SpawnCards(cardsCount, grid.transform);
    }
    private void CalculateSize(float cardsCount)
    {
        _width = _objectWidthCount * (_cellSize.x + _spacing.x * 2);
        _height = (float)Math.Ceiling(cardsCount / _objectWidthCount) * (_cellSize.y + _spacing.y * 2);
    }
    private void GridSetting(GameObject grid)
    {
        grid.transform.SetParent(_parent);
        grid.transform.localPosition = new Vector2(0, 0);
        grid.AddComponent<RectTransform>().sizeDelta = new Vector2(_width, _height);
        Image image = grid.AddComponent<Image>();
        image.color = _gridColor;
        image.sprite = _gridSprite;
    }
    private void LayoutGroupSetting(GridLayoutGroup layoutGroup)
    {
        layoutGroup.spacing = _spacing;
        layoutGroup.cellSize = _cellSize;
        layoutGroup.childAlignment = TextAnchor.MiddleCenter;
    }
}
