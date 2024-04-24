using DG.Tweening;
using HolyWater.MykytaTask.Extension;
using HolyWater.MykytaTask.Infrastructure.Services.Audio;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HolyWater.MykytaTask.UI
{
    public class ModalWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup modalGroup;
        [SerializeField] private Button closeButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private float animationDuration;

        private const string InstagramUrl = "https://www.instagram.com/nikita.stan/";

        private readonly Vector3 startAnimationScale = Vector3.zero;
        private readonly Vector3 endAnimationScale = Vector3.one;
        
        private IAudioService audioService;

        [Inject]
        private void Construct(IAudioService audioService)
        {
            this.audioService = audioService;
        }
        
        private void Start()
        {
            closeButton.onClick.AddListener(OnCloseWindow);
            profileButton.onClick.AddListener(OnOpenProfile);
            modalGroup.State(true);
        }

        public void OpenWindow() => 
            ShowAnimation();

        private void OnCloseWindow()
        {
            audioService.PlayUIClick();
            HideAnimation();
        }

        private void OnOpenProfile()
        {
            audioService.PlayUIClick();
            Application.OpenURL(InstagramUrl);
        }

        private void ShowAnimation()
        {
            modalGroup.State(true);
            transform.localScale = startAnimationScale;
            transform.DOScale(endAnimationScale, animationDuration)
                .SetEase(Ease.InExpo);
        }

        private void HideAnimation()
        {
            transform.DOScale(startAnimationScale, animationDuration)
                .SetEase(Ease.OutExpo)
                .OnComplete(() => modalGroup.State(false));
        }
    }
}