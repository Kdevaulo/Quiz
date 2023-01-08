using System;
using System.Threading;

using Cysharp.Threading.Tasks;

using UnityEngine;

namespace Quiz.SoundSystem
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _wrongSound;

        [SerializeField] private AudioSource _correctSound;

        [SerializeField] private AudioSource _backgroundSound;

        public async UniTask PlayWrongSoundAsync(CancellationToken token)
        {
            await PlaySoundAsync(_wrongSound, token);
        }

        public async UniTask PlayCorrectSoundAsync(CancellationToken token)
        {
            await PlaySoundAsync(_correctSound, token);
        }

        public async UniTask PlayBackgroundSoundAsync(CancellationToken token)
        {
            var backgroundSound = _backgroundSound;
            backgroundSound.Play();

            await UniTask.Delay(TimeSpan.FromSeconds(backgroundSound.clip.length), cancellationToken: token);
        }

        private async UniTask PlaySoundAsync(AudioSource audioSource, CancellationToken token)
        {
            audioSource.PlayOneShot(audioSource.clip);

            await UniTask.Delay(TimeSpan.FromSeconds(audioSource.clip.length), cancellationToken: token);
        }
    }
}