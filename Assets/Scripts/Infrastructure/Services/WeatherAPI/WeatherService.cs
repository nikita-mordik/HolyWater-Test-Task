using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Extension;
using HolyWater.MykytaTask.Weather;
using UnityEngine;
using UnityEngine.Networking;

namespace HolyWater.MykytaTask.Infrastructure.Services.WeatherAPI
{
    public class WeatherService : IWeatherService
    {
        private const string APIAddress = "https://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid={2}";
        private const string APIKey = "e381a1652ef2ff5ae18cf05283ed468a";

        public async UniTask<WeatherData> SendWeatherRequest(int latitude, int longitude)
        {
            var data = string.Format(APIAddress, latitude, longitude, APIKey);

            UnityWebRequest request = new UnityWebRequest(data);
            request.downloadHandler = new DownloadHandlerBuffer();

            await request.SendWebRequest().ToUniTask();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                var downloadHandlerText = request.downloadHandler.text;
                var weatherData = downloadHandlerText.ToDeserialized<WeatherData>();
                return weatherData;
            }

            Debug.LogError("Error");
            return null;
        }
    }
}