using Microsoft.AspNetCore.Mvc;
using RM.WeatherForLunch.Web.Requests;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RM.WeatherForLunch.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        // GET: api/<WeatherForLunchController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<WeatherForLunchController>/5
        [HttpGet("historic")]
        public string Get(DateTime dateTime)
        {

            return "value";
        }

        // POST api/<WeatherForLunchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
