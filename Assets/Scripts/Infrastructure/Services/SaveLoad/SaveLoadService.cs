using HolyWater.MykytaTask.Data;
using HolyWater.MykytaTask.Extension;
using HolyWater.MykytaTask.Infrastructure.Services.Factory;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";
        
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;

        public SaveLoadService(IGameFactory gameFactory, IPersistentProgressService progressService)
        {
            this.gameFactory = gameFactory;
            this.progressService = progressService;
        }

        public void SaveProgress()
        {
            foreach (ISavedProgress progressesWriter in gameFactory.ProgressesWriters)
            {
                progressesWriter.UpdateProgress(progressService.SessionProgress);
            }
            
            PlayerPrefs.SetString(ProgressKey, progressService.SessionProgress.ToJson());
        }

        public SessionProgress LoadProgress() => 
            PlayerPrefs.GetString(ProgressKey)?
                .ToDeserialized<SessionProgress>();
    }
}