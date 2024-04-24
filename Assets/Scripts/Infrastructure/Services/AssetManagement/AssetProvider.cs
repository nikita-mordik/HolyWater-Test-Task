using System.Collections.Generic;
using System.Linq;
using HolyWater.MykytaTask.StaticData;
using UnityEngine;
using Zenject;

namespace HolyWater.MykytaTask.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IInstantiator instantiator;

        public AssetProvider(IInstantiator instantiator)
        {
            this.instantiator = instantiator;
        }
        
        public GameObject InstantiateObject(string path)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return instantiator.InstantiatePrefab(gameObject);
        }

        public List<WeatherCoord> LoadWeatherCoords() => 
            Resources.LoadAll<WeatherCoord>(AssetsPath.AllWeatherData).ToList();
    }
}