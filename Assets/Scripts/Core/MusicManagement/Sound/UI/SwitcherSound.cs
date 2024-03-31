using CodeBase.MusicManagement.Music;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.MusicManagement.Sound.UI
{
    public class SwitcherSound : MonoBehaviour
    {
        [SerializeField] private Sprite MusicOnS;
        [SerializeField] private Sprite MusicOffS;

        [SerializeField] private Image SwitcherBtn;


        private ISoundService _soundService;

        [Inject]
        private void Binding(ISoundService soundService)
        {
            _soundService = soundService;
        }

        private void Awake()
        {
            _soundService.OnSwitchSound += UpdateUI;
            UpdateUI(_soundService.IsMute);
        }

        public void SwitchMusic()
        {
            _soundService.SetMute(!_soundService.IsMute);
        }

        private void UpdateUI(bool musicOff)
        {
            SwitcherBtn.sprite = musicOff ? MusicOffS : MusicOnS;
        }

        private void OnDestroy()
        {
            if (_soundService != null)
                _soundService.OnSwitchSound -= UpdateUI;
        }
    }
}