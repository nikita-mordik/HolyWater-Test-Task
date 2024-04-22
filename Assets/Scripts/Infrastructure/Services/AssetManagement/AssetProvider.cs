using System.Collections.Generic;
using System.Linq;
using HolyWater.MykytaTask.StaticData;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.Services.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject InstantiateObject(string path)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject);
        }

        public List<WeatherCoord> LoadWeatherCoords() => 
            Resources.LoadAll<WeatherCoord>("Weather").ToList();
    }
}