using HolyWater.MykytaTask.Data;

namespace HolyWater.MykytaTask.Infrastructure.Services.Progress
{
    public interface IPersistentProgressService
    {
        SessionProgress SessionProgress { get; set; }
    }
}