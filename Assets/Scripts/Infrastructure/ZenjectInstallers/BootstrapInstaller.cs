using HolyWater.MykytaTask.Infrastructure.SceneLoader;
using HolyWater.MykytaTask.Infrastructure.Services.AssetManagement;
using HolyWater.MykytaTask.Infrastructure.Services.Factory;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using HolyWater.MykytaTask.Infrastructure.Services.ProgressBar;
using HolyWater.MykytaTask.Infrastructure.Services.SaveLoad;
using HolyWater.MykytaTask.Infrastructure.Services.WeatherAPI;
using HolyWater.MykytaTask.Infrastructure.StateMachine;
using Zenject;

namespace HolyWater.MykytaTask.Infrastructure.ZenjectInstallers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindSceneLoaderService();
            BindAssetsProvider();
            BindGameFactory();
            BindWeatherService();
            BindProgressBarService();
            BindPersistentProgressService();
            BindSaveLoadService();
        }
        
        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .FromSubContainerResolve()
                .ByInstaller<StateMachineInstaller>()
                .AsSingle();
        }
        
        private void BindSceneLoaderService()
        {
            Container.Bind<ISceneLoaderService>()
                .To<SceneLoaderService>()
                .AsSingle();
        }

        private void BindAssetsProvider()
        {
            Container.Bind<IAssetProvider>()
                .To<AssetProvider>()
                .AsSingle();
        }

        private void BindGameFactory()
        {
            Container.Bind<IGameFactory>()
                .To<GameFactory>()
                .AsSingle();
        }

        private void BindWeatherService()
        {
            Container.Bind<IWeatherService>()
                .To<WeatherService>()
                .AsSingle();
        }

        private void BindProgressBarService()
        {
            Container.Bind<IProgressBarService>()
                .To<ProgressBarService>()
                .AsSingle();
        }

        private void BindPersistentProgressService()
        {
            Container.Bind<IPersistentProgressService>()
                .To<PersistentProgressService>()
                .AsSingle();
        }

        private void BindSaveLoadService()
        {
            Container.Bind<ISaveLoadService>()
                .To<SaveLoadService>()
                .AsSingle();
        }
    }
}