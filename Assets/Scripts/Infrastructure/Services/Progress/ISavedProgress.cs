using HolyWater.MykytaTask.Data;

namespace HolyWater.MykytaTask.Infrastructure.Services.Progress
{
    public interface ISavedProgressReader
    {
        /// <summary>
        /// Load saved progress
        /// </summary>
        /// <param name="progress">Progress data</param>
        void LoadProgress(SessionProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        /// <summary>
        /// Save progress
        /// </summary>
        /// <param name="progress">Progress data</param>
        void UpdateProgress(SessionProgress progress);
    }
}