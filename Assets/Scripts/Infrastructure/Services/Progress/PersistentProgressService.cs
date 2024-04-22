using HolyWater.MykytaTask.Data;

namespace HolyWater.MykytaTask.Infrastructure.Services.Progress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public SessionProgress SessionProgress { get; set; }
    }
}