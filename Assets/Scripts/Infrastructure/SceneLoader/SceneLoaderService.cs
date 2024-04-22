using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HolyWater.MykytaTask.Infrastructure.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public bool IsSceneLoaded(string sceneName) => 
            SceneManager.GetSceneByName(sceneName).isLoaded;

        public async UniTask LoadScene(string sceneName, Action onSceneLoad = null)
        {
            var handler = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            await handler.ToUniTask();
            onSceneLoad?.Invoke();
        }

        public async UniTask LoadSceneWithProgress(string sceneName, object fillAmount, Action onSceneLoad = null)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            // Continuously update the fill amount of the radial image based on the loading progress
            while (!operation.isDone)
            {
                fillAmount = Mathf.Clamp01(operation.progress / 0.9f); // Clamp progress value between 0 and 1

                // Wait for the next frame to avoid blocking the main thread
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
            
            onSceneLoad?.Invoke();
        }
    }
}