using System;
using Implementation;
using UnityEngine;

namespace CodeBase.MusicManagement.Music
{
    public class MusicService : IMusicService
    {
        public event Action<bool> OnSwitchMusic;
        public bool IsMute { get; set; }
        
        private readonly AudioSource _audioSource;

        public void Init()
        {
            bool mute = PlayerPrefs.GetInt(Constants.MusicKey, 1) == 0;
            SetMute(mute);
        }

        public MusicService(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void Pause()
        {
            _audioSource.Pause();;
        }

        public void Play()
        {
            _audioSource.Play();
        }

        public void SetMusic(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
        }

        public void SetMute(bool mute)
        {
            _audioSource.mute = mute;
            IsMute = mute;
            OnSwitchMusic?.Invoke(mute);
            SaveProgress();
        }

        private void SaveProgress()
        {
            PlayerPrefs.SetInt(Constants.MusicKey, IsMute ? 0 : 1);
        }
    }
}