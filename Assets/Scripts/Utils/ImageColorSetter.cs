using UnityEngine;
using UnityEngine.UI;

using Random = System.Random;

namespace Quiz.Utils
{
    public class ImageColorSetter : MonoBehaviour
    {
        [SerializeField] private Color[] _colors;

        [SerializeField] private Image _image;

        private void OnEnable()
        {
            _image.color = ChooseColor();
        }

        private Color ChooseColor()
        {
            Random random = new Random(GetInstanceID());

            return _colors[random.Next(_colors.Length)];
        }
    }
}