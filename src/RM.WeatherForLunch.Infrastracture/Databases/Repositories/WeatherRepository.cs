using Microsoft.EntityFrameworkCore;
using RM.WeatherForLunch.Core.Interfaces;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Infrastracture.Databases.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly AppDbContext dbContext;

    public WeatherRepository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public LunchForcast? Get(int id)
    {
        return dbContext.LunchForcasts?.Find(id);
    }

    public Task<List<LunchForcast>> GetAllAsync(string city)
    {
        return dbContext.LunchForcasts
            .Where(lunchForcast => lunchForcast.City == city)
            .ToListAsync();
    }

    public LunchForcast? GetLatestToday(string city)
    {
        return dbContext.LunchForcasts            
            .LastOrDefault(lunchForcast => 
                lunchForcast.City == city &&
                lunchForcast.DateCreated.Date.Equals(DateTime.UtcNow.Date));
    }

    public LunchForcast? GetLunchForcastByDate(string city, DateTime date)
    {
        return dbContext.LunchForcasts?.
            Where(lunchForcast => 
                lunchForcast.City == city &&
                lunchForcast.ObservationTime == date)
            .FirstOrDefault();
    }

    public void Add(LunchForcast lunchForcast)
    {
        dbContext.LunchForcasts?.Add(lunchForcast);
        dbContext.SaveChanges();
    }
}
