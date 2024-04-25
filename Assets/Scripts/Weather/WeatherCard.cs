using System;
using DG.Tweening;
using HolyWater.MykytaTask.Data;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HolyWater.MykytaTask.Weather
{
    public class WeatherCard : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private Button weatherButton;
        [SerializeField] private Image coverImage;
        
        [Header("Weather data")]
        [SerializeField] private Image weatherIcon;
        [SerializeField] private TextMeshProUGUI degreesText;
        [SerializeField] private TextMeshProUGUI cityText;

        private const float KelvinTemperature = 300f;
        private const float AnimationDuration = 0.5f;

        private bool isClicked;
        private string id;
        private Coord coord;
        private SessionProgress sessionProgress;
        
        private void Start()
        {
            weatherButton.onClick.AddListener(OnClickWeatherCard);
            weatherIcon.DOFade(0f, 0f);
        }

        public void FillData(Texture2D icon, float degrees, string city, Coord coordinate)
        {
            float celsiusTemperature = KelvinTemperature - degrees;
            degreesText.text = $"{celsiusTemperature:F2}°C";
            cityText.text = city;
            coord = coordinate;
         
            var loadIcon = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
            coverImage.DOColor(Color.clear, AnimationDuration).OnComplete(() =>
            {
                weatherIcon.sprite = loadIcon;
                weatherIcon.DOFade(1f, AnimationDuration);
                coverImage.raycastTarget = false;
            });
        }

        public void LoadProgress(SessionProgress progress) => 
            SetProgress(progress);

        public void UpdateProgress(SessionProgress progress)
        {
            SetProgress(progress);

            if (isClicked || IsCardExist()) return;
            
            SetId(GenerateId());
            progress.LeftWeatherCard.IdLeftWeatherCard.Add(id);
            progress.LeftWeatherCard.WeatherCoordinate.Add(coord);
        }
        
        public void SetId(string cardId) => 
            id = cardId;

        private void OnClickWeatherCard()
        {
            isClicked = true;
            
            if (IsCardExist())
            {
                sessionProgress.LeftWeatherCard.IdLeftWeatherCard.Remove(id);
                sessionProgress.LeftWeatherCard.WeatherCoordinate.Remove(coord);
            }
            
            Destroy(gameObject, 0.25f);
        }

        private void SetProgress(SessionProgress progress) => 
            sessionProgress ??= progress;

        private string GenerateId() => 
            $"{gameObject.scene.name}_{Guid.NewGuid().ToString()}";

        private bool IsCardExist() =>
            sessionProgress.LeftWeatherCard.IdLeftWeatherCard.Count > 0 &&
            sessionProgress.LeftWeatherCard.IdLeftWeatherCard.Contains(id);
    }
}