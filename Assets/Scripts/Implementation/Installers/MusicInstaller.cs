using CodeBase.MusicManagement.Music;
using CodeBase.MusicManagement.Sound;
using UnityEngine;
using Zenject;

namespace CodeBase.Installers
{
    public class MusicInstaller : MonoInstaller
    {
        [SerializeField] private AudioSource musicServicePrefab;
        
        [SerializeField] private AudioSource soundServicePrefab;
        public override void InstallBindings()
        {
            RegisterMusicService();
            RegisterSoundService();
        }

        private void RegisterMusicService()
        {
            AudioSource musicSource = Instantiate(musicServicePrefab);
            DontDestroyOnLoad(musicSource);
            IMusicService musicService = new MusicService(musicSource);
            musicService.Init();
            Container.Bind<IMusicService>().FromInstance(musicService).AsSingle();
        }
        
        private void RegisterSoundService()
        {
            AudioSource soundSource = Instantiate(soundServicePrefab);
            DontDestroyOnLoad(soundSource);
            ISoundService soundService = new SoundService(soundSource);
            soundService.Init();
            Container.Bind<ISoundService>().FromInstance(soundService).AsSingle();
        }
    }
}