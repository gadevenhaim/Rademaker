using RM.WeatherForLunch.Core.Base;
using RM.WeatherForLunch.Infrastracture.Base;
using RM.WeatherForLunch.Web.AutoMapperProfiles;
using RM.WeatherForLunch.Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(WeatherProfile));
builder.Services.AddHttpClient();

builder.Services.RegisterDependencies(
    typeof(CoreAssembly).Assembly,
    typeof(InfrastructureAssembly).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
