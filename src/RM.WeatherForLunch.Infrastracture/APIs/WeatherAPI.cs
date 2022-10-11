using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace RM.WeatherForLunch.Infrastracture.APIs
{
    public class WeatherAPI : IWeatherAPI
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string baseAddress = "https://wttr.in/";

        public WeatherAPI(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<Forcast> GetWeatherAsync(string city)
        {
            var forcast = new Forcast();
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseAddress);

            var response = await httpClient.GetAsync($"{city}?format=j1");
            var json = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                forcast = await response.Content.ReadFromJsonAsync<Forcast>() ?? new Forcast();
            }

            //var forcast = await httpClient.GetFromJsonAsync<Forcast>($"{city}?format=j1");
            return forcast;
        }
    }
}
