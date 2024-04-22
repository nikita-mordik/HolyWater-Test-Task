using System;
using Cysharp.Threading.Tasks;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine
{
    public interface IGameStateMachine
    {
        UniTask Enter<TState>(Action action = null) where TState : class, IState;
        UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    }
}