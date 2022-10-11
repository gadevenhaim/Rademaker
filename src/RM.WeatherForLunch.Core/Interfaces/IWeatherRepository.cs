using RM.WeatherForLunch.Core.Base;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Interfaces;

public interface IWeatherRepository
{
    LunchForcast? Get(int id);
    List<LunchForcast>? GetAll(string city);
    LunchForcast? GetLatestToday(string city);
    LunchForcast? GetLunchForcastByDate(string city, DateTime date);
    void Add(LunchForcast lunchForcast);
}
