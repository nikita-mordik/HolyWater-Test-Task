using System;
using DG.Tweening;
using HolyWater.MykytaTask.Extension;
using HolyWater.MykytaTask.Infrastructure.Services.Audio;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HolyWater.MykytaTask.UI
{
    public class SettingWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup settingGroup;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button modalWindowButton;

        [SerializeField] private Toggle musicToggle;
        [SerializeField] private Toggle soundToggle;

        [SerializeField] private ModalWindow modalWindow;
        
        private const float AnimationDuration = 1f;
        private const float EndFadeValue = 0f;
        private const float StartFadeValue = 1f;

        private IAudioService audioService;
        private IPersistentProgressService progressService;

        [Inject]
        private void Construct(IAudioService audioService, IPersistentProgressService progressService)
        {
            this.progressService = progressService;
            this.audioService = audioService;
        }
        
        private void Start()
        {
            settingGroup.State(false);
            
            closeButton.onClick.AddListener(OnCloseWindow);
            modalWindowButton.onClick.AddListener(OnOpenModalWindow);
            
            musicToggle.onValueChanged.AddListener(OnMusicChange);
            soundToggle.onValueChanged.AddListener(OnSoundChange);

            if (musicToggle.isOn && progressService.SessionProgress.VolumeData.MusicVolume <= 0f)
            {
                musicToggle.isOn = false;
            }
            
            if (soundToggle.isOn && progressService.SessionProgress.VolumeData.SoundVolume <= 0f)
            {
                soundToggle.isOn = false;
            }
        }

        public void ShowWindow()
        {
            settingGroup.DOFade(StartFadeValue, AnimationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() => settingGroup.State(true));
        }

        private void OnCloseWindow()
        {
            audioService.PlayUIClick();
            HideWindow();
        }

        private void OnOpenModalWindow()
        {
            audioService.PlayUIClick();
            HideWindow(modalWindow.OpenWindow);
        }

        private void OnMusicChange(bool state) => 
            audioService.ChangeMusicVolume(state);

        private void OnSoundChange(bool state) => 
            audioService.ChangeSoundVolume(state);

        private void HideWindow(Action onHide = null)
        {
            settingGroup.DOFade(EndFadeValue, AnimationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    settingGroup.State(false);
                    onHide?.Invoke();
                });
        }
    }
}