using UnityEngine;
using Zenject;

namespace CodeBase.MusicManagement.Sound.Component
{
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;

        [SerializeField] private bool PlayOnAwake;
        
        private ISoundService _soundService;


        [Inject]
        private void Binding(ISoundService soundService)
        {
            _soundService = soundService;
        }
        
        private void Awake()
        {
            if (PlayOnAwake)
            {
                _soundService.Play(audioClip);
            }
        }

        public void Play()
        {
            _soundService.Play(audioClip);
        }

        public void SetMusic(AudioClip setAudioClip)
        {
            audioClip = setAudioClip;
        }
    }
}