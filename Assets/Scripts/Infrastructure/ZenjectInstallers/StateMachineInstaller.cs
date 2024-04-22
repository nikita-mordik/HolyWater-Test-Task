using HolyWater.MykytaTask.Infrastructure.StateMachine;
using Zenject;

namespace HolyWater.MykytaTask.Infrastructure.ZenjectInstallers
{
    public class StateMachineInstaller : Installer<StateMachineInstaller>
    {
        public override void InstallBindings()
        {
            BindStateFactory();
            BindGameStateMachine();
        }

        private void BindStateFactory()
        {
            Container.Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindGameStateMachine()
        {
            Container.Bind<IGameStateMachine>()
                .To<GameStateMachine>()
                .AsSingle()
                .NonLazy();
        }
    }
}