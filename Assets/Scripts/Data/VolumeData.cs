using System;

namespace HolyWater.MykytaTask.Data
{
    [Serializable]
    public class VolumeData
    {
        public float MusicVolume;
        public float SoundVolume;

        public VolumeData(float musicVolume, float soundVolume)
        {
            MusicVolume = musicVolume;
            SoundVolume = soundVolume;
        }
    }
}