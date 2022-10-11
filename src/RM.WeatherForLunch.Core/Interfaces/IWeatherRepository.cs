using RM.WeatherForLunch.Core.Base;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Interfaces;

public interface IWeatherRepository
{
    LunchForcast? Get(int id);
    Task<List<LunchForcast>> GetAllAsync(string city);
    LunchForcast? GetLatestToday(string city);
    Task<LunchForcast?> GetLunchForcastByDate(string city, DateTime date);
    void Add(LunchForcast lunchForcast);
    void Update(LunchForcast lunchForcast);
}
