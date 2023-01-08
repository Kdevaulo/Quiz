using System;

using Quiz.Factories;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Object = UnityEngine.Object;

namespace Quiz.GridSystem
{
    public class GridController : IDisposable
    {
        private readonly Color _gridColor;

        private readonly Sprite _gridSprite;

        private readonly Vector2 _spacing;

        private readonly Vector2 _cellSize;

        private readonly int _objectWidthCount;

        private readonly CardCreator _cardCreator;

        private readonly Transform _gridParent;

        private readonly UnityEvent<int> _levelStarted;

        private float _width;

        private float _height;

        private GameObject _grid;

        public GridController(CardCreator cardCreator, Transform gridParent, GridModel gridModel,
            UnityEvent<int> levelStarted)
        {
            _cardCreator = cardCreator;
            _gridParent = gridParent;

            _gridColor = gridModel.GridColor;
            _gridSprite = gridModel.GridSprite;
            _spacing = gridModel.Spacing;
            _cellSize = gridModel.CellSize;
            _objectWidthCount = gridModel.ObjectWidthCount;

            _levelStarted = levelStarted;
            levelStarted.AddListener(CreateGrid);
        }

        void IDisposable.Dispose()
        {
            _levelStarted.RemoveListener(CreateGrid);
        }

        private void CreateGrid(int cardsCount)
        {
            if (_grid)
            {
                _grid.SetActive(false);
                Object.Destroy(_grid);
            }

            _grid = new GameObject("Grid");

            CalculateSize(cardsCount);
            SetupGrid(_grid);
            SetupLayoutGroup(_grid.AddComponent<GridLayoutGroup>());

            _grid.transform.SetParent(_gridParent);

            _cardCreator.CreateCards(cardsCount, _grid.transform);
        }

        private void CalculateSize(float cardsCount)
        {
            _width = _objectWidthCount * (_cellSize.x + _spacing.x * 2);
            _height = (float) Math.Ceiling(cardsCount / _objectWidthCount) * (_cellSize.y + _spacing.y * 2);
        }

        private void SetupGrid(GameObject grid)
        {
            grid.transform.SetParent(_gridParent);
            grid.transform.localPosition = Vector2.zero;
            grid.AddComponent<RectTransform>().sizeDelta = new Vector2(_width, _height);

            var image = grid.AddComponent<Image>();
            image.color = _gridColor;
            image.sprite = _gridSprite;
        }

        private void SetupLayoutGroup(GridLayoutGroup layoutGroup)
        {
            layoutGroup.spacing = _spacing;
            layoutGroup.cellSize = _cellSize;
            layoutGroup.childAlignment = TextAnchor.MiddleCenter;
        }
    }
}