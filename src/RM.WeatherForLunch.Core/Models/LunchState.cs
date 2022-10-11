namespace RM.WeatherForLunch.Core.Models;

public class LunchState
{
    public decimal? PrecipitationInMM { get; set; }
    public decimal TemperatureCelsius { get; set; }
    public int WindSpeedKmPH { get; set; }
    public decimal WindDirectionDegrees { get; set; }
    public decimal CloudsCoverPercentage { get; set; }
    public decimal HumidityPercentage { get; set; }
    public DateTime ObservationTime { get; set; }
    public bool CanSitOutside { get; set; }
}
