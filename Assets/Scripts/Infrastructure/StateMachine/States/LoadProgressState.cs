using System;
using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Data;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using HolyWater.MykytaTask.Infrastructure.Services.SaveLoad;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine.States
{
    public class LoadProgressState : IState
    {
        private readonly IPersistentProgressService persistentProgressService;
        private readonly ISaveLoadService saveLoadService;
        private readonly IGameStateMachine gameStateMachine;

        public LoadProgressState(IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService,
            IGameStateMachine gameStateMachine)
        {
            this.persistentProgressService = persistentProgressService;
            this.saveLoadService = saveLoadService;
            this.gameStateMachine = gameStateMachine;
        }
        
        public async UniTask Enter(Action action = null)
        {
            LoadProgressOrInitNew();
            await gameStateMachine.Enter<LoadMainSceneState>();
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }

        private void LoadProgressOrInitNew()
        {
            persistentProgressService.SessionProgress = saveLoadService.LoadProgress() ?? NewProgress();
        }

        private static SessionProgress NewProgress()
        {
            var progress = new SessionProgress
            {
                LeftWeatherCard = new LeftWeatherCard(),
                VolumeData = new VolumeData(1f, 1f),
                FirstOrAfterResetSession = true
            };
            return progress;
        }
    }
}