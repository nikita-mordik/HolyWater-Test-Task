using System;
using Cysharp.Threading.Tasks;

namespace HolyWater.MykytaTask.Infrastructure.SceneLoader
{
    public interface ISceneLoaderService
    {
        bool IsSceneLoaded(string sceneName);
        UniTask LoadScene(string sceneName, Action onSceneLoad = null);
        UniTask LoadSceneWithProgress(string sceneName, object fillAmount, Action onSceneLoad = null);
    }
}