using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HolyWater.MykytaTask.Infrastructure.Services.ProgressBar;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HolyWater.MykytaTask.Infrastructure.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly IProgressBarService progressBarService;

        public SceneLoaderService(IProgressBarService progressBarService)
        {
            this.progressBarService = progressBarService;
        }
        
        public bool IsSceneLoaded(string sceneName) => 
            SceneManager.GetSceneByName(sceneName).isLoaded;

        public async UniTask LoadScene(string sceneName, Action onSceneLoad = null)
        {
            var handler = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            await handler.ToUniTask();
            onSceneLoad?.Invoke();
        }

        public async UniTask LoadSceneWithProgress(string sceneName, Action onSceneLoad = null)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            while (operation != null && !operation.isDone)
            {
                var fillAmount = Mathf.Clamp01(operation.progress / 0.9f);
                progressBarService.ProgressBarImage.fillAmount = fillAmount;
                
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            onSceneLoad?.Invoke();
        }
    }
}