using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Weather;

namespace HolyWater.MykytaTask.Infrastructure.Services.WeatherAPI
{
    public interface IWeatherService
    {
        UniTask<WeatherData> SendWeatherRequest(int latitude, int longitude);
    }
}