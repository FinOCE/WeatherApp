using WeatherApp.Server.Services;
using WeatherApp.Server.Utils.WeatherClient;

var builder = WebApplication.CreateBuilder(args);

// Add service to builder
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddSingleton<IWeatherClient, OpenMeteoWeatherClient>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddCors(p => p.AddPolicy("no-cors", b =>
{
    b.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseCors("no-cors");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
