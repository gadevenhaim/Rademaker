using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
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
        public async Task<ActionResult<IEnumerable<WeatherInformationViewModel>>> GetAll(string city, [FromServices] ILunchForcastService lunchForcastService)
        {
            city = city.WeatherCityCorrection();
            var lunchForcasts = await lunchForcastService.GetHistoricLunchForcasts(city);
            if (lunchForcasts == null || !lunchForcasts.Any()) return NotFound("No information was found");

            return Ok(lunchForcasts.Select(lunchForcast => mapper.Map<WeatherInformationViewModel>(lunchForcast)));
        }

        [HttpGet("current")]
        public async Task<ActionResult<LunchNowViewModel>> GetCurrent(string city, [FromServices] ILunchForcastService lunchForcastService)
        {
            city = city.WeatherCityCorrection();
            var lunchForcast = await lunchForcastService.GetLunchForcast(city);
            if (lunchForcast == null) return NotFound($"Could not retrieve the required information for {city}");

            return Ok(mapper.Map<LunchNowViewModel>(lunchForcast));
        }

        [HttpGet("byDateAndTime")]
        public async Task<ActionResult<LunchNowViewModel>> GetByDateAndTime(string city, DateTime dateTime, [FromServices] ILunchForcastService lunchForcastService)
        {
            city = city.WeatherCityCorrection();
            var lunchForcast = await lunchForcastService.GetLunchForcastByDateAndTime(city, dateTime);
            if (lunchForcast == null) return NotFound($"Could not retrieve the required information for {city} at {dateTime.ToString()}");

            return Ok(mapper.Map<LunchNowViewModel>(lunchForcast));
        }
    }
}
