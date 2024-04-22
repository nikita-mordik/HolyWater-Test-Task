using HolyWater.MykytaTask.Weather;
using UnityEngine;

namespace HolyWater.MykytaTask.StaticData
{
    [CreateAssetMenu(fileName = "WeatherCoordinate", menuName = "Weather")]
    public class WeatherCoord : ScriptableObject
    {
        public Coord weatherCoordinate;
    }
}