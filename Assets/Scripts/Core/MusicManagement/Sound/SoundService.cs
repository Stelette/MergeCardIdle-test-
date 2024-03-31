using System;
using Implementation;
using UnityEngine;

namespace CodeBase.MusicManagement.Sound
{
    public class SoundService : ISoundService
    {
        public event Action<bool> OnSwitchSound;
        public bool IsMute { get; set; }
        
        private readonly AudioSource _audioSource;

        public SoundService(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        public void Init()
        {
            bool mute = PlayerPrefs.GetInt(Constants.SoundKey, 1) == 0;
            SetMute(mute);
        }

        public void Play(AudioClip audioClip)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
        }

        public void SetMute(bool mute)
        {
            _audioSource.mute = mute;
            IsMute = mute;
            OnSwitchSound?.Invoke(mute);
            SaveProgress();
        }
        
        private void SaveProgress()
        {
            PlayerPrefs.SetInt(Constants.SoundKey, IsMute ? 0 : 1);
        }
    }
}