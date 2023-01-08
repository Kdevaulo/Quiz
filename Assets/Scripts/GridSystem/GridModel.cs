using UnityEngine;

namespace Quiz.GridSystem
{
    [CreateAssetMenu(fileName = nameof(GridModel), menuName = nameof(GridModel) + " in " + nameof(GridSystem))]
    public class GridModel : ScriptableObject
    {
        [field: SerializeField] public Color GridColor { get; private set; }

        [field: SerializeField] public Sprite GridSprite { get; private set; }

        [field: SerializeField] public Vector2 Spacing { get; private set; }

        [field: SerializeField] public Vector2 CellSize { get; private set; }

        [field: SerializeField] public int ObjectWidthCount { get; private set; }
    }
}