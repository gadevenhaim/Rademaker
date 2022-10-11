using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RM.WeatherForLunch.Core.Interfaces;
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
        
        [HttpGet("{city}/current")]
        public async Task<LunchNowViewModel> Get(string city, [FromServices] ILunchOutsideService lunchOutsideService)
        {
            if (city.Contains(' ')) { city = city.Replace(" ", "+"); }
            var lunchState = await lunchOutsideService.GetLunchState(city);
            if (lunchState == null) return (LunchNowViewModel)Results.BadRequest("Could not retrieve the required information");

            return mapper.Map<LunchNowViewModel>(lunchState);
        }
    }
}
