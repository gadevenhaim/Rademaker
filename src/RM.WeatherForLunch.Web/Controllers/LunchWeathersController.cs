using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;
using RM.WeatherForLunch.Web.Helpers;
using RM.WeatherForLunch.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RM.WeatherForLunch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LunchWeathersController : ControllerBase
    {
        private readonly IMapper mapper;

        public LunchWeathersController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<WeatherInformationViewModel>> GetAll(string city, [FromServices] ILunchForcastService lunchForcastService)
        {
            city = city.WeatherCityCorrection();
            var lunchForcasts = await lunchForcastService.GetHistoricLunchForcasts(city);
            if (lunchForcasts == null) return (List<WeatherInformationViewModel>)Results.NotFound("No information was found");

            return lunchForcasts.Select(lunchForcast => mapper.Map<WeatherInformationViewModel>(lunchForcast));
        }

        [HttpGet("current")]
        public async Task<LunchNowViewModel> GetCurrent(string city, [FromServices] ILunchForcastService lunchForcastService)
        {
            city = city.WeatherCityCorrection();
            var lunchForcast = await lunchForcastService.GetLunchForcast(city);
            if (lunchForcast == null) return (LunchNowViewModel)Results.BadRequest("Could not retrieve the required information");

            return mapper.Map<LunchNowViewModel>(lunchForcast);
        }
    }
}
