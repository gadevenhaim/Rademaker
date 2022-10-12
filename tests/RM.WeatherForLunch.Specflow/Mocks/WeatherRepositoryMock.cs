using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Specflow.Mocks
{
    public class WeatherRepositoryMock : IWeatherRepository
    {
        public void Add(LunchForcast lunchForcast)
        {
            
        }

        public LunchForcast? Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<LunchForcast>> GetAllAsync(string city)
        {
            throw new NotImplementedException();
        }

        public LunchForcast? GetLatestToday(string city)
        {
            return null;
        }

        public Task<LunchForcast?> GetLunchForcastByDate(string city, DateTime date)
        {
            return Task.FromResult<LunchForcast?>(null);
        }

        public void Update(LunchForcast lunchForcast)
        {
            
        }
    }
}
