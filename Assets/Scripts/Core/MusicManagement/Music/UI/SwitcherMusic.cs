using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.MusicManagement.Music.UI
{
    public class SwitcherMusic : MonoBehaviour
    {
        [SerializeField] private Sprite MusicOnS;
        [SerializeField] private Sprite MusicOffS;

        [SerializeField] private Image SwitcherBtn;


        private IMusicService _musicService;

        [Inject]
        private void Binding(IMusicService musicService)
        {
            _musicService = musicService;
        }

        private void Awake()
        {
            _musicService.OnSwitchMusic += UpdateUI;
            UpdateUI(_musicService.IsMute);
        }

        public void SwitchMusic()
        {
            _musicService.SetMute(!_musicService.IsMute);
        }

        private void UpdateUI(bool musicOff)
        {
            SwitcherBtn.sprite = musicOff ? MusicOffS : MusicOnS;
        }

        private void OnDestroy()
        {
            if (_musicService != null)
                _musicService.OnSwitchMusic -= UpdateUI;
        }
    }
}