using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Infrastructure.SceneLoader;
using HolyWater.MykytaTask.Infrastructure.Services.AssetManagement;
using HolyWater.MykytaTask.Infrastructure.Services.Factory;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using HolyWater.MykytaTask.Infrastructure.Services.WeatherAPI;
using HolyWater.MykytaTask.StaticData;
using HolyWater.MykytaTask.UI;
using HolyWater.MykytaTask.Weather;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.StateMachine.States
{
    public class LoadMainSceneState : IState
    {
        private readonly ISceneLoaderService sceneLoaderService;
        private readonly IGameStateMachine gameStateMachine;
        private readonly IGameFactory gameFactory;
        private readonly IPersistentProgressService progressService;
        private readonly IWeatherService weatherService;
        private readonly IAssetProvider assetProvider;

        public LoadMainSceneState(ISceneLoaderService sceneLoaderService, IGameStateMachine gameStateMachine,
            IGameFactory gameFactory, IPersistentProgressService progressService, IWeatherService weatherService,
            IAssetProvider assetProvider)
        {
            this.sceneLoaderService = sceneLoaderService;
            this.gameStateMachine = gameStateMachine;
            this.gameFactory = gameFactory;
            this.progressService = progressService;
            this.weatherService = weatherService;
            this.assetProvider = assetProvider;
        }
        
        public async UniTask Enter(Action action = null)
        {
            await sceneLoaderService.LoadSceneWithProgress(SceneName.MainScene, OnLoad);
        }

        public async UniTask Exit()
        {
            await UniTask.Yield();
        }

        private async void OnLoad()
        {
            await InitializeSessionAsync();
            InformProgressReaders();
            
            await gameStateMachine.Enter<SessionLoopState>();
        }

        private async UniTask InitializeSessionAsync()
        {
            var weatherWindow = GameObject.FindWithTag("WeatherWindow").GetComponent<WeatherWindow>();
            
            if (progressService.SessionProgress.FirstOrAfterResetSession)
            {
                progressService.SessionProgress.FirstOrAfterResetSession = false;

                List<WeatherCoord> weatherCoords = assetProvider.LoadWeatherCoords();
                List<WeatherCard> weatherCards = new(weatherCoords.Count);
                CreateWeatherCards(weatherWindow, weatherCoords.Count, weatherCards);

                for (var index = 0; index < weatherCoords.Count; index++)
                {
                    var weatherCoord = weatherCoords[index];
                    var weatherCard = weatherCards[index];
                    await FillWeatherCard(weatherCard, weatherCoord.weatherCoordinate);
                }
            }
            
            await InitializeLeftWeatherCards(weatherWindow);
        }

        private async UniTask InitializeLeftWeatherCards(WeatherWindow weatherWindow)
        {
            var leftWeatherIdCount = progressService.SessionProgress.LeftWeatherCard.IdLeftWeatherCard.Count;
            if (leftWeatherIdCount <= 0) return;
            
            List<WeatherCard> weatherCards = new(leftWeatherIdCount);
            CreateWeatherCards(weatherWindow, leftWeatherIdCount, weatherCards);
            
            for (int i = 0; i < leftWeatherIdCount; i++)
            {
                string id = progressService.SessionProgress.LeftWeatherCard.IdLeftWeatherCard[i];
                Coord coord = progressService.SessionProgress.LeftWeatherCard.WeatherCoordinate[i];
                await FillWeatherCard(weatherCards[i], coord);
                weatherCards[i].SetId(id);
            }
        }

        private void CreateWeatherCards(WeatherWindow weatherWindow, int leftWeatherIdCount, List<WeatherCard> weatherCards)
        {
            for (int i = 0; i < leftWeatherIdCount; i++)
            {
                var weatherCard = gameFactory.CreateWeatherCard(AssetsPath.WeatherCard, weatherWindow.Content)
                    .GetComponent<WeatherCard>();
                weatherCards.Add(weatherCard);
            }
        }

        private async UniTask FillWeatherCard(WeatherCard weatherCard, Coord coord)
        {
            WeatherData weatherData = await weatherService.SendWeatherRequest(coord.lat, coord.lon);
            Texture2D weatherIcon = await weatherService.LoadWeatherIconAsync(weatherData.weather[0].icon);
            weatherCard.FillData(weatherIcon, weatherData.main.temp, weatherData.name, coord);
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