using System.Collections.Generic;
using HolyWater.MykytaTask.StaticData;
using UnityEngine;

namespace HolyWater.MykytaTask
{
    public interface IAssetProvider
    {
        GameObject InstantiateObject(string path);
        List<WeatherCoord> LoadWeatherCoords();
    }
}