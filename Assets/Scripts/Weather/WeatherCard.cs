using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HolyWater.MykytaTask.Weather
{
    public class WeatherCard : MonoBehaviour
    {
        [SerializeField] private Button weatherButton;
        
        [Header("Weather data")]
        [SerializeField] private Image weatherIcon;
        [SerializeField] private TextMeshProUGUI degreesText;
        [SerializeField] private TextMeshProUGUI cityText;

        private void Start()
        {
            weatherButton.onClick.AddListener(OnClickWeatherCard);
        }

        public void FillData(Sprite icon, string degrees, string city)
        {
            weatherIcon.sprite = icon;
            degreesText.text = degrees;
            cityText.text = city;
        }

        private void OnClickWeatherCard()
        {
            Destroy(gameObject);
        }
    }
}