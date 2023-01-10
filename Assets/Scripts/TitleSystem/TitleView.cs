using UnityEngine;
using UnityEngine.UI;

namespace Quiz.TitleSystem
{
    [AddComponentMenu(nameof(TitleView) + " in " + nameof(TitleSystem))]
    public class TitleView : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public Text GetText()
        {
            return _text;
        }

        public void SetText(string targetText)
        {
            _text.text = "Find " + targetText;
        }
    }
}