using System.Text;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;

namespace DependencyServices.Services
{
    public class WeatherDisplayService : IWeatherDisplayService
    {
        public string GenerateDisplay(WeatherResult weatherResult, string shirtDescription)
        {
            var message = new StringBuilder();
            message.AppendLine("Weather for today:");
            message.AppendLine($"High: {weatherResult.main.temp_max}");
            message.AppendLine($"Low: {weatherResult.main.temp_min}");
            message.AppendLine($"Current: {weatherResult.main.temp}");
            message.AppendLine();
            message.Append($"You should wear {shirtDescription}");
            
            

            return message.ToString();
        }
    }
}