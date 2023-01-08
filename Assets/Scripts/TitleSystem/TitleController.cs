using System;

using Cysharp.Threading.Tasks;

using Quiz.Utils;

using UnityEngine.Events;
using UnityEngine.UI;

namespace Quiz.TitleSystem
{
    public class TitleController : IDisposable
    {
        private readonly TitleView _titleView;

        private readonly UnityEvent<string> _correctItemChosen;

        private readonly ColorFader<Text> _colorFader = new ColorFader<Text>();

        public TitleController(TitleView titleView, UnityEvent<string> correctItemChosen)
        {
            correctItemChosen.AddListener(SetTitle);
            _titleView = titleView;
            _correctItemChosen = correctItemChosen;
        }

        void IDisposable.Dispose()
        {
            _correctItemChosen.RemoveListener(SetTitle);
        }

        private void SetTitle(string targetText)
        {
            _titleView.SetText(targetText);

            _colorFader.FadeIn(1.5f, _titleView.GetText()).Forget();
        }
    }
}