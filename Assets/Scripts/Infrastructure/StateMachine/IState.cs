using System;
using Cysharp.Threading.Tasks;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine
{
    public interface IState : IExitableState
    {
        UniTask Enter(Action action = null);
    }

    public interface IExitableState
    {
        UniTask Exit();
    }
    
    public interface IPayloadedState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload, Action action = null);
    }
}