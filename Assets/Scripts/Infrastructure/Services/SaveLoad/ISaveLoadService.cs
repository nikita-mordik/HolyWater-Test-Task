using HolyWater.MykytaTask.Data;

namespace HolyWater.MykytaTask.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        void SaveProgress();
        SessionProgress LoadProgress();
    }
}