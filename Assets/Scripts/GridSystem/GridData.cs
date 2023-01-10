using System;

using UnityEngine;

namespace Quiz.GridSystem
{
    [Serializable]
    public class GridData
    {
        public int PointsCount => _pointsCount;

        public Vector2 GridSize => _gridSize;

        public Vector2[] PointsPositions => _pointsPositions;

        [SerializeField] private int _pointsCount;

        [SerializeField] private Vector2 _gridSize;

        [SerializeField] private Vector2[] _pointsPositions;
    }
}