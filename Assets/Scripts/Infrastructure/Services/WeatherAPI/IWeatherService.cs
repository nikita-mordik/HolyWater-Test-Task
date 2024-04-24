using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Weather;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.Services.WeatherAPI
{
    public interface IWeatherService
    {
        UniTask<WeatherData> SendWeatherRequest(float latitude, float longitude);
        UniTask<Texture2D> LoadWeatherIconAsync(string iconId);
    }
}