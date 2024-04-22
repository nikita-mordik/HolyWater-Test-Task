using Zenject;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine
{
    public class StateFactory : IStateFactory
    {
        private IInstantiator instantiator;

        public StateFactory(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }

        public TState Create<TState>() where TState : IExitableState => 
            instantiator.Instantiate<TState>();
    }
}