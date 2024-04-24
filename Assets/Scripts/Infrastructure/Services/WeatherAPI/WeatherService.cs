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
        private const string ImageUrl = "http://openweathermap.org/img/wn/{0}@2x.png";
        private const string APIKey = "e381a1652ef2ff5ae18cf05283ed468a";

        public async UniTask<WeatherData> SendWeatherRequest(float latitude, float longitude)
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

        public async UniTask<Texture2D> LoadWeatherIconAsync(string iconId)
        {
            var url = string.Format(ImageUrl, iconId);

            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                await request.SendWebRequest().ToUniTask();

                if (request.result is UnityWebRequest.Result.ConnectionError or UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogError("Failed to load image: " + request.error);
                    return null;
                }

                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                return texture;
            }
        }
    }
}