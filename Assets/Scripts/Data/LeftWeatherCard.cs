using System;
using System.Collections.Generic;
using HolyWater.MykytaTask.Weather;

namespace HolyWater.MykytaTask.Data
{
    [Serializable]
    public class LeftWeatherCard
    {
        public List<string> IdLeftWeatherCard = new();
        public List<Coord> WeatherCoordinate = new();
    }
}