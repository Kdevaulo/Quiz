using UnityEngine;

namespace Quiz.GridSystem
{
    [AddComponentMenu(nameof(GridView) + " in " + nameof(GridSystem))]
    public class GridView : MonoBehaviour
    {
        public Transform GridParent => _gridParent;

        public SpriteRenderer GridRenderer => _gridRenderer;

        [SerializeField] private Transform _gridParent;

        [SerializeField] private SpriteRenderer _gridRenderer;
    }
}