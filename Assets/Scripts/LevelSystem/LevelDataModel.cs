using UnityEngine;

namespace Quiz.LevelSystem
{
    [CreateAssetMenu(fileName = nameof(LevelDataModel),
        menuName = nameof(LevelDataModel) + " in " + nameof(LevelSystem))]
    public class LevelDataModel : ScriptableObject
    {
        [SerializeField] private LevelData[] _levelData;

        public LevelData[] GetLevelDataCollection()
        {
            return _levelData;
        }
    }
}