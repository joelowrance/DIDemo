using System.Text;
using DependencyServices;
using DependencyServices.Abstractions;
using DependencyServices.WeatherModels;

namespace WebApplication.Services
{
    public class HtmlWeatherDisplayService : IWeatherDisplayService 
    {
        public string GenerateDisplay(WeatherResult weatherResult, string shirtType)
        {
            var message = new StringBuilder();
            message.AppendLine("Weather for today:<br/>");
            message.AppendLine($"<b>High</b>: {weatherResult.main.temp_max}<br/>");
            message.AppendLine($"<b>Low</b>: {weatherResult.main.temp_min}<br/>");
            message.AppendLine($"<b>Current</b>: {weatherResult.main.temp}<br/>");
            message.AppendLine("<br/>");
            message.Append($"You should wear <b>{shirtType}</b>");

            return message.ToString();
        }
    }
}