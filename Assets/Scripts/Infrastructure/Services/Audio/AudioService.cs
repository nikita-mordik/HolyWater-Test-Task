using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using UnityEngine;
using Zenject;

namespace HolyWater.MykytaTask.Infrastructure.Services.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource soundSource;

        private const float AudioFadeDuration = 1.5f;
        
        private IPersistentProgressService progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService)
        {
            this.progressService = progressService;

            if (progressService.SessionProgress.VolumeData.MusicVolume <= 0f)
                musicSource.volume = 0f;
        }
        
        public void PlayUIClick() => 
            soundSource.Play();

        public async UniTask ChangeMusicVolume(bool state)
        {
            var timer = 0f;
            var startVolume = musicSource.volume;

            while (timer <= AudioFadeDuration)
            {
                timer += Time.deltaTime;
                musicSource.volume = Mathf.Lerp(startVolume, state ? 1f : 0f, timer / AudioFadeDuration);
                await UniTask.Yield(PlayerLoopTiming.Update);
            }

            progressService.SessionProgress.VolumeData.MusicVolume = musicSource.volume;
        }

        public void ChangeSoundVolume(bool state)
        {
            soundSource.volume = state ? 1f : 0f;
            progressService.SessionProgress.VolumeData.SoundVolume = soundSource.volume;
        }
    }
}