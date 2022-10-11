using RM.WeatherForLunch.Core.Enums;
using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Core.Services;

public class LunchOutsideService : ILunchOutsideService
{
    private readonly IWeatherAPI weatherAPI;
    // TODO: add to appsettings
    private decimal maximumCloudsCover = 50;
    private decimal maximumPrecipitationInMM = 0.1M;
    private decimal minimumTemperature = 18;

    public LunchOutsideService(IWeatherAPI weatherAPI)
    {
        this.weatherAPI = weatherAPI;
    }

    public async Task<LunchState> GetLunchState(string city = "")
    {
        var forcast = await weatherAPI.GetWeatherAsync(city);
        var currentCondition = forcast.CurrentConditions?.FirstOrDefault();
        if (currentCondition == null) throw new Exception($"could not find weather for city: {city}");
        var lunchState = MapLunchState(currentCondition);

        return lunchState;
    }

    private LunchState MapLunchState(CurrentCondition currentCondition)
    {
        var lunchState = new LunchState()
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

    private bool CanSitOutsideValidator(LunchState lunchState)
    {
        if (lunchState == null) return false;

        if (lunchState.CloudsCoverPercentage > maximumCloudsCover) return false;
        if (lunchState.PrecipitationInMM > maximumPrecipitationInMM) return false;
        if (lunchState.WindSpeedKmPH > (int)KNMIWindSchaal.ZwakMax) return false;

        if (lunchState.TemperatureCelsius < minimumTemperature) return false;

        return true;
    }


}
