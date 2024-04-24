using System.Collections.Generic;
using HolyWater.MykytaTask.Infrastructure.Services.Progress;
using UnityEngine;

namespace HolyWater.MykytaTask.Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressesWriters { get; }
        GameObject CreateWeatherCard(string path, Transform parent);
        void CleanReaders();
        void CleanWriters();
    }
}