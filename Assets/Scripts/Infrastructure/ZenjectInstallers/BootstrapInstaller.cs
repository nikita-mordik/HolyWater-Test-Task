using HolyWater.MykytaTask.Infrastructure.SceneLoader;
using HolyWater.MykytaTask.Infrastructure.Services.AssetManagement;
using HolyWater.MykytaTask.Infrastructure.Services.Factory;
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
    }
}