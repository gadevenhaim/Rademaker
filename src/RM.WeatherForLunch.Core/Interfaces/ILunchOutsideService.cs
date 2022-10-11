using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Interfaces
{
    public interface ILunchOutsideService
    {
        Task<LunchState> GetLunchState(string city = "");
    }
}