using UnityEngine;
using UnityEngine.Events;

public class Restart : MonoBehaviour
{
    [SerializeField] private float _delay;
    public UnityEvent GameRestarted;

    public void RestartGame()
    {
        Invoke(nameof(RemoveAllAndRestart), _delay);
        Invoke(nameof(GameRestart), _delay);
    }
    private void RemoveAllAndRestart()
    {
        Transform parent = gameObject.transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            Destroy(parent.GetChild(i).gameObject);
        }
    }
    private void GameRestart()
    {
        GameRestarted.Invoke();
    }
}
