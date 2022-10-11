using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Interfaces;

public interface IWeatherAPI
{
    Task<Forcast> GetWeatherAsync(string city);
}
