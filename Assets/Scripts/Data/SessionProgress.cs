using System;

namespace HolyWater.MykytaTask.Data
{
    [Serializable]
    public class SessionProgress
    {
        public LeftWeatherCard LeftWeatherCard;
        public VolumeData VolumeData;
        public bool FirstOrAfterResetSession { get; set; }
    }
}