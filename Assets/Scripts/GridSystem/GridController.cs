using System;

using Quiz.Factories;

using UnityEngine;
using UnityEngine.Events;

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

        private readonly Vector2[] _gridPositions;

        private readonly CardCreator _cardCreator;

        private readonly GridView _gridView;

        private readonly GridModel _gridModel;

        private readonly UnityEvent<int> _levelStarted;

        private float _width;

        private float _height;

        private GameObject _grid;

        public GridController(CardCreator cardCreator, GridView gridView, GridModel gridModel,
            UnityEvent<int> levelStarted)
        {
            _cardCreator = cardCreator;
            _gridView = gridView;
            _gridModel = gridModel;
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

            _grid.transform.SetParent(_gridView.GridParent);

            var gridData = _gridModel.GetGridData(cardsCount);

            SetGridSize(gridData.GridSize);

            _cardCreator.CreateCards(cardsCount, _grid.transform, gridData.PointsPositions);
        }

        private void SetGridSize(Vector2 gridSize)
        {
            _gridView.GridRenderer.size = gridSize;
        }
    }
}