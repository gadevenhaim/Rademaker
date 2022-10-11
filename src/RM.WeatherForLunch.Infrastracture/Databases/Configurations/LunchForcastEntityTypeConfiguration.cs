using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RM.WeatherForLunch.Core.Models;

namespace RM.WeatherForLunch.Infrastracture.Databases.Configurations;

public class LunchForcastEntityTypeConfiguration: IEntityTypeConfiguration<LunchForcast>
{
    public LunchForcastEntityTypeConfiguration()
    {

    }

    public void Configure(EntityTypeBuilder<LunchForcast> builder)
    {
        builder.HasKey(lunchForcast => lunchForcast.Id);
    }
}
