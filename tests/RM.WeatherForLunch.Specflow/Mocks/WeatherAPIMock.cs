using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Specflow.Mocks
{
    public class WeatherAPIMock : IWeatherAPI
    {
        private readonly Forcast forcast;

        public WeatherAPIMock(Forcast forcast)
        {
            this.forcast = forcast;
        }
        public Task<Forcast> GetWeatherAsync(string city)
        {
            return Task.FromResult(forcast);
        }
    }
}
