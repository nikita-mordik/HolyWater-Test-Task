using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Infrastructure.Services.AssetManagement;
using HolyWater.MykytaTask.Infrastructure.Services.Audio;
using HolyWater.MykytaTask.Infrastructure.Services.Factory;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using HolyWater.MykytaTask.Infrastructure.Services.SaveLoad;
using HolyWater.MykytaTask.Infrastructure.Services.WeatherAPI;
using HolyWater.MykytaTask.StaticData;
using HolyWater.MykytaTask.Weather;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace HolyWater.MykytaTask.UI
{
    public class WeatherWindow : MonoBehaviour
    {
        [Header("Common helpers")]
        [SerializeField] private SettingWindow settingWindow;
        [SerializeField] private CanvasGroup weatherGroup;
        [SerializeField] private Transform content;
        
        [Header("Buttons")]
        [SerializeField] private Button exitButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button resetButton;

        public Transform Content => content;
        
        private IPersistentProgressService progressService;
        private IAssetProvider assetProvider;
        private IWeatherService weatherService;
        private IGameFactory gameFactory;
        private ISaveLoadService saveLoadService;
        private IAudioService audioService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, IAssetProvider assetProvider,
            IWeatherService weatherService, IGameFactory gameFactory, ISaveLoadService saveLoadService,
            IAudioService audioService)
        {
            this.audioService = audioService;
            this.saveLoadService = saveLoadService;
            this.gameFactory = gameFactory;
            this.weatherService = weatherService;
            this.assetProvider = assetProvider;
            this.progressService = progressService;
        }

        private void Start()
        {
            exitButton.onClick.AddListener(OnExitApp);
            settingsButton.onClick.AddListener(OnOpenSettings);
            resetButton.onClick.AddListener(OnResetWeatherCards);
        }

        private void OnApplicationQuit()
        {
            saveLoadService.SaveProgress();
        }

        private void OnExitApp()
        {
            audioService.PlayUIClick();
            saveLoadService.SaveProgress();
            Application.Quit();
        }

        private void OnOpenSettings()
        {
            audioService.PlayUIClick();
            settingWindow.ShowWindow();
        }

        private async void OnResetWeatherCards()
        {
            audioService.PlayUIClick();
            ResetProgressSessionData();
            CleanContent();
            await CreateNewWeatherCards();
            InformProgressReaders();
        }

        private void ResetProgressSessionData()
        {
            progressService.SessionProgress.FirstOrAfterResetSession = true;
            progressService.SessionProgress.LeftWeatherCard.IdLeftWeatherCard.Clear();
            progressService.SessionProgress.LeftWeatherCard.WeatherCoordinate.Clear();
            
            gameFactory.CleanWriters();
            gameFactory.CleanReaders();
        }

        private void CleanContent()
        {
            if (content.childCount > 0)
            {
                for (int i = content.childCount - 1; i >= 0; i--)
                {
                    Destroy(content.GetChild(i).gameObject);
                }
            }
        }

        private async UniTask CreateNewWeatherCards()
        {
            List<WeatherCoord> weatherCoords = assetProvider.LoadWeatherCoords();
            for (int i = 0; i < weatherCoords.Count; i++)
            {
                Coord weatherCoordinate = weatherCoords[i].weatherCoordinate;
                await FillWeatherCard(weatherCoordinate);
            }
        }

        private async UniTask FillWeatherCard(Coord coord)
        {
            WeatherData weatherData = await weatherService.SendWeatherRequest(coord.lat, coord.lon);
            Texture2D weatherIcon = await weatherService.LoadWeatherIconAsync(weatherData.weather[0].icon);
                
            var weatherCard = gameFactory.CreateWeatherCard(AssetsPath.WeatherCard, content)
                .GetComponent<WeatherCard>();
            weatherCard.FillData(weatherIcon,weatherData.main.temp, weatherData.name, coord);
        }
        
        private void InformProgressReaders()
        {
            foreach (ISavedProgressReader progressReader in gameFactory.ProgressReaders)
            {
                progressReader.LoadProgress(progressService.SessionProgress);
            }
        }
    }
}