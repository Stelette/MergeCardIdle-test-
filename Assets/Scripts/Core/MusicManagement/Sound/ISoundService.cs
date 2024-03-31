using System;
using UnityEngine;

namespace CodeBase.MusicManagement.Sound
{
    public interface ISoundService
    {
        public event Action<bool> OnSwitchSound;
        
        public bool IsMute { get; set; }
        
        void Init();
        void Play(AudioClip audioClip);
        void SetMute(bool mute);
    }
}