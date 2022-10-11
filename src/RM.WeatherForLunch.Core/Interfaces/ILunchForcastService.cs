using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Interfaces
{
    public interface ILunchForcastService
    {
        Task<LunchForcast> GetLunchForcast(string city);
        Task<List<LunchForcast>> GetHistoricLunchForcasts(string city);
    }
}