using System.Collections.Generic;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider assetProvider;
        
        public List<ISavedProgressReader> ProgressReaders { get; } = new();
        public List<ISavedProgress> ProgressesWriters { get; } = new();

        public GameFactory(IAssetProvider assetProvider)
        {
            this.assetProvider = assetProvider;
        }
        
        public GameObject CreateWeatherCard(string path, Transform parent)
        {
            var gameObject = assetProvider.InstantiateObject(path);
            gameObject.transform.SetParent(parent);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        public void CleanReaders() => 
            ProgressReaders.Clear();

        public void CleanWriters() => 
            ProgressesWriters.Clear();

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                RegisterProgressReader(progressReader);
            }
        }

        private void RegisterProgressReader(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressesWriters.Add(progressWriter);
            
            ProgressReaders.Add(progressReader);
        }
    }
}