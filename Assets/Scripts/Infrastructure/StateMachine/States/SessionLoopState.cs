using System;
using Cysharp.Threading.Tasks;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine.States
{
    public class SessionLoopState : IState
    {
        public async UniTask Enter(Action action = null)
        {
            await UniTask.Yield();
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }
    }
}