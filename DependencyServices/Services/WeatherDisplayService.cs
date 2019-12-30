using System.Text;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;

namespace DependencyServices.Services
{
    public class WeatherDisplayService : IWeatherDisplayService
    {
        public string GenerateDisplay(WeatherResult weatherResult)
        {
            var message = new StringBuilder();
            message.AppendLine("Weather for today:");
            message.AppendLine($"High: {weatherResult.main.temp_max}");
            message.AppendLine($"Low: {weatherResult.main.temp_min}");
            message.AppendLine($"Current: {weatherResult.main.temp}");
            message.AppendLine();
            message.Append("You should wear ");
            
            if (weatherResult.main.temp < 40)
            {
                message.Append("a jacket");
            } 
            else if (weatherResult.main.temp < 60)
            {
                message.Append("a sweatshirt");
            }
            else
            {
                message.Append("a t-shirt");    
            }

            return message.ToString();
        }
    }
}