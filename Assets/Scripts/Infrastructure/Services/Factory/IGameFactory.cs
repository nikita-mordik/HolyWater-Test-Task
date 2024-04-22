using System.Collections.Generic;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        GameObject CreateWeatherCard(string path, Transform parent);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressesWriters { get; }
    }
}