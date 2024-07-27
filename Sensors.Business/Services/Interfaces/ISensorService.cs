using Sensors.Business.Dto;

namespace Sensors.Business.Services.Interfaces
{
    public interface ISensorService
    {
        AverageWaterTempDto GetAverageWaterTemp(DateTime? startDate, DateTime? endDate, string? groupName = "");
        List<FishSpeciesDto> GetTopFishes(int count, DateTime? startDate, DateTime? endDate, string? groupName = "");
    }
}
