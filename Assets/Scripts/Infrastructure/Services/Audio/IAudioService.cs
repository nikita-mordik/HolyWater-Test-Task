using Cysharp.Threading.Tasks;

namespace HolyWater.MykytaTask.Infrastructure.Services.Audio
{
    public interface IAudioService
    {
        void PlayUIClick();
        UniTask ChangeMusicVolume(bool state);
        void ChangeSoundVolume(bool state);
    }
}