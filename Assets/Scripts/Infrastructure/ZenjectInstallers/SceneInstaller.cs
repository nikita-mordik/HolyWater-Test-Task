using HolyWater.MykytaTask.Infrastructure.Services.Audio;
using Zenject;

namespace HolyWater.MykytaTask.Infrastructure.ZenjectInstallers
{
    public class SceneInstaller : MonoInstaller
    {
        public AudioService AudioService;
        
        public override void InstallBindings()
        {
            BindAudioService();
        }

        private void BindAudioService()
        {
            Container.Bind<IAudioService>()
                .To<AudioService>()
                .FromComponentInNewPrefab(AudioService)
                .AsSingle();
        }
    }
}