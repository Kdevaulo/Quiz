using System.Linq;

using UnityEngine;
using UnityEngine.Assertions;

namespace Quiz.GridSystem
{
    [CreateAssetMenu(fileName = nameof(GridModel), menuName = nameof(GridModel) + " in " + nameof(GridSystem))]
    public class GridModel : ScriptableObject
    {
        [SerializeField] private GridData[] _gridDataCollection;

        public GridData GetGridData(int pointsCount)
        {
            var gridData = _gridDataCollection.FirstOrDefault(x => x.PointsCount == pointsCount);

            Assert.IsNotNull(gridData, $"{nameof(GridModel)} {nameof(GetGridData)} " +
                                       $"Can't find dataCollection with pointsCount == {pointsCount}");

            return gridData;
        }
    }
}