namespace RM.WeatherForLunch.Web.Helpers
{
    public static class CityHelper
    {
        public static string WeatherCityCorrection(this string city)
        {
            city = city.ToLower();
            if (city.Contains(' ')) { city = city.Trim().Replace(" ", "+"); }
            return city;
        }
    }
}
