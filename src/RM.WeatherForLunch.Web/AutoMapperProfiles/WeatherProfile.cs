using AutoMapper;
using RM.WeatherForLunch.Core.Models;
using RM.WeatherForLunch.Web.ViewModels;

namespace RM.WeatherForLunch.Web.AutoMapperProfiles
{
    public class WeatherProfile: Profile
    {
        public WeatherProfile()
        {
            CreateMap<LunchForcast, LunchNowViewModel>();
        }
    }
}
