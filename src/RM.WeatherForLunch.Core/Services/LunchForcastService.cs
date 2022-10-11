using RM.WeatherForLunch.Core.Enums;
using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Services;

public class LunchForcastService : ILunchForcastService
{
    private readonly IWeatherAPI weatherAPI;
    private readonly IWeatherRepository weatherRepository;

    // TODO: add to settings
    private decimal maximumCloudsCover = 50;
    private decimal maximumPrecipitationInMM = 0.1M;
    private decimal minimumTemperature = 18;

    public LunchForcastService(IWeatherAPI weatherAPI, IWeatherRepository weatherRepository)
    {
        this.weatherAPI = weatherAPI;
        this.weatherRepository = weatherRepository;
    }

    public async Task<LunchForcast> GetLunchForcast(string city)
    {
        LunchForcast? lunchForcast = null;
        var lunchForcastFromDb = weatherRepository.GetLatestToday(city);
        if (lunchForcastFromDb != null && lunchForcastFromDb.DateCreated.AddMinutes(5) >= DateTime.UtcNow)
        {
            lunchForcast = lunchForcastFromDb;
        }
        else
        {
            var forcast = await weatherAPI.GetWeatherAsync(city);
            var currentCondition = forcast.CurrentConditions?.FirstOrDefault();
            if (currentCondition == null) throw new Exception($"could not find weather for city: {city}");
            lunchForcast = MapLunchForcast(currentCondition);
            lunchForcast.City = city;
            weatherRepository.Add(lunchForcast);
        }

        return lunchForcast;
    }

    public async Task<List<LunchForcast>> GetHistoricLunchForcasts(string city)
    {
        return await weatherRepository.GetAllAsync(city);
    }

    private LunchForcast MapLunchForcast(CurrentCondition currentCondition)
    {
        var lunchState = new LunchForcast()
        {
            TemperatureCelsius = decimal.TryParse(currentCondition.temp_C, out decimal temperature) ? temperature : 0,
            CloudsCoverPercentage = decimal.TryParse(currentCondition.cloudcover, out decimal cloudcover) ? cloudcover : 0,
            HumidityPercentage = decimal.TryParse(currentCondition.humidity, out decimal humidity) ? humidity : 0,
            ObservationTime = DateTime.TryParse(currentCondition.localObsDateTime, out DateTime localObsDateTime) ? localObsDateTime : DateTime.Now,
            WindDirectionDegrees = decimal.TryParse(currentCondition.winddirDegree, out decimal winddirDegree) ? winddirDegree : 0,
            WindSpeedKmPH = int.TryParse(currentCondition.windspeedKmph, out int windspeedKmph) ? windspeedKmph : 0,
            PrecipitationInMM = decimal.TryParse(currentCondition.precipMM, out decimal precipMM) ? precipMM : 0,
        };
        lunchState.CanSitOutside = CanSitOutsideValidator(lunchState);

        return lunchState;
    }

    private bool CanSitOutsideValidator(LunchForcast lunchState)
    {
        if (lunchState == null) return false;

        if (lunchState.CloudsCoverPercentage > maximumCloudsCover) return false;
        if (lunchState.PrecipitationInMM > maximumPrecipitationInMM) return false;
        if (lunchState.WindSpeedKmPH > (int)KNMIWindSchaal.ZwakMax) return false;

        if (lunchState.TemperatureCelsius < minimumTemperature) return false;

        return true;
    }

    public async Task<LunchForcast?> GetLunchForcastByDateAndTime(string city, DateTime dateTime)
    {
        var lunchForcast = await weatherRepository.GetLunchForcastByDate(city, dateTime);        
        return lunchForcast;
    }
}
