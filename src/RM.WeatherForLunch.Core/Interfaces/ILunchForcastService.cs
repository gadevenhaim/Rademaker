using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Interfaces
{
    public interface ILunchForcastService
    {
        Task<LunchForcast> GetLunchForcast(string city);
        Task<LunchForcast?> GetLunchForcastByDateAndTime(string city, DateTime dateTime);
        Task<List<LunchForcast>> GetHistoricLunchForcasts(string city);
    }
}