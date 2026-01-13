namespace WebAppTest.Client.Services;

public class WeatherService
{
    //Change this one to false to try out the no-delay version
    public bool AddDelay { get; set; } = false;
    public async Task<WeatherForecast[]?> GetForecastsAsync()
    {
        if(AddDelay)
        {
            // Simulate asynchronous loading to demonstrate a loading indicator
            await Task.Delay(5000);
        }
        var startDate = DateOnly.FromDateTime(DateTime.Now);
        var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = startDate.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = summaries[Random.Shared.Next(summaries.Length)]
        }).ToArray();
    }
}
