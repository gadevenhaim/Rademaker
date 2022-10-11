using System.Text.Json.Serialization;

namespace RM.WeatherForLunch.Core.Models;

public class Forcast
{
    [JsonPropertyName("current_condition")]
    public List<CurrentCondition>? CurrentConditions { get; set; }
}
