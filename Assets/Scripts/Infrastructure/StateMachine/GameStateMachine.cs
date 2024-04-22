using System;
using Cysharp.Threading.Tasks;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly IStateFactory stateFactory;

        private IExitableState activeState;

        public GameStateMachine(IStateFactory stateFactory)
        {
            this.stateFactory = stateFactory;
        }

        public async UniTask Enter<TState>(Action action = null) where TState : class, IState
        {
            activeState = await ChangeState<TState>();
            await ((TState)activeState).Enter(action);
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            activeState = await ChangeState<TState>();
            await ((TState)activeState).Enter(payload);
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (activeState != null)
                await activeState.Exit();
            
            var currentState = stateFactory.Create<TState>();
            return currentState;
        }
    }
}