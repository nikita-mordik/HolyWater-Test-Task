namespace HolyWater.MykytaTask.Infrastructure.StateMachine
{
    public interface IStateFactory
    {
        TState Create<TState>() where TState : IExitableState;
    }
}