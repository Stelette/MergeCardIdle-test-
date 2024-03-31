using System;
using UnityEngine;

namespace CodeBase.MusicManagement.Music
{
    public interface IMusicService
    {
        public event Action<bool> OnSwitchMusic;
        
        public bool IsMute { get; set; }

        void Init();
        void Pause();
        void Play();
        void SetMusic(AudioClip audioClip);

        void SetMute(bool mute);
    }
}