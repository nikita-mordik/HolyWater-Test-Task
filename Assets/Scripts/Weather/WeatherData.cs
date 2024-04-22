using System;
using System.Collections.Generic;

namespace HolyWater.MykytaTask.Weather
{
    [Serializable]
    public class Coord
    {
        public float lon;
        public float lat;
    }

    [Serializable]
    public class Weather
    {
        public int id;
        public string main;
        public string description;
        public string icon;
    }

    [Serializable]
    public class Main
    {
        public float temp;
        public float feels_like;
        public float temp_min;
        public float temp_max;
        public int pressure;
        public int humidity;
        public int sea_level;
        public int grnd_level;
    }

    [Serializable]
    public class Wind
    {
        public float speed;
        public int deg;
        public float gust;
    }

    [Serializable]
    public class Clouds
    {
        public int all;
    }

    [Serializable]
    public class Sys
    {
        public string country;
        public long sunrise;
        public long sunset;
    }

    [Serializable]
    public class WeatherData
    {
        public Coord coord;
        public List<Weather> weather;
        public string baseString;
        public Main main;
        public int visibility;
        public Wind wind;
        public Clouds clouds;
        public long dt;
        public Sys sys;
        public int timezone;
        public int id;
        public string name;
        public int cod;
    }
}